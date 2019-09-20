using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;
using TodoApi.Models;

namespace ToDoClient.Model
{
    public class TodoDataService : IDataService
    {
        public async Task<List<TodoItem>> GetDataAsync()
        {
            List<TodoItem> itemsList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["TodoDataServerURL"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["GetDataEndpoint"]);

                if (response.IsSuccessStatusCode)
                {
                    itemsList = JsonConvert.DeserializeObject<List<TodoItem>>(await response.Content.ReadAsStringAsync());
                }
            }
            return itemsList;
        }
    }
}
