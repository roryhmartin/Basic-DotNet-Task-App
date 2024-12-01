using Microsoft.EntityFrameworkCore;
using Task.Api.Models;

namespace Task.Api.Data
{
    public class ToDoDBContext : DbContext
    {
        public ToDoDBContext(DbContextOptions<ToDoDBContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> ToDoItems { get; set; } // create a table called TodoItems based on the model TodoItem


    }
}
