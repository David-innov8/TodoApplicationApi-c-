using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApplicationApi.Controllers.Models;
using TodoApplicationApi.Data;

namespace TodoApplicationApi.Controllers
{
    [ApiController]
    [Route("api/TodoItems")]
    public class TodoItems : Controller
    {
        private readonly TodoApiDbContext dbContext;

        public TodoItems(TodoApiDbContext dbContext )
        {
                this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok( await dbContext.Todo.ToListAsync());
            
        }

        [HttpGet]
        [Route("{Id:guid}")]
        public async Task<IActionResult> GetSingleContact([FromRoute] Guid Id)
        {
            var todo = await dbContext.Todo.FirstOrDefaultAsync(t => t.Id == Id);

            if (todo == null) { return NotFound(); } else
            {
                return Json(todo);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(AddTodoRequest addTodoRequest)
        {
            var existingTodo = await dbContext.Todo
           .FirstOrDefaultAsync(t => t.title == addTodoRequest.title);

            if (existingTodo != null)
            {
                // If the title already exists, return a conflict response
                return Conflict(new { message = "A Todo item with this title already exists." });
            }

            // If no existing item is found, proceed to create the new Todo item
            var todoSingleItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                title = addTodoRequest.title,
                description = addTodoRequest.description,
                created = DateTime.Now,
                updated = DateTime.Now,
                completed = false
            };

            await dbContext.Todo.AddAsync(todoSingleItem);
            await dbContext.SaveChangesAsync();
            return Ok(todoSingleItem);

        }

        [HttpPut]
        [Route("{Id:guid}")]

        public async Task<IActionResult> UpdateTodo([FromRoute] Guid Id, UpdateTodoRequest updateTodoRequest) 
        {
            var todo = await dbContext.Todo.FirstOrDefaultAsync(t => t.Id == Id);
            if (todo != null) {
                todo.title = updateTodoRequest.title;
                todo.description = updateTodoRequest.description;
                todo.updated = DateTime.Now;

                await dbContext.SaveChangesAsync();
                return Ok(todo);

            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete]
        [Route("{Id:guid}")]

        public async Task<IActionResult> DeleteTodo([FromRoute] Guid Id)
        {
            var todo = await dbContext.Todo.FindAsync(Id);


            if (todo != null)
            {
                 dbContext.Remove(todo);
                dbContext.SaveChangesAsync();
                return Ok(todo);
            }
            else
            {
                return    NotFound();
            }
        }
    }
}
