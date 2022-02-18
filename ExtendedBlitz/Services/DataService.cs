using System.Threading.Tasks;
using System.Net.Http;
using ExtendedBlitz.Models.WoTBlitz.Personal_data;
using Newtonsoft.Json;
using System.Threading;

namespace ExtendedBlitz.Services
{
    internal class DataService
    {
        string application_id;
        string account_id;
        string region;
        HttpClient client;

        public DataService(HttpClient client, string application_id, string account_id, string region)
        {
            this.client = client;
            this.application_id = application_id;
            this.account_id = account_id;
            this.region = region;
        }

        private async Task<string> GetDataStream()
        {
            var response = await client.GetAsync($"https://api.wotblitz.ru/wotb/account/info/?application_id={application_id}&account_id={account_id}&extra=statistics.rating&language={region}");            
            return await response.Content.ReadAsStringAsync();
        }

        public Player GetData()
        {
            var responseBody = (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream)).Result;

            string substring = account_id.ToString();
            int index = responseBody.IndexOf(substring);
            string json = responseBody.Remove(index, substring.Length).Insert(index, "account");

            return JsonConvert.DeserializeObject<Player>(json);
        }
    }
}
