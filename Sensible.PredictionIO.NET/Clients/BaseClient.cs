using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Sensible.PredictionIO.NET.Clients
{
    public abstract class BaseClient
    {
        public string ApiUrl { get; protected set; }
        protected RestClient RestClient;
        protected BaseClient(string apiUrl)
        {
            ApiUrl = apiUrl;
            RestClient = new RestClient(apiUrl);
        }

        protected JToken Execute(string resource, Method method, object body)
        {
            var request = new RestRequest(resource);
            request.Method = method;
            if (body != null)
            {
                request.AddParameter(Constants.ApplicationJsonContentType, body, ParameterType.RequestBody);
            }
            var content = RestClient.Execute(request).Content;
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }
            try
            {
                var json = JToken.Parse(content);
                return json;
            }
            catch
            {
                throw new Exception(content);
            }
        }
        
        protected async Task<JToken> ExecuteAsync(string resource, Method method, object body)
        {
            var request = new RestRequest(resource);
            request.Method = method;
            if (body != null)
            {
                request.AddParameter(Constants.ApplicationJsonContentType, body, ParameterType.RequestBody);
            }
            var tcs = new TaskCompletionSource<IRestResponse>();
            RestClient.ExecuteAsync(request, (response) =>
            {
                if (response.ErrorException != null)
                {
                    tcs.TrySetException(response.ErrorException);
                }
                else
                {
                    tcs.TrySetResult(response);
                }
            });
            var content = (await tcs.Task).Content;
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }
            try
            {
                var json = JToken.Parse(content);
                return json;
            }
            catch
            {
                throw new Exception(content);
            }
        }
    }
}
