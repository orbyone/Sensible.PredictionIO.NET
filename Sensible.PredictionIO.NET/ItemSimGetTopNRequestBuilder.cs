using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Sensible.PredictionIO.NET
{
    public class ItemSimGetTopNRequestBuilder:AbstractRequestBuilder
    {
        private SimilarityEngineRequest _similarityEngineRequest;
        internal ItemSimGetTopNRequestBuilder(string apiUrl, string appKey, SimilarityEngineRequest similarityEngineRequest) : base(apiUrl, appKey)
        {
            _similarityEngineRequest = similarityEngineRequest;
        }
        
        public RestClient Build()
        {
            var client = new RestClient(_apiUrl);
            RestRequest = new RestRequest(string.Format("/engines/itemsim/{0}/topn.json", _similarityEngineRequest.Engine), Method.GET);
            RestRequest.AddParameter("pio_appkey", _appKey);
            RestRequest.AddParameter("pio_iid", _similarityEngineRequest.ItemId);
            RestRequest.AddParameter("pio_n", _similarityEngineRequest.NumberOfItems);
            if (_similarityEngineRequest.Coordinates != null && _similarityEngineRequest.Coordinates.Count == 2)
            {
                RestRequest.AddParameter("pio_latlng", string.Format("{0},{1}", _similarityEngineRequest.Coordinates[0].ToString(CultureInfo.InvariantCulture), _similarityEngineRequest.Coordinates[1].ToString(CultureInfo.InvariantCulture)));
            }
            if (_similarityEngineRequest.Within > 0)
            {
                RestRequest.AddParameter("pio_within", _similarityEngineRequest.Within.ToString(CultureInfo.InvariantCulture));
                RestRequest.AddParameter("pio_unit", _similarityEngineRequest.Unit);
            }
            if (_similarityEngineRequest.ItemTypes != null && _similarityEngineRequest.ItemTypes.Any())
            {
                RestRequest.AddParameter("pio_itypes", string.Format("[{0}]", string.Join(",", _similarityEngineRequest.ItemTypes)));
            }
            return client;
        }
    }
}
