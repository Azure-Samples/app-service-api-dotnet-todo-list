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

        [HttpGet]
        [ResponseType(typeof(IEnumerable<TodoListItem>))]
        [Route("todos")]
        public async Task<IEnumerable<TodoListItem>> Get()
        {
            return await GetTodos();
        }

        [HttpGet]
        [ResponseType(typeof(TodoListItem))]
        [Route("todos/{id}")]
        public async Task<TodoListItem> GetById([FromUri] string id)
        {
            var todos = await GetTodos();
            return todos.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        [ResponseType(typeof(TodoListItem))]
        [Route("todos")]
        public async Task<TodoListItem> Post([FromBody] TodoListItem todoItem)
        {
            var todos = await GetTodos();
            var list = todos.ToList();
            todoItem.Id = GetRandomFileName();
            list.Add(todoItem);
            await _storage.Save(list, FILENAME);
            return todoItem;
        }

        [HttpDelete]
        [ResponseType(typeof(bool))]
        [Route("todos/{id}")]
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
