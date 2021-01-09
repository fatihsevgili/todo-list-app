using Microsoft.EntityFrameworkCore;
using ToDoListDataService.Data.Model.Entity;

namespace ToDoListDataService.Data.Model.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<TodoItem> TodoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}