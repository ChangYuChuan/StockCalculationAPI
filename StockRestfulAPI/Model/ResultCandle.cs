using System;
using System.Collections.Generic;
using System.Text;

namespace StockRestfulAPI.Model
{
    public class ResultCandle
    {
        public DateTime DateTime { get; set; }
        public float Cost { get; set; }
        public float Gain { get; set; }
        public float Reward { get { return Gain - Cost; } set { } }
        public float Price { get; set; }
        public int TotalShares { get; set; }
    }
}
