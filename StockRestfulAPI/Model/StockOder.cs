using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace StockRestfulAPI.Model
{
    public class StockOrder
    {
        [Required]
        public string Symbol { get; set; }
        [Required]
        public int PayPerMonth { get; set; }
        [Required]
        public bool IsDividentReinvest { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }


    }
}
