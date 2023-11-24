using System.Net.Http.Json;
using System.Net;
using System.Text;
using System;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace PizzaMaster.Infrastructure.System
{

    public class RestClient
    {
        public double apiTimeoutSec;
        public string url;

        const int numSteps = 25;
        const double progressSec = 500;

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RestClient(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            // Now you can access configuration values like this:
            this.url = _configuration["API_Url"];
            this.apiTimeoutSec = double.Parse(_configuration["API_Timeout"]);
            _httpContextAccessor = httpContextAccessor;
        }

        #region GET
        public Treturn wsGet<Treturn>(string requestUri)
        {
            var _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient httpClient = new HttpClient(_clientHandler))
            {
                httpClient.Timeout = TimeSpan.FromSeconds(apiTimeoutSec);
                httpClient.BaseAddress = new Uri(url);

            
                AddAuthorizationHeader(httpClient);



                HttpResponseMessage response = httpClient.GetAsync(requestUri).Result;

                var treturn = HandleResponse<Treturn>(response);
                return treturn;

            }
        }
        public async Task<Treturn> wsGetAsync<Treturn>(string requestUri)
        {
            var _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient httpClient = new HttpClient(_clientHandler))
            {
                httpClient.Timeout = TimeSpan.FromSeconds(apiTimeoutSec);
                httpClient.BaseAddress = new Uri(url);


                AddAuthorizationHeader(httpClient);

              
                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                var treturn = await HandleResponseAsync<Treturn>(response);


                return treturn;
               
            }
        }

        #endregion
        #region POST

        public Treturn wsPost<Treturn, Tmodel>(string requestUri, Tmodel value, bool multipartFormData = false)
        {
             var _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient httpClient = new HttpClient(_clientHandler))
            {

                httpClient.Timeout = TimeSpan.FromSeconds(apiTimeoutSec);
                httpClient.BaseAddress = new Uri(url);

              
                AddAuthorizationHeader(httpClient);

                var response = PostRequest(httpClient,requestUri, value, multipartFormData);

                var treturn = HandleResponse<Treturn>(response);
                return treturn;
            }
        }

      

        public async Task<Treturn> wsPostAsync<Treturn, Tmodel>(string requestUri, Tmodel value, bool multipartFormData = false)
        {
            var _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient httpClient = new HttpClient(_clientHandler))
            {

                httpClient.Timeout = TimeSpan.FromSeconds(apiTimeoutSec);
                httpClient.BaseAddress = new Uri(url);


                AddAuthorizationHeader(httpClient);

                var response = await PostRequestAsync(httpClient, requestUri, value, multipartFormData);
                var treturn = await HandleResponseAsync<Treturn>(response);



                return treturn;
              
            }
        }

        private HttpResponseMessage PostRequest<T>(HttpClient httpClient, string requestUri, T value, bool multipartFormData)
        {
            HttpResponseMessage response;

            if (multipartFormData)
            {
                var additionalFormData = ConvertValueToKeyValuePairs(value);
                var formData = new MultipartFormDataContent();

                foreach (var pair in additionalFormData)
                {
                    formData.Add(new StringContent(pair.Value), pair.Key);
                }

                response = httpClient.PostAsync(requestUri, formData).Result;
            }
            else
            {
                response = httpClient.PostAsJsonAsync(requestUri, value).Result;
            }

            return response;
        }

        private async Task<HttpResponseMessage> PostRequestAsync<T>(HttpClient httpClient, string requestUri, T value, bool multipartFormData)
        {
            HttpResponseMessage response;

            if (multipartFormData)
            {
                var additionalFormData = ConvertValueToKeyValuePairs(value);
                var formData = new MultipartFormDataContent();

                foreach (var pair in additionalFormData)
                {
                    formData.Add(new StringContent(pair.Value), pair.Key);
                }

                response = await httpClient.PostAsync(requestUri, formData);
            }
            else
            {
                response = await httpClient.PostAsJsonAsync(requestUri, value);
            }

            return response;
        }

        #endregion
        #region DELETE
        public Treturn wsDelete<Treturn, Tmodel>(string requestUri, Tmodel value)
        {

            var _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient httpClient = new HttpClient(_clientHandler))
            {
                httpClient.Timeout = TimeSpan.FromSeconds(apiTimeoutSec);
                httpClient.BaseAddress = new Uri(url);


                AddAuthorizationHeader(httpClient);

                // Serialize the data to JSON and create a StringContent
                string jsonData = JsonConvert.SerializeObject(value);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Create a DELETE request with the content
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
                request.Content = content;

                HttpResponseMessage response = httpClient.SendAsync(request).Result;

                var treturn = HandleResponse<Treturn>(response);
                return treturn;

            }

        }

        public async Task<Treturn> wsDeleteAsync<Treturn, Tmodel>(string requestUri, Tmodel value)
        {
            var _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient httpClient = new HttpClient(_clientHandler))
            {
                httpClient.Timeout = TimeSpan.FromSeconds(apiTimeoutSec);
                httpClient.BaseAddress = new Uri(url);

              

                AddAuthorizationHeader(httpClient);

                string jsonData = JsonConvert.SerializeObject(value);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json"); ;

                HttpResponseMessage response = await httpClient.SendAsync(request);

                var treturn = await HandleResponseAsync<Treturn>(response);

                return treturn;   

            }

        }

        #endregion
        #region PUT

        public Treturn wsPut<Treturn, Tmodel>(string requestUri, Tmodel value, bool multipartFormData = false)
        {
            var _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient httpClient = new HttpClient(_clientHandler))
            {
                httpClient.Timeout = TimeSpan.FromSeconds(apiTimeoutSec);
                httpClient.BaseAddress = new Uri(url);


                AddAuthorizationHeader(httpClient);

                var response = PutRequest(httpClient, requestUri, value, multipartFormData);

                var treturn = HandleResponse<Treturn>(response);
                return treturn;
            }
        }
        

        public async Task<Treturn> wsPutAsync<Treturn, Tmodel>(string requestUri, Tmodel value, bool multipartFormData = false)
        {
            var _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient httpClient = new HttpClient(_clientHandler))
            {
                httpClient.Timeout = TimeSpan.FromSeconds(apiTimeoutSec);
                httpClient.BaseAddress = new Uri(url);


                AddAuthorizationHeader(httpClient);
              
                var response = await PutRequestAsync(httpClient, requestUri, value, multipartFormData);
                var treturn = await HandleResponseAsync<Treturn>(response);

                return treturn;
              
            }
        }

        private HttpResponseMessage PutRequest<T>(HttpClient httpClient, string requestUri, T value, bool multipartFormData)
        {
            HttpResponseMessage response;

            if (multipartFormData)
            {
                var additionalFormData = ConvertValueToKeyValuePairs(value);
                var formData = new MultipartFormDataContent();

                foreach (var pair in additionalFormData)
                {
                    formData.Add(new StringContent(pair.Value), pair.Key);
                }

                response = httpClient.PutAsync(requestUri, formData).Result;
            }
            else
            {
                // Serialize the data to JSON and create a StringContent
                string jsonData = JsonConvert.SerializeObject(value);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                response = httpClient.PutAsync(requestUri, content).Result;
            }

            return response;
        }

        private async Task<HttpResponseMessage> PutRequestAsync<T>(HttpClient httpClient, string requestUri, T value, bool multipartFormData)
        {
            HttpResponseMessage response;

            if (multipartFormData)
            {
                var additionalFormData = ConvertValueToKeyValuePairs(value);
                var formData = new MultipartFormDataContent();

                foreach (var pair in additionalFormData)
                {
                    formData.Add(new StringContent(pair.Value), pair.Key);
                }

                response = await httpClient.PutAsync(requestUri, formData);
            }
            else
            {
                // Serialize the data to JSON and create a StringContent
                string jsonData = JsonConvert.SerializeObject(value);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                response = await httpClient.PutAsync(requestUri, content);
            }

            return response;
        }

        #endregion



        private Treturn HandleResponse<Treturn>(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    {
                        string responseContent = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Treturn>(responseContent)!;
                    }
                case HttpStatusCode.Unauthorized:
                    throw new Exception($"Unauthorized: {response.ReasonPhrase}");

                case HttpStatusCode.InternalServerError:
                    throw new Exception($"Internal Server Error: {response.ReasonPhrase}");

                case HttpStatusCode.BadRequest:
                    throw new Exception($"Bad Request: {response.ReasonPhrase}");

                default:
                    throw new Exception($"HTTP request failed with status code: {response.StatusCode}");
            }

        }

        private async Task<Treturn> HandleResponseAsync<Treturn>(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Treturn>(responseContent)!;
                    }
                case HttpStatusCode.Unauthorized:
                    throw new Exception($"Unauthorized: {response.ReasonPhrase}");

                case HttpStatusCode.InternalServerError:
                    throw new Exception($"Internal Server Error: {response.ReasonPhrase}");

                case HttpStatusCode.BadRequest:
                    throw new Exception($"Bad Request: {response.ReasonPhrase}");

                default:
                    throw new Exception($"HTTP request failed with status code: {response.StatusCode}");
            }

        }

        private void AddAuthorizationHeader(HttpClient httpClient)
        {
            byte[]? tokenBytes = _httpContextAccessor.HttpContext.Session.TryGetValue("Token", out byte[]? tempTokenBytes) ? tempTokenBytes : null;
            string token = tokenBytes != null ? Encoding.UTF8.GetString(tokenBytes) : null;

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }

        private async Task ProgressUpdate()
        {
            for (int i = 1; i <= numSteps; i++)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(progressSec / numSteps));
            }
        }

        private static List<KeyValuePair<string, string>> ConvertValueToKeyValuePairs<T>(T dto)
        {
            var properties = typeof(T).GetProperties();
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            foreach (var property in properties)
            {
                var key = property.Name;
                var value = property.GetValue(dto)?.ToString(); // Convert value to string

                if (value != null)
                {
                    keyValuePairs.Add(new KeyValuePair<string, string>(key, value));
                }
            }

            return keyValuePairs;
        }


    }
}
