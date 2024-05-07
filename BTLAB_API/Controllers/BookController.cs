using BTLAB_API.Data;
using BTLAB_API.Models.Domain;
using BTLAB_API.Models.DTO;
using BTLAB_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
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
           

            var AllBook = _bookRepository.GetBookById(id);
            return StatusCode(StatusCodes.Status200OK, AllBook);
        }
        [HttpPost ("add-book")]
        public async Task<IActionResult> AddBook(BookDTO bookDTO)
        {
            
           
            var AllBook =  _bookRepository.GetAllBooks();
            return StatusCode(StatusCodes.Status200OK, AllBook);
            

        }
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {

            
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
            
            var AllBook = _bookRepository.GetAllBooks();
            return StatusCode(StatusCodes.Status200OK, AllBook);
        }


    }
}
