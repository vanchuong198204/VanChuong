using BTLAB_API.Data;
using BTLAB_API.Interfaces;
using BTLAB_API.Models.Domain;
using BTLAB_API.Models.DTO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using BTLAB_API.Repositories;
using System.ComponentModel;


namespace BTLAB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        protected readonly AppDbContext _dbcontext;
        private readonly IBookRepository _bookRepository;
        public BookController(AppDbContext dbcontext, IBookRepository bookRepository)
        {
            _dbcontext = dbcontext;
            _bookRepository = bookRepository;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var AllBook = await  _bookRepository.GetAllBooks();
            return StatusCode(StatusCodes.Status200OK, AllBook);
        }
        [HttpGet("Get-Id")]
        public async Task<IActionResult> GetById(int id)
        {
            //var book = _dbcontext.Books.Where(b => b.Id == id);
            //if (book == null)
            //{
            //    return NotFound();
            //}
            //var bookDTO = book.Select(book => new BookDTO()
            //{
            //    Id = book.Id,
            //    Title = book.Title,
            //    Description = book.Description,
            //    IsRead = book.IsRead,
            //    DateRead = book.DateRead,
            //    Rate = book.Rate,
            //    Genre = book.Genre,
            //    CoverUrl = book.CoverUrl,
            //    DateAdded = book.DateAdded,
            //    PublishersName = book.publishers.Name,
            //    AuthorName = book.Book_Author.Select(ba => ba.authors.FullName).ToList()
            //});

            var AllBook = _bookRepository.GetBookById(id);
            return StatusCode(StatusCodes.Status200OK, AllBook);
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookDTO)
        {
            //var book = new Books
            //{
            //    Title = bookDTO.Title,
            //    Description = bookDTO.Description,
            //    IsRead = bookDTO.IsRead,
            //    DateAdded = bookDTO.DateAdded,
            //    Rate = bookDTO.Rate,
            //    Genre = bookDTO.Genre,
            //    CoverUrl = bookDTO.CoverUrl,
            //    DateRead = bookDTO.DateRead,
            //    PublishersId = bookDTO.PublishersId
            //};
            //_dbcontext.Books.Add(book);
            //await _dbcontext.SaveChangesAsync();
            //return StatusCode(StatusCodes.Status200OK, "Successsfully");
            var AllBook =  _bookRepository.GetAllBooks();
            return StatusCode(StatusCodes.Status200OK, AllBook);
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {

            //var book = await _dbcontext.Books.FindAsync(id);
            //if (book == null)
            //{
            //    return NotFound();
            //}
            //book.Title = bookDTO.Title;
            //book.Description = bookDTO.Description;
            //book.IsRead = bookDTO.IsRead;
            //book.DateAdded = bookDTO.DateAdded;
            //book.Rate = bookDTO.Rate;
            //book.Genre = bookDTO.Genre;
            //book.CoverUrl = bookDTO.CoverUrl;
            //book.DateRead = bookDTO.DateRead;
            //book.PublishersId = bookDTO.PublishersId;
            //_dbcontext.Entry(book).State = EntityState.Modified;
            //try
            //{
            //    await _dbcontext.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!BookExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            var AllBook = _bookRepository.GetAllBooks();
            return StatusCode(StatusCodes.Status200OK, AllBook);
        }
        private bool BookExists(int id)
        {
            
            return _dbcontext.Books.Any(e => e.Id == id);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            //var book = await _dbcontext.Books.FindAsync(id);
            //if (book == null)
            //{
            //    return NotFound();
            //}
            //_dbcontext.Books.Remove(book);
            //await _dbcontext.SaveChangesAsync();
            var AllBook = _bookRepository.GetAllBooks();
            return StatusCode(StatusCodes.Status200OK, AllBook);
        }


    }
}
