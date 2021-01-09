using ToDoListDataService.Data.Model.Entity;
using ToDoListDataService.Data.Repository.BaseRepository;

namespace ToDoListDataService.Data.Repository.TodoItemRepository
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem>
    {
        
    }
}