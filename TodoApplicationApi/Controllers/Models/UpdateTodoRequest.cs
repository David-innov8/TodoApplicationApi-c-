namespace TodoApplicationApi.Controllers.Models
{
    public class UpdateTodoRequest
    {
        public string title { get; set; }
        public string description { get; set; }

        public bool completed { get; set; }
    }
}
