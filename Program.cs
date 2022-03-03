using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GlobalATM
{
    public class Program
    {
        public static void Main(string[] args)
        {
    //         var client = new HttpClient();
    //         var request = new HttpRequestMessage
    //         {
                
    //             Method = HttpMethod.Get,
    //             RequestUri = new Uri("https://currency-converter5.p.rapidapi.com/currency/convert?format=json&from=USD&to=CAD&amount=1"),
    //             Headers =
    // {
    //     { "x-rapidapi-host", "currency-converter5.p.rapidapi.com" },
    //     { "x-rapidapi-key", "fdfc279848msh76c57f64e86b9cep18b915jsn4f1f3af214ee" },
    // },
    //         };
    //         using (var response = await client.SendAsync(request))
    //         {
    //             response.EnsureSuccessStatusCode();
    //             var body = await response.Content.ReadAsStringAsync();
    //             Console.WriteLine(body);
    //         }
            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
