namespace BTLAB_API.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public List<string> Name_Book { get; set; }
    }
    public class AuthorNoIdDTO
    {
        public string FullName { get; set; }
    }
}
