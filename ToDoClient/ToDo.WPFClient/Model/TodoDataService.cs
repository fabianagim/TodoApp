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

        public async Task AddItemAsync(TodoItem item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["TodoDataServerURL"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                StringContent payload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["AddItemEndpoint"], payload);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task DeleteItemAsync(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["TodoDataServerURL"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(String.Format(ConfigurationManager.AppSettings["DeleteItemEndpoint"], id));
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task UpdateItemAsync(TodoItem item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["TodoDataServerURL"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                StringContent payload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(String.Format(ConfigurationManager.AppSettings["UpdateItemEndpoint"], item.Key.ToString()), payload);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
