using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Sensible.PredictionIO.NET
{
    public class UserActionRequestBuilder:AbstractRequestBuilder
    {
        private UserAction _userAction;

        internal UserActionRequestBuilder(string apiUrl, string appKey, UserAction userAction)
            : base(apiUrl, appKey)
        {
            _userAction = userAction;
        }

        public RestClient Build()
        {
            var client = new RestClient(_apiUrl);
            RestRequest = new RestRequest("/actions/u2i.json", Method.POST);
            RestRequest.AddParameter("pio_appkey", _appKey);
            RestRequest.AddParameter("pio_action", _userAction.Action);
            RestRequest.AddParameter("pio_uid", _userAction.UserId);
            RestRequest.AddParameter("pio_iid", _userAction.ItemId);
            if (_userAction.Coordinates != null && _userAction.Coordinates.Count == 2)
            {
                RestRequest.AddParameter("pio_latlng", string.Format("{0},{1}", _userAction.Coordinates[0].ToString(CultureInfo.InvariantCulture), _userAction.Coordinates[1].ToString(CultureInfo.InvariantCulture)));
            }
            return client;
        }
    }
}
