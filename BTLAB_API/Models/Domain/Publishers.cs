using System.ComponentModel.DataAnnotations;

namespace BTLAB_API.Models.Domain
{
    public class Publishers
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public List<Books>? Books { get; set; }
    }
}
