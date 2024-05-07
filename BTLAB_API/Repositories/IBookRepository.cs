using BTLAB_API.Models.Domain;
using BTLAB_API.Models.DTO;

namespace BTLAB_API.Repositories
{
    public interface IBookRepository
    {
        Task<List<BookDTO>> GetAllBooks();
        BookDTO GetBookById(int id);
        AddBookDTO AddBook(AddBookDTO addBookRequestDTO);
        AddBookDTO? UpdateBookById(int id, AddBookDTO bookDTO);
        Books? DeleteBookById(int id);
    }
}
