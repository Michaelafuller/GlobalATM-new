using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using GlobalATM.Models;
using System.Net.Mail;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GlobalATM.Controllers
{

    public class ConversionController : ControllerBase
    {

        private int? UUID
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }

        private bool isLoggedIn
        {
            get
            {
                return UUID != null;
            }
        }

        private readonly ILogger<HomeController> _logger;
        private MyContext db;

        public ConversionController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            db = context;
        }

        // [ApiController]
        // [Route("/conversion")]
        // public 

    }
}
