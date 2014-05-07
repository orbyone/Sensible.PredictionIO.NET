using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace Sensible.PredictionIO.NET
{
    public class User
    {
        [DeserializeAs(Name = "pio_uid")]
        public string UserId { get; set; }

        [DeserializeAs(Name = "pio_latlng")]
        public List<double> Coordinates { get; set; }

        [DeserializeAs(Name = "pio_inactive")]
        public bool Inactive { get; set; }
    }
}
