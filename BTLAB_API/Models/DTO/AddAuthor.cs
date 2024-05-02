namespace BTLAB_API.Models.DTO
{
    public class AddAuthor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<int> BookIds { get; set; }

    }
}
