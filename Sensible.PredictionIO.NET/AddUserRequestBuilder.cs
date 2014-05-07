using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
namespace Sensible.PredictionIO.NET
{
    public class AddUserRequestBuilder : AbstractRequestBuilder
    {
        private User _user;

        internal AddUserRequestBuilder(string apiUrl, string appKey, User user)
            : base(apiUrl, appKey)
        {
            _user = user;
        }

        public RestClient Build()
        {
            var client = new RestClient(_apiUrl);
            RestRequest = new RestRequest("/users.json", Method.POST);
            RestRequest.AddParameter("pio_appkey", _appKey);
            RestRequest.AddParameter("pio_uid", _user.UserId);
            if (_user.Coordinates != null && _user.Coordinates.Count == 2)
            {
                RestRequest.AddParameter("pio_latlng", string.Format("{0},{1}", _user.Coordinates[0].ToString(CultureInfo.InvariantCulture), _user.Coordinates[1].ToString(CultureInfo.InvariantCulture)));
            }
            RestRequest.AddParameter("pio_inactive", _user.Inactive.ToString().ToLower());
            return client;
        }
    }
}
