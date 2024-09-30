using Microsoft.EntityFrameworkCore;
using TodoApplicationApi.Controllers.Models;

namespace TodoApplicationApi.Data
{
    public class TodoApiDbContext : DbContext
    {
        public TodoApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public  DbSet<TodoItem>  Todo { get; set; }
    }
}
