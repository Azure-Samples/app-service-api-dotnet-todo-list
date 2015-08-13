using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TodoList.App_Start;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class TodoListController : ApiController
    {
        const string FILENAME = "todos.json";
        private GenericStorage _storage;

        private static string GetRandomFileName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        }

        private async Task<IEnumerable<TodoListItem>> GetTodos()
        {
            var todos = await _storage.Get(FILENAME);

            if (todos == null)
            {
                await _storage.Save(new TodoListItem[]
                    {
                        new TodoListItem { Content = "Pick up the groceries", Id = GetRandomFileName() },
                        new TodoListItem { Content = "Drop off the dry cleaning", Id = GetRandomFileName() },
                    }, FILENAME);
            }

            return todos;
        }

        public TodoListController()
        {
            _storage = new GenericStorage();
        }

        /// <summary>
        /// Gets the list of todo items
        /// </summary>
        /// <returns>Todos</returns>
        [HttpGet]
        [Route("~/")]
        [SwaggerResponse(HttpStatusCode.OK,
            Type = typeof(IEnumerable<TodoListItem>))]
        public async Task<IEnumerable<TodoListItem>> Get()
        {
            return await GetTodos();
        }

        /// <summary>
        /// Gets a specific todo item
        /// </summary>
        /// <param name="id">The todo item's identifier</param>
        /// <returns>The requested todo item</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK,
            Description = "OK",
            Type = typeof(TodoListItem))]
        [SwaggerResponse(HttpStatusCode.NotFound,
            Description = "Todo not found",
            Type = typeof(IEnumerable<TodoListItem>))]
        [SwaggerOperation("GetTodoById")]
        [Route("~/{id}")]
        public async Task<TodoListItem> Get([FromUri] string id)
        {
            var todos = await GetTodos();
            return todos.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Create a new todo item
        /// </summary>
        /// <param name="todoItem">The todo</param>
        /// <returns>The newly-saved todo</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created,
            Description = "Created",
            Type = typeof(TodoListItem))]
        [Route("~/")]
        public async Task<TodoListItem> Post([FromBody] TodoListItem todoItem)
        {
            var todos = await GetTodos();
            var list = todos.ToList();
            todoItem.Id = GetRandomFileName();
            list.Add(todoItem);
            await _storage.Save(list, FILENAME);
            return todoItem;
        }

        /// <summary>
        /// Deletes a todo item
        /// </summary>
        /// <param name="id">The identifier of the todo item</param>
        /// <returns>True if the todo was deleted</returns>
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK,
            Description = "OK",
            Type = typeof(bool))]
        [SwaggerResponse(HttpStatusCode.NotFound,
            Description = "Todo not found",
            Type = typeof(bool))]
        [Route("~/{id}")]
        public async Task<bool> Delete([FromUri] string id)
        {
            var todos = await GetTodos();
            var list = todos.ToList();
            list.RemoveAll(x => x.Id == id);
            await _storage.Save(list, FILENAME);
            return true;
        }
    }
}
