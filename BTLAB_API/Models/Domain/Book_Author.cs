using System.ComponentModel.DataAnnotations;

namespace BTLAB_API.Models.Domain
{
    public class Book_Author
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Books books { get; set; }
        public int AuthorId { get; set; }
        public Authors authors { get; set; }

    }
}
