using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensible.PredictionIO.NET
{
    public class RecommendationEngineRequest:AbstractEngineRequest
    {
        public string UserId { get; set; }
       
    }
}
