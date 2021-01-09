using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ToDoListDataService.Data.Model.Entity;
using ToDoListDataService.Data.Repository.TodoItemRepository;
using ToDoListDataService.Dto;
using ToDoListDataService.Exceptions;
using ToDoListDataService.Service.Interface;
using ToDoListDataService.Utils;

namespace ToDoListDataService.Service.Impl
{
    public class TodoListServicesImpl : ITodoListServices
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IMapper _mapper;

        public TodoListServicesImpl(ITodoItemRepository todoItemRepository, IMapper mapper)
        {
            _todoItemRepository = todoItemRepository;
            _mapper = mapper;
        }

        public List<TodoItemDto> GetAllItems()
        {
            return _mapper.Map<List<TodoItemDto>>(_todoItemRepository.All()
                .OrderBy(x=> x.Id));
        }

        public TodoItemDto CreateItem(TodoItemDto todoItemDto)
        {
            var result = _todoItemRepository.Add(_mapper.Map<TodoItem>(todoItemDto));
            return _mapper.Map<TodoItemDto>(result);
        }

        public TodoItemDto UpdateItem(TodoItemDto todoItemDto)
        {
            var oldResource = _todoItemRepository.Get(todoItemDto.Id);
            if (oldResource == null)
                throw new TodoException(ExceptionTypes.RECORD_NOT_FOUND);
            var result = _todoItemRepository.Update(_mapper.Map(todoItemDto,oldResource));
            return _mapper.Map<TodoItemDto>(result);
        }

        public bool DeleteItem(int id)
        {
            var oldResource = _todoItemRepository.Get(id);
            if (oldResource == null)
                throw new TodoException(ExceptionTypes.RECORD_NOT_FOUND);
            var result = _todoItemRepository.Delete(oldResource);
            if (result)
                return true;
            throw new TodoException(ExceptionTypes.DB_ERROR);
        }
    }
}