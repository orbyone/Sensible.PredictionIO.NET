using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Sensible.PredictionIO.NET
{
    public class ItemRecGetTopNRequestBuilder:AbstractRequestBuilder
    {
        private RecommendationEngineRequest _recommendationEngineRequest;

        internal ItemRecGetTopNRequestBuilder(string apiUrl, string appKey, RecommendationEngineRequest recommendationEngineRequest)
            : base(apiUrl, appKey)
        {
            _recommendationEngineRequest = recommendationEngineRequest;
        }

        public RestClient Build() 
        {
            var client = new RestClient(_apiUrl);
            RestRequest = new RestRequest(string.Format("/engines/itemrec/{0}/topn.json",_recommendationEngineRequest.Engine), Method.GET);
            RestRequest.AddParameter("pio_appkey", _appKey);
            RestRequest.AddParameter("pio_uid", _recommendationEngineRequest.UserId);
            RestRequest.AddParameter("pio_n", _recommendationEngineRequest.NumberOfItems);
            if (_recommendationEngineRequest.Coordinates != null && _recommendationEngineRequest.Coordinates.Count == 2)
            {
                RestRequest.AddParameter("pio_latlng", string.Format("{0},{1}", _recommendationEngineRequest.Coordinates[0].ToString(CultureInfo.InvariantCulture), _recommendationEngineRequest.Coordinates[1].ToString(CultureInfo.InvariantCulture)));
            }
            if (_recommendationEngineRequest.Within > 0)
            {
                RestRequest.AddParameter("pio_within", _recommendationEngineRequest.Within.ToString(CultureInfo.InvariantCulture));
                RestRequest.AddParameter("pio_unit", _recommendationEngineRequest.Unit);
            }
            if (_recommendationEngineRequest.ItemTypes != null && _recommendationEngineRequest.ItemTypes.Any())
            {
                RestRequest.AddParameter("pio_itypes", string.Format("[{0}]", string.Join(",", _recommendationEngineRequest.ItemTypes)));
            }
            return client;
        }
    }
}
