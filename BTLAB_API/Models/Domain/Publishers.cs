namespace BTLAB_API.Models.Domain
{
    public class Publishers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Books> Books { get; set; }
    }
}
