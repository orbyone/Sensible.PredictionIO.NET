using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Sensible.PredictionIO.NET.Clients
{
    public abstract class BaseClient
    {
        public string ApiUrl { get; protected set; }

        public string AccessKey { get; private set; }

        protected RestClient RestClient;

        protected BaseClient(string apiUrl, string accessKey)
        {
            ApiUrl      = apiUrl;
            AccessKey   = accessKey;
            RestClient  = new RestClient(apiUrl);
            RestClient.Timeout = Constants.ClientTimeoutMS;
        }

        protected JToken Execute(string resource, Method method, object body)
        {
            var request = new RestRequest(appendAccessKey(resource));
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
            var request = new RestRequest(appendAccessKey(resource));
            request.Method = method;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json; charset=UTF-8");
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
            var content = (await tcs.Task.ConfigureAwait(false)).Content;
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

        private string appendAccessKey(string resource)
        {   
            if (AccessKey == null)
            {
                throw new ArgumentException("Access Key is NULL");
            }

            var builder = new StringBuilder(resource);
            builder.Append('?');
            builder.Append(Constants.AccessKey);
            builder.Append('=');
            builder.Append(AccessKey);
            return builder.ToString();
        }
    }
}
