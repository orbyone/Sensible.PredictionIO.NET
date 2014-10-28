using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensible.PredictionIO.NET
{
    public class DateTimeAdapter
    {
        public string Serialize(DateTime source)
        {
            return source.ToString("O");
        }

        public DateTime Deserialize(string json)
        {
            throw new NotImplementedException();
        }
    }
}
