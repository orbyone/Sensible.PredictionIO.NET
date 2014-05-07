using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Sensible.PredictionIO.NET
{
    public class DeleteItemRequestBuilder:AbstractRequestBuilder
    {
        private string _itemId;
        internal DeleteItemRequestBuilder(string apiUrl, string appKey, string itemId) : base(apiUrl, appKey)
        {
            _itemId = itemId;
        }
        public RestClient Build()
        {
            var client = new RestClient(_apiUrl);
            RestRequest = new RestRequest(string.Format("/items/{0}.json", _itemId), Method.DELETE);
            RestRequest.AddParameter("pio_appkey", _appKey);
            return client;
        }
    }
}
