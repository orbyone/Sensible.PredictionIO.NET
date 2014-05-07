using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensible.PredictionIO.NET
{
    public class UserAction
    {
        public class Actions
        {
            public const string Like = "like";
            public const string Dislike = "dislike";
            public const string View = "view";
            public const string Conversion = "conversion";
            public const string Rate = "rate";
        }

        public string UserId { get; set; }

        public string ItemId { get; set; }
        
        public string Action { get; set; }

        public List<double> Coordinates { get; set; }
    }
}
