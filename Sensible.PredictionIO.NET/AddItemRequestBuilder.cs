using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
namespace Sensible.PredictionIO.NET
{
    public class AddItemRequestBuilder : AbstractRequestBuilder
    {
        private Item _item;
        internal AddItemRequestBuilder(string apiUrl, string appKey, Item item)
            : base(apiUrl, appKey)
        {
            _item = item;
        }

        public RestClient Build()
        {
            var client = new RestClient(_apiUrl);
            RestRequest = new RestRequest("/items.json", Method.POST);
            RestRequest.AddParameter("pio_appkey", _appKey);
            RestRequest.AddParameter("pio_iid", _item.ItemId);
            if (_item.ItemTypes != null && _item.ItemTypes.Any())
            {
                RestRequest.AddParameter("pio_itypes", string.Format("[{0}]", string.Join(",", _item.ItemTypes)));
            }
            if (_item.Coordinates != null && _item.Coordinates.Count == 2)
            {
                RestRequest.AddParameter("pio_latlng", string.Format("{0},{1}", _item.Coordinates[0].ToString(CultureInfo.InvariantCulture), _item.Coordinates[1].ToString(CultureInfo.InvariantCulture)));
            }
            RestRequest.AddParameter("pio_inactive", _item.Inactive.ToString().ToLower());
            RestRequest.AddParameter("pio_price", _item.Price.ToString(CultureInfo.InvariantCulture));
            RestRequest.AddParameter("pio_profit", _item.Profit.ToString(CultureInfo.InvariantCulture));
            if (_item.StartTime > DateTime.MinValue)
            {
                RestRequest.AddParameter("pio_startT", _item.StartTimeSeconds);
            }
            if (_item.EndTime > DateTime.MinValue)
            {
                RestRequest.AddParameter("pio_endT", _item.EndTimeSeconds);
            }
            return client;
        }
    }
}
