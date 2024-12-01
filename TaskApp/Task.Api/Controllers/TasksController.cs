using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Api.Data;
using Task.Api.Models;

namespace Task.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        // In memory storage for simplicity
        // private static readonly List<TodoItem> _todoItems = [];

        // Our database context
        private readonly ToDoDBContext _toDoDbContext;

        public TasksController(ToDoDBContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        //GET: api/Tasks
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAllTasks()
        {
            var item = _toDoDbContext.ToDoItems.ToList();
            return Ok(item);
        }

        //GET By ID: api/tasks/1
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTaskById(int id)
        {
            var todoItem = _toDoDbContext.ToDoItems.FirstOrDefault(x => x.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        //Post: api/tasks
        [HttpPost]
        public ActionResult Post([FromBody] TodoItem todoItem)
        {
            _toDoDbContext.ToDoItems.Add(todoItem);
            _toDoDbContext.SaveChanges();

            return CreatedAtAction(nameof(GetTaskById), new { id = todoItem.Id }, todoItem);
        }


        //PUT: api/tasks/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            var existingTodoItem = _toDoDbContext.ToDoItems.FirstOrDefault(x => x.Id == id);
            if (existingTodoItem == null)
            {
                return NotFound();
            }

            existingTodoItem.Name = todoItem.Name;
            existingTodoItem.Description = todoItem.Description;
            existingTodoItem.IsComplete = todoItem.IsComplete;
            _toDoDbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete ("{id}")]
        public ActionResult Delete(int id)
        {
            var todoItem = _toDoDbContext.ToDoItems.FirstOrDefault(x => x.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _toDoDbContext.ToDoItems.Remove(todoItem);
            _toDoDbContext.SaveChanges();

            return Ok();
        }

    }
}
