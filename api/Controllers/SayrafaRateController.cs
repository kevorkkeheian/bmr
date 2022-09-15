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
    [Route("api/rate/sayrafa")]
    [ApiController]
    public class SayrafaRateController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public SayrafaRateController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        // GET: api/rate/sayrafa/latest
        [HttpGet("latest")]
        public async Task<SayrafaRate> GetRateAsync()
        {
        

            var baseAddress = _configuration.GetValue<string>("Lirarate:BaseAddress");
            var token = _configuration.GetValue<string>("Lirarate:Token");
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var request = await _client.GetFromJsonAsync<LiraRateSayrafa>("sayrafa?currency=LBP&_ver");

            var buyCount = request.Sayrafa.Count;

            var buy = request.Sayrafa[buyCount - 1];

            var timeStamp = buy[0];
            var value = buy[1];
            var volume = buy[2];

            SayrafaRate sayrafaRate = new SayrafaRate()
            {
                FromCurrency = "USD",
                ToCurrency = "LBP",
                Value = value,
                Volume = volume,
                TimeStamp = timeStamp
            };
            return sayrafaRate;


      
        }






    }
}
