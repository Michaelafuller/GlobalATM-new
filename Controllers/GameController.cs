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

namespace GlobalATM.Controllers
{

    public class GameController : Controller
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

        public GameController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet("/reportlostorstolen")]
        public IActionResult ReportLostOrStolen()
        {
            if(!isLoggedIn)
            {
                return RedirectToAction("LogIn", "Home");
            }
            User loggedUser = db.Users
                .Include(u => u.Accounts)
                .FirstOrDefault(u => u.UserId == (int)UUID);
        
                return View("ReportLostOrStolen", loggedUser);
        }

        [HttpPost("/stolen")]
        public IActionResult YesCardStolen()
        {
            if (isLoggedIn)
            {
                Account userAccount = db.Accounts
                                        .FirstOrDefault(a => a.AccountNumber == HttpContext.Session.GetString("AccountNumber"));
                userAccount.IsCardStolen = true;


            return RedirectToAction("LogIn", "Home");
        }

    }
}
