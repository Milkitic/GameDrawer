using Milkitic.ApplicationUpdater.Github;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Milkitic.ApplicationUpdater
{
    public class Updater
    {
        private const int Timeout = 10000;
        private const int RetryCount = 3;
        private static readonly HttpClient HttpClient;
        public UpdaterViewModel UpdaterViewModel { get; } = new UpdaterViewModel();

        public async Task<bool?> CheckUpdateAsync()
        {
            UpdaterViewModel.IsRunningChecking = true;
            bool? result = null;
            await Task.Run(() =>
            {
                try
                {
                    string json = "";
                    while (json == "")
                    {
                        json = HttpGet(UpdaterConfig.RequestUri);
                    }

                    Console.WriteLine(@"Get");
                    List<Release> releases = JsonConvert.DeserializeObject<List<Release>>(json);
                    var latest = releases.OrderByDescending(k => k.PublishedAt)
                        .FirstOrDefault(k => !k.Draft && !k.Prerelease);
                        //.FirstOrDefault(k => !k.Draft);
                    if (latest == null)
                    {
                        UpdaterViewModel.NewRelease = null;
                        result = false;
                        return;
                    }

                    var latestVer = latest.TagName;

                    Version latestVerObj = new Version(latestVer);
                    Version nowVerObj = new Version(UpdaterViewModel.CurrentVersion);

                    if (latestVerObj <= nowVerObj)
                    {
                        UpdaterViewModel.NewRelease = null;
                        result = false;
                        return;
                    }

                    UpdaterViewModel.NewRelease = latest;
                    UpdaterViewModel.NewRelease.NewVerString = latestVer;
                    UpdaterViewModel.NewRelease.NowVerString = UpdaterViewModel.CurrentVersion;
                    UpdaterViewModel.NewRelease.Body = UpdaterViewModel.NewRelease.Body;
                    result = true;
                }
                catch
                {
                    result = null;
                }
            });
            UpdaterViewModel.IsRunningChecking = false;
            return result;
        }

        static Updater()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient =
                new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip })
                {
                    Timeout = new TimeSpan(0, 0, 0, 0, Timeout)
                };
            HttpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
        }

        private static string HttpGet(string url)
        {
            for (int i = 0; i < RetryCount; i++)
            {
                try
                {
                    var message = new HttpRequestMessage(HttpMethod.Get, url);
                    CancellationTokenSource cts = new CancellationTokenSource(Timeout);
                    HttpResponseMessage response = HttpClient.SendAsync(message, cts.Token).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception)
                {
                    if (i == RetryCount - 1)
                        throw;
                }
            }

            return null;
        }
    }
}
