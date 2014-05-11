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
        private static DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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

        [DeserializeAs(Name = "pio_startT")]
        public long StartTimeSeconds { get; set; }

        [DeserializeAs(Name = "pio_endT")]
        public long EndTimeSeconds { get; set; }

        public DateTime StartTime
        {
            get
            {
                return StartTimeSeconds == 0 ? DateTime.MinValue : _epoch.AddSeconds(StartTimeSeconds);
            }
            set { StartTimeSeconds = (long)(value - _epoch).TotalSeconds; }
        }

        public DateTime EndTime
        {
            get
            {
                return EndTimeSeconds == 0 ? DateTime.MinValue : _epoch.AddSeconds(EndTimeSeconds);
            }
            set { EndTimeSeconds = (long)(value - _epoch).TotalSeconds; }
        }

    }
}
