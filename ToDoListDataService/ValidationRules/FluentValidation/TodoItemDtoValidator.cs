using FluentValidation;
using ToDoListDataService.Dto;

namespace ToDoListDataService.ValidationRules.FluentValidation
{
    public class TodoItemDtoValidator : AbstractValidator<TodoItemDto>
    {
        public TodoItemDtoValidator()
        {
            RuleFor(dto => dto.Description).NotEmpty()
                .WithMessage(x => "Task description can not be empty");
            RuleFor(dto => dto.Description).MaximumLength(50)
                .WithMessage(x => "Task description should can be maximum 50 character");
        }
    }
}