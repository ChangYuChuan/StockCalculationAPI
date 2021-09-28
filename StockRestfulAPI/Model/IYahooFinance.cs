using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockRestfulAPI.Model
{
    public interface IYahooFinance
    {

        Task<float> GetStocksReward(IEnumerable<StockOrderDto> stockOrders);
        Task<IEnumerable<StockOrderDto>> GetStockOrdersHistory(IEnumerable<StockOrderDto> stockOrders);

    }
}