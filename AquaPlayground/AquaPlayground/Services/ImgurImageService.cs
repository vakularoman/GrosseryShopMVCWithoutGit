namespace AquaPlayground.Services
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using AquaPlayground.Services.Interfaces;
    using Newtonsoft.Json.Linq;

    public class ImgurImageService : ISavingImageService
    {
        private readonly string putRequestUrl;
        private readonly string clientId;

        public ImgurImageService(IConfiguration configuration)
        {
            this.putRequestUrl = configuration.GetValue<string>("ImgurSettings:postRequestPath");
            this.clientId = configuration.GetValue<string>("ImgurSettings:ImgurClientId");
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            var response = await this.SendPostRequest(image);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseMsg = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseMsg);
            var result = Convert.ToString(jsonObject["data"]["link"]);

            return result;
        }

        private async Task<HttpResponseMessage> SendPostRequest(IFormFile image)
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, this.putRequestUrl);
            using var stream = new MemoryStream();

            await image.CopyToAsync(stream);
            byte[] byteArray = stream.ToArray();

            using var content = new MultipartFormDataContent()
            {
                { new ByteArrayContent(byteArray), "image" },
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Client-ID", this.clientId);
            request.Content = content;

            var response = await httpClient.SendAsync(request);

            return response;
        }
    }
}
