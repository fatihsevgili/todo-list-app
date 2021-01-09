using System.Collections.Generic;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using ToDoListDataService.Dto;
using ToDoListDataService.Service.Interface;

namespace ToDoListDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoListServices _todoListServices;
        
        public TodoItemController(ITodoListServices todoListServices)
        {
            _todoListServices = todoListServices;
        }

        [HttpPost("Load")]
        public ActionResult<List<TodoItemDto>> Load([FromBody] DataSourceLoadOptionsBase loadOptions)
        {
            return Ok(_todoListServices.GetAllItems());
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"TODO LIST Application"};
        }
        
        
        [HttpPost]
        public ActionResult<TodoItemDto> Insert([FromBody] TodoItemDto req)
        {
            return Ok(_todoListServices.CreateItem(req));
        }

        
        [HttpPut("{id}")]
        public ActionResult<TodoItemDto> Put(int id, [FromBody] TodoItemDto req)
        {
            return Ok(_todoListServices.UpdateItem(req));
        }
        
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        { 
            return Ok(_todoListServices.DeleteItem(id));
        }
    }
}