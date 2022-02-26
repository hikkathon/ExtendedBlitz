using ExtendedBlitz.Models.WoTBlitz.Personal_data;
using ExtendedBlitz.Models.WoTBlitz;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Collections;

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

        public DataService() { }

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

        public void SaveToJson(ICollection session)
        {
            File.WriteAllText($"Data\\Sessions.json", JsonConvert.SerializeObject(session));
        }
    }
}