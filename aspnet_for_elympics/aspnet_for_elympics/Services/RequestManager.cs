using Microsoft.Extensions.Options;

namespace Aspnet_for_elympics.Services
{
    public class RequestManager : IRequestManager
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly string? requestUri;

        public RequestManager(IHttpClientFactory clientFactory, IOptions<ApplicationSettings> settings)
        {
            this.clientFactory = clientFactory;
            requestUri = settings.Value.RandomNumberRequestUri;
        }

        public bool TryGetTheNumberRequest(out string responseContent)
        {
            responseContent = "";
            var response = SendRequestForNumber();

            if (!response.Result.IsSuccessStatusCode)
                return false;

            responseContent = GetResposne(response).Result;
            if (string.IsNullOrEmpty(responseContent))
                return false;

            return true;
        }

        private async Task<HttpResponseMessage> SendRequestForNumber()
        {
            var client = clientFactory.CreateClient();
            return await client.GetAsync(requestUri);
        }

        private static async Task<string> GetResposne(Task<HttpResponseMessage> response)
        {
            return await response.Result.Content.ReadAsStringAsync();
        }
    }
}
