using Microsoft.Azure.AppService.ApiApps.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.App_Start
{
    public class GenericStorage
    {
        CloudIsolatedStorage storage;

        public GenericStorage()
        {
            storage = Runtime.FromAppSettings().IsolatedStorage;
        }

        public async Task Save(IEnumerable<TodoListItem> target, string filename)
        {
            var json = JsonConvert.SerializeObject(target);
            var data = Encoding.ASCII.GetBytes(json);
            await storage.WriteAsync(filename, data);
        }

        public async Task<IEnumerable<TodoListItem>> Get(string filename)
        {
            var json = await storage.ReadAsStringAsync(filename);
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonConvert.DeserializeObject<IEnumerable<TodoListItem>>(json);
        }
    }
}
