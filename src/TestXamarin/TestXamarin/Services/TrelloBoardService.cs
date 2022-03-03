using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestXamarin.Models;

namespace TestXamarin.Services
{

    public class TrelloBoardService : TrelloService
    {            

        public async Task<List<ListModel>> GetLists()
        {
            var res = await Client.GetStringAsync($"/1/boards/{TrelloApi.BoardId}/lists?fields=id,name&key={TrelloApi.ApiKey}&token={TrelloApi.Token}");
            var items = JsonConvert.DeserializeObject<List<ListModel>>(res);
            return items;
        }

        public async Task InviteMember(string boardId, string email)
        {
            var res = await Client.PutAsync($"/1/boards/{boardId}/members?key={TrelloApi.ApiKey}&token={TrelloApi.Token}&email={email}",
                null);
         
            res.EnsureSuccessStatusCode();

        }
    }
}
