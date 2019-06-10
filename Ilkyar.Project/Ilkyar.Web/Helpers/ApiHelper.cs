using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ilkyar.Web.Helpers
{
    public static class ApiHelper
    {
        private readonly static string webApiBaseAddress = ConfigurationManager.AppSettings["WebApiBaseAddress"];

        private static string GetKey()
        {
            return "886363c990c527eab81a1b5a1d9d2639";
        }

        private delegate Task<HttpResponseMessage> Upsert(string requestUri, HttpContent content);

        public static HttpResponseMessage CallGetApiMethod(string apiUrl, string methodName, Dictionary<string, string> queryString)
        {
            HttpResponseMessage result;

            var uriBuilder = new UriBuilder(webApiBaseAddress + apiUrl + methodName) { };
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var item in queryString) { query[item.Key] = item.Value; }
            uriBuilder.Query = query.ToString();

            using (var client = CreateClient())
            {
                var response = client.GetAsync(uriBuilder.Uri).Result;
                result = response;
            }

            return result;
        }

        internal static object CallSendApiMethod(string projectApiUrl, string v, Dictionary<string, string> queryString)
        {
            throw new NotImplementedException();
        }

        public static HttpResponseMessage CallSendApiMethod(string apiUrl, string methodName, Dictionary<string, string> queryString, object model)
        {
            HttpResponseMessage result;

            var resourceDocument = JsonConvert.SerializeObject(model);
            var content = new StringContent(resourceDocument, Encoding.UTF8, "application/json");

            var uriBuilder = new UriBuilder(webApiBaseAddress + apiUrl + methodName) { };
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var item in queryString) { query[item.Key] = item.Value; }
            uriBuilder.Query = query.ToString();

            using (var client = CreateClient())
            {
                Upsert upsert = client.PostAsync;

                var response = upsert(uriBuilder.Uri.ToString(), content).Result;
                result = response;
            }

            return result;
        }

        private static HttpClient CreateClient()
        {
            var result = new HttpClient() { BaseAddress = new Uri(webApiBaseAddress) };

            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            result.DefaultRequestHeaders.Add("apiKey", GetKey());

            return result;
        }
    }
}