using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text.Json;

namespace Updater
{
    internal class Program
    {
        public static string urlDownload = string.Empty;
        public static string version = string.Empty;
        static HttpClient client = new HttpClient();
        static WebClient webClient = new WebClient();

        private static async Task GetJsonDataAsync(string url)
        {

            string json = await client.GetStringAsync(url);
            Data data = JsonSerializer.Deserialize<Data>(json);

            urlDownload = data.url;
            version = data.version;
        }

        private static async Task DownloadFileAsync(string url, string fileName)
        {
            webClient.DownloadFile(new System.Uri(url), fileName);
        }

        private static void KillProcessAsync(string processName)
        {

            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                if (process.ProcessName == processName)
                {
                    process.CloseMainWindow();
                }
            }
        }

        static async Task Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0] == "Notepad")
                {
                    KillProcessAsync(args[0]);
                    await GetJsonDataAsync("https://fosorx.github.io/update.json");
                    //await DownloadFileAsync(urlDownload, "MM Client.exe");

                }
                else if (args[0] == "MM Services")
                {
                    //await GetJsonDataAsync("https://fosorx.github.io/update.json");
                    //await DownloadFileAsync(urlDownload, "MM Services.exe");
                }
            }
        }
    }
}
