namespace TodoApplicationApi.Controllers.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public bool completed { get; set; }

    }
}
