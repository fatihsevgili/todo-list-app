using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using DevExtreme.AspNet.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoListDataService.Controllers;
using ToDoListDataService.Dto;
using ToDoListDataService.Service.Interface;
using ToDoListDataServiceTest.Service;
using Xunit;

namespace ToDoListDataServiceTest.Controller
{
    public class TodoItemControllerTests
    {
        [Theory, AutoMoqData]
        public void Load_Should_Return_As_Expected(Mock<ITodoListServices> todoListServicesMock, List<TodoItemDto> expected,
            Mock<DataSourceLoadOptionsBase> loadOptions)
        {
            var sut = new TodoItemController(todoListServicesMock.Object);
            todoListServicesMock.Setup(x => x.GetAllItems()).Returns(expected);
            
            var result = sut.Load(loadOptions.Object);
            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject; 
            var actual = apiOkResult.Value.Should().BeAssignableTo<List<TodoItemDto>>().Subject;
            
            Assert.Equal(expected,actual);
        }

        [Theory, AutoMoqData]
        public void Insert_Should_Return_As_Expected(Mock<ITodoListServices> todoListServicesMock, TodoItemDto todoItemDto,
            TodoItemDto expected)
        {
            var sut = new TodoItemController(todoListServicesMock.Object);
            todoListServicesMock.Setup(x => x.CreateItem(todoItemDto)).Returns(expected);

            var result = sut.Insert(todoItemDto);
            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<TodoItemDto>().Subject;
            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void Put_Should_Return_As_Expected(Mock<ITodoListServices> todoListServicesMock, TodoItemDto expected)
        {
            var sut = new TodoItemController(todoListServicesMock.Object);
            todoListServicesMock.Setup(x => x.UpdateItem(expected)).Returns(expected);
            
            var result = sut.Put(expected.Id, expected);
            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<TodoItemDto>().Subject;
            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void Delete_Should_Return_As_Expected(Mock<ITodoListServices> todoListServicesMock, TodoItemDto todoItemDto)
        {
            var sut = new TodoItemController(todoListServicesMock.Object);
            todoListServicesMock.Setup(x => x.DeleteItem(todoItemDto.Id)).Returns(true);
            
            var result = sut.Delete(todoItemDto.Id);
            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<bool>().Subject;
            actual.Should().Be(true);
        }


        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute()
                : base(new Fixture().Customize(new AutoMoqCustomization()))
            {
            }
        }
    }
}