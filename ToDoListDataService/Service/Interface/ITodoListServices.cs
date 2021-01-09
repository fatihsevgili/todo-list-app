using System.Collections.Generic;
using ToDoListDataService.Dto;

namespace ToDoListDataService.Service.Interface
{
    public interface ITodoListServices
    {
        List<TodoItemDto> GetAllItems();
        TodoItemDto CreateItem(TodoItemDto todoItemDto);
        TodoItemDto UpdateItem(TodoItemDto todoItemDto);
        bool DeleteItem(int id);
    }
}