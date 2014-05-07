using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Sensible.PredictionIO.NET
{
    
    public abstract class AbstractRequestBuilder
    {
        protected string _apiUrl, _appKey;

        public RestRequest RestRequest { get; set; }

        public AbstractRequestBuilder(string apiUrl, string appKey)
        {
            _apiUrl = apiUrl;
            _appKey = appKey;
        }
    }
}
