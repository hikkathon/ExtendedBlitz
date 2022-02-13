string application_id = "07ac358d831595916aca265c2f14750c";
string account_id = "71941826";
string region = "ru";

API api = new API();

using (var client = new HttpClient())
{
    for (int i = 0; i < 1; i++)
        await api.GetContent(client, application_id, account_id, region);
}

public class API
{
    public async Task GetContent(HttpClient client, string application_id, string account_id, string region)
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync($"https://api.wotblitz.ru/wotb/account/info/?application_id={application_id}&account_id={account_id}&extra=statistics.rating&language={region}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            string substring = account_id.ToString();
            int index = responseBody.IndexOf(substring);
            string json = responseBody.Remove(index, substring.Length).Insert(index, "account");

            Console.WriteLine(json);
        }
        catch (HttpRequestException exc)
        {
            Console.WriteLine("/nException Caught!");
            Console.WriteLine("Message :{0}", exc.Message);
        }
    }

    //public async Task<string> GetAccountInfo(string application_id, string account_id, string region)
    //{
    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri($"https://api.wotblitz.{region}/");

    //        var content = new FormUrlEncodedContent(new[]
    //        {
    //            new KeyValuePair<string, string>("application_id", application_id),
    //            new KeyValuePair<string, string>("account_id", account_id),
    //            new KeyValuePair<string, string>("extra", "statistics.rating")
    //        });

    //        var response = await client.PostAsync("/wotb/account/info/", content);
    //        string json = await response.Content.ReadAsStringAsync();

    //        string original = json;
    //        string substring = account_id.ToString();
    //        int i = original.IndexOf(substring);
    //        string resultJson = original.Remove(i, substring.Length).Insert(i, "account");

    //        return resultJson;
    //    }
    //}
}