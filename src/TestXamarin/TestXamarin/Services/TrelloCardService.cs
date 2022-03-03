using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestXamarin.Models;

namespace TestXamarin.Services
{
    public class TrelloCardService : TrelloService
    {
        public async Task<List<ListItemModel>> GetCards(string listId)
        {
            var res = await Client.GetStringAsync($"/1/lists/{listId}/cards?key={TrelloApi.ApiKey}&token={TrelloApi.Token}");
            var items = JsonConvert.DeserializeObject<List<ListItemModel>>(res);
            return items;
        }

        public async Task<ListItemModel> CreateCard(ListItemModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var res = await Client.PostAsync($"/1/cards/?key={TrelloApi.ApiKey}&token={TrelloApi.Token}",
                content);

            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ListItemModel>(json);
        }

        public async Task<ListItemModel> UpdateCard(ListItemModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var res = await Client.PutAsync($"/1/cards/{model.Id}/?key={TrelloApi.ApiKey}&token={TrelloApi.Token}",content);

            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ListItemModel>(json);
        }

        public async Task<List<ListItemAttachementModel>> GetCardAttachements(string cardId)
        {
            var res = await Client.GetStringAsync($"/1/cards/{cardId}/attachments?key={TrelloApi.ApiKey}&token={TrelloApi.Token}");
            var items = JsonConvert.DeserializeObject<List<ListItemAttachementModel>>(res);
            return items;
        }

        public async Task<ListItemAttachementModel> CreateCardAttachement(string name, string mimeType, string cardId, Stream data, string filename)
        {
            var content = new MultipartFormDataContent();
            
            content.Add(new StringContent(name), "name");

            if(mimeType != null)
                content.Add(new StringContent(mimeType), "mimeType");

            
            var fileContent = new StreamContent(data);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);
            fileContent.Headers.Add("Content-Disposition", "form-data; name=\"file\"; filename=\"" + filename + "\"");

            content.Add(fileContent, "file", filename);

            var res = await Client.PostAsync($"/1/cards/{cardId}/attachments?key={TrelloApi.ApiKey}&token={TrelloApi.Token}",
                content);
         
            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ListItemAttachementModel>(json);
        }

    }
}
