using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ToDoListDataService.Data.Model.Context;
using ToDoListDataService.Data.Model.Entity;
using ToDoListDataService.Data.Repository.TodoItemRepository.Impl;
using Xunit;

namespace ToDoListDataServiceTest.Repository
{
    public class TodoItemRepositoryTests
    {
        [Fact]
        public void Add_Should_Save_The_Item_And_Should_Return_All_Count_As_Two()
        {
            var Item1 = new TodoItem {Description = "item1", Completed = false};
            var Item2 = new TodoItem {Description = "item2", Completed = false};
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("add_db")
                .Options;
            using (var context = new DataContext(options))
            {
                var repository = new TodoItemRepository(context);
                repository.Add(Item1);
                repository.Add(Item2);
                context.SaveChanges();
            }
            using (var context = new DataContext(options))
            {
                var repository = new TodoItemRepository(context);
                repository.All().Count().Should().Be(2);
            }
        }

        [Fact]
        public void Delete_Should_Delete_The_Item_And_Should_Return_All_Count_As_One()
        {
            var Item1 = new TodoItem {Description = "item1", Completed = false};
            var Item2 = new TodoItem {Description = "item2", Completed = false};
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("delete_db")
                .Options;
            using (var context = new DataContext(options))
            {
                var repository = new TodoItemRepository(context);
                repository.Add(Item1);
                repository.Add(Item2);
                context.SaveChanges();
            }
            using (var context = new DataContext(options))
            {
                var repository = new TodoItemRepository(context);
                List<TodoItem> oldItemList = repository.All().ToList();
                repository.Delete(oldItemList[0]);
                repository.All().Count().Should().Be(1);
            }
        }

        [Fact]
        public void Update_Should_Update_The_Item()
        {
            var Item1 = new TodoItem {Description = "item1", Completed = false};
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("update_db")
                .Options;
            using (var context = new DataContext(options))
            {
                var repository = new TodoItemRepository(context);
                repository.Add(Item1);
                context.SaveChanges();
            }
            using (var context = new DataContext(options))
            {
                var repository = new TodoItemRepository(context);
                var oldItem = repository.Where(x => x.Description == "item1").FirstOrDefault();
                oldItem.Description = "Updated Item";
                repository.Update(oldItem);
                context.SaveChanges();
            }
            
            using (var context = new DataContext(options))
            {
                var repository = new TodoItemRepository(context);
                var result = repository.Where(x => x.Description == "Updated Item").FirstOrDefault();
                result.Should().NotBe(null);
                result?.Completed.Should().Be(false);
            }
        }


    }
}