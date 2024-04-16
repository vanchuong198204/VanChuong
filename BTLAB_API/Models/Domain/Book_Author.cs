using System.ComponentModel.DataAnnotations;

namespace BTLAB_API.Models.Domain
{
    public class Book_Author
    {
        public int ID { get; set; }
        public int? BookID { get; set; }
        public int? AuthorID { get; set; } // Thêm khóa ngoại đến bảng Authors
        public Books? Books { get; set; }
        public Authors? Author { get; set; } // Thêm thuộc tính Author

    }
}
