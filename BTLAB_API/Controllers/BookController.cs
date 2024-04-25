﻿using BTLAB_API.Data;
using BTLAB_API.Models.Domain;
using BTLAB_API.Models.DTO;
using BTLAB_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;


namespace BTLAB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        protected readonly AppDbContext _dbcontext;
        private readonly IBookServices _BookServices;
        public BookController(AppDbContext dbcontext, IBookServices bookServices)
        {
            _dbcontext = dbcontext;
            _BookServices = bookServices;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var book = _dbcontext.Books.Include(b => b.publishers).Include(b => b.Book_Author).ThenInclude(a => a.authors).ToList();
            if (book == null || !book.Any())
            {
                return StatusCode(StatusCodes.Status204NoContent, "No books in database.");
            }
            var bookDTO = book.Select(book => new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = book.DateAdded,
                PublishersName = book.publishers?.Name,
                AuthorName = book.Book_Author.Select(b => b.authors.FullName).ToList(),
            }).ToList();

            return StatusCode(StatusCodes.Status200OK, bookDTO);
        }
        [HttpGet("Get-Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = _dbcontext.Books.Where(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            var bookDTO = book.Select(book => new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = book.DateAdded,
                PublishersName = book.publishers.Name,
                AuthorName = book.Book_Author.Select(ba => ba.authors.FullName).ToList()
            });
            return Ok(bookDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookDTO)
        {
            var book = new Books
            {
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                IsRead = bookDTO.IsRead,
                DateAdded = bookDTO.DateAdded,
                Rate = bookDTO.Rate,
                Genre = bookDTO.Genre,
                CoverUrl = bookDTO.CoverUrl,
                DateRead = bookDTO.DateRead,
                PublishersId = bookDTO.PublishersId
            };
            _dbcontext.Books.Add(book);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "Successsfully");
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {

            var book = await _dbcontext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = bookDTO.Title;
            book.Description = bookDTO.Description;
            book.IsRead = bookDTO.IsRead;
            book.DateAdded = bookDTO.DateAdded;
            book.Rate = bookDTO.Rate;
            book.Genre = bookDTO.Genre;
            book.CoverUrl = bookDTO.CoverUrl;
            book.DateRead = bookDTO.DateRead;
            book.PublishersId = bookDTO.PublishersId;
            _dbcontext.Entry(book).State = EntityState.Modified;
            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(StatusCodes.Status200OK, "Successsfully");
        }
        private bool BookExists(int id)
        {
            return _dbcontext.Books.Any(e => e.Id == id);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _dbcontext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _dbcontext.Books.Remove(book);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, book);
        }


    }
}
