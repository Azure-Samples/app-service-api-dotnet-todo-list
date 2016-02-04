using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoListAPI.Filters;
using System.Net.Http.Headers;
using System.Configuration;
using ToDoListAPI.Models;
using ToDoListAPI;

namespace ToDoListAPI.Filters
{
    using System.Web.Http.Filters;

    public class HttpOperationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is Microsoft.Rest.HttpOperationException)
            {
                var ex = (Microsoft.Rest.HttpOperationException)context.Exception;
                context.Response = ex.Response;
            }
        }
    }
}

namespace ToDoListAPI.Controllers
{
    [HttpOperationExceptionFilterAttribute]
    public class ToDoListController : ApiController
    {
        private string owner = "*";

        private static ToDoListDataAPI NewDataAPIClient()
        {
            var client = new ToDoListDataAPI(new Uri(ConfigurationManager.AppSettings["toDoListDataAPIURL"]));
            // Uncomment following line and entire ServicePrincipal.cs file for service principal authentication of calls to ToDoListDataAPI
            //client.HttpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Bearer", ServicePrincipal.GetS2SAccessTokenForProdMSA().AccessToken);
            return client;
        }

        // GET: api/ToDoItemList
        public async Task<IEnumerable<ToDoItem>> Get()
        {
            // Uncomment following line in each action method for user authentication
            //owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            using (var client = NewDataAPIClient())
            {
                var results = await client.ToDoList.GetByOwnerAsync(owner);
                return results.Select(m => new ToDoItem
                {
                    Description = m.Description,
                    ID = (int)m.ID,
                    Owner = m.Owner
                });
            }
        }

        // GET: api/ToDoItemList/5
        public async Task<ToDoItem> GetByID(int id)
        {
            //owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            using (var client = NewDataAPIClient())
            {
                var result = await client.ToDoList.GetByIdByOwnerAndIdAsync(owner, id);
                return new ToDoItem
                {
                    Description = result.Description,
                    ID = (int)result.ID,
                    Owner = result.Owner
                };
            }
        }

        // POST: api/ToDoItemList
        public async Task Post(ToDoItem todo)
        {
            //owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            todo.Owner = owner;
            using (var client = NewDataAPIClient())
            {
                await client.ToDoList.PostByTodoAsync(new ToDoItem
                {
                    Description = todo.Description,
                    ID = todo.ID,
                    Owner = todo.Owner
                });
            }
        }

        public async Task Put(ToDoItem todo)
        {
            //owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            todo.Owner = owner;
            using (var client = NewDataAPIClient())
            {
                await client.ToDoList.PutByTodoAsync(new ToDoItem
                {
                    Description = todo.Description,
                    ID = todo.ID,
                    Owner = todo.Owner
                });
            }
        }

        // DELETE: api/ToDoItemList/5
        public async Task Delete(int id)
        {
            //owner = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            using (var client = NewDataAPIClient())
            {
                await client.ToDoList.DeleteByOwnerAndIdAsync(owner, id);
            }
        }
    }
}

