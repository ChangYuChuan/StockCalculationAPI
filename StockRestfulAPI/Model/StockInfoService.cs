using StockRestfulAPI;
using StockRestfulAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using YahooFinanceApi;
using System;

public class StockInfoService : IYahooFinance
{
    public IEnumerable<StockOrderDto> _stockOrders { get; set; }
    public StockInfoService()
    {

    }
    private float CalculateReturn(IReadOnlyList<Candle> candles, DateTime startDate, DateTime endTime, int buyPerMonth)
    {
        float totalReward = 0;
        float totalCost = 0;
        float totalShares = 0;
        foreach (Candle candle in candles)
        {
            int shares = (int)(buyPerMonth / candle.Close);
            totalShares = totalShares + shares;
            totalCost = totalCost + (float)(shares * candle.Close);
        }
        //reward = (shares * latest price) - cost
        totalReward = totalShares * (float)candles[candles.Count - 1].Close - totalCost;

        return totalReward;
    }

    public async Task<float> GetStocksReward(IEnumerable<StockOrderDto> stockOrders)
    {
        if (stockOrders == null) return 0;
        float sum = 0;

        foreach (StockOrderDto stockOrder in stockOrders)
        {
            IReadOnlyList<Candle> candles = await Yahoo.GetHistoricalAsync(stockOrder.Symbol, stockOrder.StartDate, stockOrder.EndDate, Period.Monthly);
            var reward = CalculateReturn(candles, stockOrder.StartDate, stockOrder.EndDate, stockOrder.PayPerMonth);
            sum = sum + reward;
        }
        return sum;
    }

    public async Task<IEnumerable<StockOrderDto>> GetStockOrdersHistory(IEnumerable<StockOrderDto> stockOrders)
    {
        if (stockOrders == null) return null;
        _stockOrders = stockOrders;

        foreach (StockOrderDto stockOrder in _stockOrders)
        {
            IReadOnlyList<Candle> candles = await Yahoo.GetHistoricalAsync(stockOrder.Symbol, stockOrder.StartDate, stockOrder.EndDate, Period.Monthly);
            stockOrder.CreateResultCandles(candles);
        }
        return stockOrders;
    }
}


