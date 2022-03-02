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
                Checking userAccount = db.Checkings
                                        .FirstOrDefault(a => a.AccountNumber == HttpContext.Session.GetString("AccountNumber"));
                userAccount.IsCardStolen = true;
                db.SaveChanges();
                return RedirectToAction("Game");
            }
            return RedirectToAction("LogIn", "Home");
        }

        [HttpGet("/game")]
        public IActionResult Game()
        {
            if(isLoggedIn)
            {
                User loggedUser = db.Users
                    .FirstOrDefault(u => u.UserId == (int)UUID);
                return View("Game");
            }
            return RedirectToAction("LogIn", "Home");
        }

        [HttpPost("/game/submit")]
        public IActionResult GameVerifyUser(User verifiedUser, string bestKPop)
        {
            if(isLoggedIn)
            {
                User loggedUser = db.Users
                    .FirstOrDefault(u => u.UserId == (int)UUID);

                if(verifiedUser.FaveColor == loggedUser.FaveColor && verifiedUser.Breakfast == loggedUser.Breakfast && verifiedUser.AvgSpeedSwallow == loggedUser.AvgSpeedSwallow && bestKPop == "BTS")
                {
                    return RedirectToAction("AccountRecovery");
                }
                return RedirectToAction("LogIn", "Home");
            }
            return RedirectToAction("LogIn", "Home");
        }

        [HttpGet("/game/accountrecovery/success")]
        public IActionResult AccountRecovery()
        {
            if(isLoggedIn)
            {
                return View("AccountRecovery");
            }
            return RedirectToAction("LogIn", "Home");
        }
    }
}
