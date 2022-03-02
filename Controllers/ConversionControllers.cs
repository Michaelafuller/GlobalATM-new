using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GlobalATM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GlobalATM.Controllers
{
    public class ConversionController : Controller
    {
        [HttpGet("convert")]
        public async Task<IActionResult> Convert(ConversionRaw nuConversion)
        {
            var from = nuConversion.Base_Currency_Code;
            var to = nuConversion.Currency_Name;
            var amount = nuConversion.Amount;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://currency-converter5.p.rapidapi.com/currency/convert?&amp;from={from}&amp;to={to}&amp;amount={amount}&amp;format=json"),
                Headers =
    {
        { "x-rapidapi-host", "currency-converter5.p.rapidapi.com" },
        { "x-rapidapi-key", "fdfc279848msh76c57f64e86b9cep18b915jsn4f1f3af214ee" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return View("CurrencyConverter", body);
            }
        }

        [HttpGet("currencyconverter")]
        public IActionResult CurrencyConverter()
        {
            return View("CurrencyConverter");
        }
    }
}