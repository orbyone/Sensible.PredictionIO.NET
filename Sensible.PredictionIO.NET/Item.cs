using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace Sensible.PredictionIO.NET
{
    public class Item
    {
        [DeserializeAs(Name = "pio_iid")]
        public string ItemId { get; set; }

        [DeserializeAs(Name = "pio_itypes")]
        public List<string> ItemTypes { get; set; }

        [DeserializeAs(Name = "pio_latlng")]
        public List<double> Coordinates { get; set; }

        [DeserializeAs(Name = "pio_inactive")]
        public bool Inactive { get; set; }

        [DeserializeAs(Name = "pio_price")]
        public double Price { get; set; }

        [DeserializeAs(Name = "pio_profit")]
        public double Profit { get; set; }

    }
}
