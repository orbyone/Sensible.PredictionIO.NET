using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensible.PredictionIO.NET
{
    public abstract class AbstractEngineRequest
    {
        public string Engine { get; set; }
        public int NumberOfItems { get; set; }
        public List<string> ItemTypes { get; set; }
        public List<double> Coordinates { get; set; }
        public double Within { get; set; }
        public string Unit { get; set; }

        public class Units
        {
            public const string Miles = "mi";
            public const string Kilometers = "km";
        }
    }
}
