using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using YahooFinanceApi;
using StockRestfulAPI.Model;
using System.Collections.Generic;
using AutoMapper;
using System.Reflection;
using Newtonsoft.Json;

namespace StockRestfulAPI.Controllers
{
    [ApiController]
    [Route("api/StockHelper")]
    public class StockController : ControllerBase
    {
        private readonly IYahooFinance _yahooFinance;
        private readonly IMapper _mapper;

        public StockController(IYahooFinance yahooFinance, IMapper mapper)
        {
            _yahooFinance = yahooFinance ?? throw new ArgumentNullException(nameof(yahooFinance));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet("TotalReward")]
        public IActionResult CalculateReturn([FromQuery] string stocksOrderJSON)
        {
            try
            {
                Console.WriteLine("CalculateReturn invoked");
                if (string.IsNullOrEmpty(stocksOrderJSON))
                    return BadRequest();
                var stocksOrders = JsonConvert.DeserializeObject<IEnumerable<StockOrder>>(stocksOrderJSON);
                var stockOrderDto = _mapper.Map<IEnumerable<StockOrderDto>>(stocksOrders);
                var returnTask = _yahooFinance.GetStocksReward(stockOrderDto);

                returnTask.Wait();
                return Ok(returnTask.Result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("TotalInfo")]
        public IActionResult GetStockOrdersInfo([FromQuery] string stocksOrderJSON)
        {
            try
            {
                Console.WriteLine("GetStockOrdersInfo invoked");
                if (string.IsNullOrEmpty(stocksOrderJSON))
                    return BadRequest();
                var stocksOrders = JsonConvert.DeserializeObject<IEnumerable<StockOrder>>(stocksOrderJSON);


                var stockOrderDto = _mapper.Map<IEnumerable<StockOrderDto>>(stocksOrders);

                var result = _yahooFinance.GetStockOrdersHistory(stockOrderDto);

                result.Wait();

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }

}
