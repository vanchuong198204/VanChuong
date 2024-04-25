using BTLAB_API.Models.Domain;
using BTLAB_API.Models.DTO;
using System.Threading.Tasks;

namespace BTLAB_API.Interfaces
{
    public interface IBookRepository
    {
        Task<List<BookDTO>> GetAllBooks();
        BookDTO GetBookById(int id);
        Task<AddBookDTO> AddBook(AddBookDTO addBookRequestDTO);
        Task<AddBookDTO?> UpdateBookById(int id, AddBookDTO bookDTO);
        Task<Books?> DeleteBookById(int id);
    }
}
