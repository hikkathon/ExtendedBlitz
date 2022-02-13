using ExtendedBlitz.Services;
using System.Net.Http;
using System.Windows;

namespace ExtendedBlitz
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            string application_id = "07ac358d831595916aca265c2f14750c";
            string account_id = "71941826";
            string region = "ru";

            using (var client = new HttpClient())
            {
                var service_test = new DataService(client, application_id, account_id, region);
                var player = service_test.GetData();
            }
        }
    }
}
