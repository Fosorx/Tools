using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MM_Client
{
    internal class Data
    {
        public string version {  get; set; }
    }
    internal class CheckUpdate
    {
        HttpClient _httpClient = new HttpClient();
        private async Task<bool> SearchUpdate()
        {
            string json = await _httpClient.GetStringAsync("https://fosorx.github.io/update.json");
            Data data = JsonSerializer.Deserialize<Data>(json);

            Version ?verze = Assembly.GetExecutingAssembly().GetName().Version;

            if (data?.version == verze?.ToString())
                return false;
            return true;
        }

        public async Task NewUpdate()
        {
            if(await SearchUpdate())
            {
                Console.WriteLine("Nový");
                Console.ReadLine();
                Process process = new Process();

                process.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater.exe");
                process.StartInfo.Arguments = "\"MM Client\"";
                process.Start();
00000000000

                Console.WriteLine("Test");
                Console.ReadLine();
            }
        }
    }
}
