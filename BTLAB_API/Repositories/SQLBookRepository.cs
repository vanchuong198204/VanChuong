using BTLAB_API.Data;
using BTLAB_API.Interfaces;
using BTLAB_API.Models.Domain;
using BTLAB_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTLAB_API.Repositories
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly AppDbContext _dbcontext;
        public SQLBookRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<BookDTO>> GetAllBooks()
        {
            var books = await _dbcontext.Books.Include(b => b.publishers).Include(b => b.Book_Author).ThenInclude(a => a.authors).ToListAsync();

            var bookDTOs = books.Select(book => new BookDTO
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
                AuthorName = book.Book_Author.Select(ba => ba.authors.FullName).ToList(),
            }).ToList();
            return bookDTOs;
        }
        public BookDTO GetBookById(int id)
        {
            var book = _dbcontext.Books.Where(b => b.Id == id);

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
            }).FirstOrDefault();
            return bookDTO;
        }
        public async Task<AddBookDTO> AddBook(AddBookDTO addBookRequestDTO)
        {
            var book = new Books
            {
                Title = addBookRequestDTO.Title,
                Description = addBookRequestDTO.Description,
                IsRead = addBookRequestDTO.IsRead,
                DateAdded = addBookRequestDTO.DateAdded,
                Rate = addBookRequestDTO.Rate,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateRead = addBookRequestDTO.DateRead,
                PublishersId = addBookRequestDTO.PublisherId
            };
            _dbcontext.Books.Add(book);
            await _dbcontext.SaveChangesAsync();
            return addBookRequestDTO;
        }

        public async Task<AddBookDTO?> UpdateBookById(int id, AddBookDTO bookDTO)
        {
            var book = await _dbcontext.Books.FirstOrDefaultAsync(n => n.Id == id);

            if (book != null)
            {
                book.Title = bookDTO.Title;
                book.Description = bookDTO.Description;
                book.IsRead = bookDTO.IsRead;
                book.DateAdded = bookDTO.DateAdded;
                book.Rate = bookDTO.Rate;
                book.Genre = bookDTO.Genre;
                book.CoverUrl = bookDTO.CoverUrl;
                book.DateRead = bookDTO.DateRead;
                book.PublishersId = bookDTO.PublisherId;

                await _dbcontext.SaveChangesAsync();
            }

            return bookDTO;
        }

        public async Task<Books?> DeleteBookById(int id)
        {
            var book = await _dbcontext.Books.FirstOrDefaultAsync(n => n.Id == id);

            if (book != null)
            {
                _dbcontext.Books.Remove(book);
                await _dbcontext.SaveChangesAsync();
            }

            return book;
        }
    }
}
