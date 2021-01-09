using ToDoListDataService.Data.Model.Context;
using ToDoListDataService.Data.Model.Entity;
using ToDoListDataService.Data.Repository.BaseRepository.Impl;

namespace ToDoListDataService.Data.Repository.TodoItemRepository.Impl
{
    public class TodoItemRepository : BaseRepository<TodoItem>,ITodoItemRepository
    {
        public TodoItemRepository(DataContext context) : base(context)
        {
        }
    }
}