using AutoMapper;
using ToDoListDataService.Data.Model.Entity;
using ToDoListDataService.Dto;

namespace ToDoListDataService.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TodoItemDto, TodoItem>()
                .ReverseMap();
        }
    }
}