using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Sensible.PredictionIO.NET
{
    public class DeleteUserRequestBuilder:AbstractRequestBuilder
    {
        private string _userId;
        internal DeleteUserRequestBuilder(string apiUrl, string appKey, string userId) : base(apiUrl, appKey)
        {
            _userId = userId;
        }

        public RestClient Build()
        {
            var client = new RestClient(_apiUrl);
            RestRequest = new RestRequest(string.Format("/users/{0}.json", _userId), Method.DELETE);
            RestRequest.AddParameter("pio_appkey", _appKey);
            return client;
        }
    }
}
