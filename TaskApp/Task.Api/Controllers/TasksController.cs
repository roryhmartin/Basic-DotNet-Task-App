using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Api.Models;

namespace Task.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        //In memory storage for simplicity
        private static readonly List<TodoItem> _todoItems = [];

        //GET: api/Tasks
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAllTasks()
        {
            return Ok(_todoItems);
        }

        //GET By ID: api/tasks/1
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTaskById(int id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
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
            _todoItems.Add(todoItem);
            return CreatedAtAction(nameof(GetTaskById), new { id = todoItem.Id }, todoItem);
        }

    }
}
