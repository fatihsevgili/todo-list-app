using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Moq;
using ToDoListDataService.Data.Model.Context;
using ToDoListDataService.Data.Model.Entity;
using ToDoListDataService.Data.Repository.TodoItemRepository;
using ToDoListDataService.Data.Repository.TodoItemRepository.Impl;
using ToDoListDataService.Dto;
using ToDoListDataService.Service.Impl;
using Xunit;



namespace ToDoListDataServiceTest.Service
{
    public class TodoListServiceImplTests
    {
        [Theory, AutoMoqData]
        public void CreateItem_Should_Success([Frozen]Mock<IMapper> mapper, [Frozen]Mock<ITodoItemRepository> repository,
            TodoItemDto todoItemDto, TodoItem todoItem, TodoListServicesImpl sut)
        {
            mapper.Setup(x => x.Map<TodoItem>(todoItemDto)).Returns(todoItem);
            repository.Setup(x => x.Add(todoItem)).Returns(It.IsAny<TodoItem>());

            Action action = () =>
            {
                sut.CreateItem(todoItemDto);
            };
            action.Should().NotThrow<Exception>();
        }
        [Theory, AutoMoqData]
        public void UpdateItem_Should_Success([Frozen]Mock<IMapper> mapper, [Frozen]Mock<ITodoItemRepository> repository,
            TodoItemDto todoItemDto, TodoItem oldResource, TodoListServicesImpl sut)
        {
            repository.Setup(x => x.Get(todoItemDto.Id))
                .Returns(oldResource);
            mapper.Setup(x => x.Map(todoItemDto,oldResource)).Returns(oldResource);
            repository.Setup(x => x.Update(oldResource)).Returns(It.IsAny<TodoItem>());
            
            Action action = () =>
            {
                sut.UpdateItem(todoItemDto);
            };
            action.Should().NotThrow<Exception>();
        }
        
        [Fact]
        public void UpdateItem_Should_Throw_Exception_When_OldResource_Is_Empty()
        {
            Mock<ITodoItemRepository> repository = new Mock<ITodoItemRepository>(MockBehavior.Loose);
            Mock<IMapper> mapper = new Mock<IMapper>(MockBehavior.Loose);
            var sut = new TodoListServicesImpl(repository.Object, mapper.Object);
            var todoItemDto = new TodoItemDto()
            {
                Completed = false,
                Description = "task"
            };
            Action action = () =>
            {
                sut.UpdateItem(todoItemDto);
            };
            action.Should().Throw<Exception>();
        }
        [Theory, AutoMoqData]
        public void GetAllItems_Should_Success([Frozen]Mock<IMapper> mapper, [Frozen]Mock<ITodoItemRepository> repository, 
            List<TodoItem> items, List<TodoItemDto> todoItemDtos, TodoListServicesImpl sut )
        {
            repository.Setup(x => x.All()).Returns(items.AsQueryable);
            var orderedItems = items.AsQueryable().OrderBy(x => x.Id);
            mapper.Setup(x => x.Map<List<TodoItemDto>>(orderedItems)).Returns(todoItemDtos);
            
            Action action = () =>
            {
                var result = sut.GetAllItems();
                result.Count.Should().Be(todoItemDtos.Count);
            };
            action.Should().NotThrow<Exception>();
        }
        
        [Theory, AutoMoqData]
        public void DeleteItem_Should_Success([Frozen]Mock<ITodoItemRepository> repository,
            int id, TodoItem oldResource, TodoListServicesImpl sut)
        {
            repository.Setup(x => x.Get(id))
                .Returns(oldResource);
            repository.Setup(x => x.Delete(oldResource)).Returns(true);
            
            Action action = () =>
            {
                sut.DeleteItem(id);
            };
            action.Should().NotThrow<Exception>();
        }
        
        [Theory, AutoMoqData]
        public void DeleteItem_Should_Throw_Exception_When_Delete_Result_Is_False([Frozen]Mock<ITodoItemRepository> repository,
            int id, TodoItem oldResource, TodoListServicesImpl sut)
        {
            repository.Setup(x => x.Get(id))
                .Returns(oldResource);
            repository.Setup(x => x.Delete(oldResource)).Returns(false);
            
            Action action = () =>
            {
                sut.DeleteItem(id);
            };
            action.Should().Throw<Exception>();
        }
        
        [Fact]
        public void DeleteItem_Should_Throw_Exception_When_Old_Resource_Is_Null()
        {
            Mock<ITodoItemRepository> repository = new Mock<ITodoItemRepository>(MockBehavior.Loose);
            Mock<IMapper> mapper = new Mock<IMapper>(MockBehavior.Loose);
            var sut = new TodoListServicesImpl(repository.Object, mapper.Object);
            Action action = () =>
            {
                sut.DeleteItem(1);
            };
            action.Should().Throw<Exception>();
        }
        
    }
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}