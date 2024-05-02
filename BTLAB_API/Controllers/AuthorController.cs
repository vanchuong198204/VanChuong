using BTLAB_API.Data;
using BTLAB_API.Models.Domain;
using BTLAB_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BTLAB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        protected readonly AppDbContext _dbcontext;

        public AuthorController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var author = _dbcontext.Authors.Include(a => a.Book_Authors).ThenInclude(ba => ba.books).ToList();
            if (author == null || !author.Any())
            {
                return StatusCode(StatusCodes.Status204NoContent, "No books in database.");
            }
            var authorDTO = author.Select(author => new AuthorDTO
            {
                Id = author.Id,
                FullName = author.FullName,
                Name_Book = author.Book_Authors.Select(ba => ba.books.Title).ToList(),
            }).ToList();
            return StatusCode(StatusCodes.Status200OK, authorDTO);
        }
        [HttpGet("Get-Id")]
        public async Task<IActionResult> GetById()
        {
            var author = _dbcontext.Authors.Include(a => a.Book_Authors).ThenInclude(b => b.books).ToList();
            if (author == null || !author.Any())
            {
                return StatusCode(StatusCodes.Status204NoContent, "No books in database.");
            }
            var authorDTO = author.Select(author => new AuthorDTO
            {
                Id = author.Id,
                FullName = author.FullName,
                Name_Book = author.Book_Authors.Select(b => b.books.Title).ToList(),
            }).ToList();
            return StatusCode(StatusCodes.Status200OK, authorDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthor author)
        {

            var authoradd = new Authors
            {
                FullName = author.FullName,

            };

            _dbcontext.Authors.Add(authoradd);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "Successes");
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AddAuthor authorDTO)
        {
            var author = await _dbcontext.Authors.FindAsync(id);
            if (author == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No books in database");
            }
            author.FullName = authorDTO.FullName;
            _dbcontext.SaveChanges();
            var book = _dbcontext.BooksAuthor.Where(a => a.AuthorId == id).ToList();
            if (author != null)
            {
                _dbcontext.BooksAuthor.RemoveRange(book);

                _dbcontext.SaveChanges();
            }
            foreach (var authorid in authorDTO.BookIds) 
            {
                var book_author = new Book_Author()
                {
                    BookId = id,
                    AuthorId = authorid
                };
            }
            return StatusCode(StatusCodes.Status200OK, author);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _dbcontext.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();

            }
            _dbcontext.Authors.Remove(author);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, author);
        }
    }
}
