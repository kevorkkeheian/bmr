#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Models;
using System.Text.Json;
using Application.Data;

namespace Application.Server.Controllers.Org
{
    [Route("api/rate/blackmarket")]
    [ApiController]
    public class BlackMarketRateController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public BlackMarketRateController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }


        // GET: api/rate/blackmarket
        [HttpGet]
        public async Task<LiraRateBM> GetRateAsync()
        {
            var baseAddress = _configuration.GetValue<string>("Lirarate:BaseAddress");
            var token = _configuration.GetValue<string>("Lirarate:Token");
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            return await _client.GetFromJsonAsync<LiraRateBM>("rates?currency=LBP&_ver");
      
        }

        // GET: api/rate/blackmarket/buy
        [HttpGet("buy")]
        public async Task<List<List<Int64>>> GetBuyRateAsync()
        {
            var baseAddress = _configuration.GetValue<string>("Lirarate:BaseAddress");
            var token = _configuration.GetValue<string>("Lirarate:Token");
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var request =  await _client.GetFromJsonAsync<LiraRateBM>("rates?currency=LBP&_ver");

            return request.Buy;
      
        }

        // GET: api/rate/blackmarket/sell
        [HttpGet("sell")]
        public async Task<List<List<Int64>>> GetSellRateAsync()
        {
            var baseAddress = _configuration.GetValue<string>("Lirarate:BaseAddress");
            var token = _configuration.GetValue<string>("Lirarate:Token");
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var request =  await _client.GetFromJsonAsync<LiraRateBM>("rates?currency=LBP&_ver");

            return request.Sell;
      
        }


        // GET: api/rate/blackmarket/latest
        [HttpGet("latest")]
        public async Task<BlackMarketRate> GetLatestRateAsync()
        {
            var baseAddress = _configuration.GetValue<string>("Lirarate:BaseAddress");
            var token = _configuration.GetValue<string>("Lirarate:Token");
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var request = await _client.GetFromJsonAsync<LiraRateBM>("rates?currency=LBP&_ver");

            var buyCount = request.Buy.Count;
            var sellCount = request.Sell.Count;

            var buy = request.Buy[buyCount - 1];
            var sell = request.Sell[sellCount - 1];

            var timeStamp = buy[0];
            var buyValue = buy[1];
            var sellValue = sell[1];

            BlackMarketRate blackMarketRate = new BlackMarketRate()
            {
                FromCurrency = "USD",
                ToCurrency = "LBP",
                Buy = buyValue,
                Sell = sellValue,
                TimeStamp = timeStamp
            };
            


            return blackMarketRate;
      
        }

    }
}
