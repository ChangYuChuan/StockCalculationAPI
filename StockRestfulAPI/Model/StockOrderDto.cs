using System;
using System.Collections.Generic;
using YahooFinanceApi;
using System.ComponentModel.DataAnnotations;

namespace StockRestfulAPI.Model
{
    public class StockOrderDto
    {
        [Required]
        public string Symbol { get; set; }
        [Required]
        public int PayPerMonth { get; set; }
        [Required]
        public bool IsDividentReinvest { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }


        public List<ResultCandle> ResultCandles { get; set; } = new List<ResultCandle>();

        public void CreateResultCandles(IReadOnlyList<Candle> candles)
        {
            if (this.ResultCandles == null || candles == null)
                return;
            float totalCost = 0;
            int totalShares = 0;
            float totalReward = 0;
            foreach (Candle candle in candles)
            {
                if (candle.Close == 0)
                    continue;
                int shares = (int)(this.PayPerMonth / candle.Close);
                float cost = (float)(shares * candle.Close);
                totalCost = totalCost + cost;
                totalShares = totalShares + shares;
                float totalGain = (float)(totalShares * candle.Close);
                totalReward = totalGain - totalCost;
                this.ResultCandles.Add(new ResultCandle
                {
                    DateTime = candle.DateTime,
                    TotalShares = totalShares,
                    Cost = totalCost,
                    Gain = totalGain,
                    Reward = totalReward,
                    Price = (float)candle.Close
                });
            }
        }

    }
}