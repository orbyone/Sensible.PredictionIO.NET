using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Sensible.PredictionIO.NET
{
    public class EngineResponse
    {
        [DeserializeAs(Name = "pio_iids")]
        public List<string> ItemIds { get; set; }
    }
}
