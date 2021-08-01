using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PixelDesk.Shared.Abstractions.Services;
using PixelDesk.Shared.Models;

namespace PixelDesk.Shared.Services
{
    public class IntercomService : IIntercomService
    {
        private readonly HttpClient httpClient;

        public IntercomService(string intercomModuleUrl)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(intercomModuleUrl);
        }

        public async Task<IntercomModule> getIntercomData()
        {
            var response = await httpClient.GetAsync("");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
            return JsonSerializer.Deserialize<IntercomModule>(await response.Content.ReadAsStringAsync());
        }
    }
}