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

namespace GlobalATM.Controllers
{

    public class HomeController : Controller
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

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost("/register")]
        public IActionResult Register(User newUser, string AccountType, string CardNumber)
        {
            if (ModelState.IsValid) 
            {
                if(db.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use");
                    return View("Index");
                }
                PasswordHasher<User> Hasher =  new PasswordHasher<User>();
                //Hash the password only after verifying that everything else is good to go
                newUser.Pin = Hasher.HashPassword(newUser, newUser.Pin);
                if (AccountType == "Checking")
                {
                    Checking account = new Checking();
                    account.CardNumber = CardNumber;
                    newUser.Accounts.Add(account);
                }
                else 
                {
                    Saving account = new Saving();
                    account.InterestRate = .05;
                    newUser.Accounts.Add(account);
                }
                db.Add(newUser);
                db.SaveChanges();
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Dashboard");
            }

            return View("Index");
        }

        [HttpPost("transactions")]
        // public IActionResult Transaction(double amount)
        // {
        //     if (HttpContext.Session.GetInt32("UserId") != null)
        //     {
        //         User currentUser =  db.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
        //         Account userAccount = db.Accounts.Include("Transactions").FirstOrDefault(a => a.AccountNumber == HttpContext.Session.GetString("AccountNumber"));
        //         if (amount > 0)
        //         {
        //             userAccount.Transactions.Add(new Transaction(Amount));
        //             //Create a new isntance of an object of transaction
        //             Transaction newTrans = new Transaction {
        //                 Amount = amount,
        //                 CreatedAt = DateTime.Now,
        //                 UserId = currentUser.UserId
        //             };
        //         db.Add(newTrans);
        //         db.SaveChanges();
        //         return Redirect("/Account/" + currentUser.UserId);
        //         }
        //         else if (amount + currentUser.Balance < 0)
        //         {
        //             ModelState.AddModelError("Amount", "Insufficient funds");
        //             return Redirect("/Account/" + currentUser.UserId);
        //         }
        //         else 
        //         {
        //             currentUser.Balance += amount;
        //             Transaction newTransaction = new Transaction {
        //                 Amount = amount,
        //                 CreatedAt = DateTime.Now,
        //                 UpdatedAt = DateTime.Now,
        //                 UserId = currentUser.UserId
        //             };
        //             db.Add(newTransaction);
        //             db.SaveChanges();
        //             return Redirect("/Account/" + currentUser.UserId);
        //         }
        //     }

        //     return View("Index");
        // }

        [HttpGet("/login")]
        public IActionResult LogIn()
        {
            return View("LogIn");
        }

        [HttpPost("Login")]
        public IActionResult Login(LogUser logUser)
        {
            if (ModelState.IsValid)
            {
                Account account = db.Accounts
                                    .Include(a => a.User).
                                        FirstOrDefault(u => u.AccountNumber == logUser.LoginAccountNum);
                Account checkingAccount = db.Checkings
                                            .Include(u => u.User)
                                                .FirstOrDefault(u => u.CardNumber == logUser.LoginAccountNum);
                // if (db.Users.Any(u=> u.Email == newUser.Email)) 
                //User userindb = db.Users.FirstOrDefault(u => u.Email == logUser.LoginEmail);
                if (checkingAccount == null)
                {
                    ModelState.AddModelError("LoginAccountNum", "Invalid login attempt");
                    return View("Login");
                }
                //check if password is correct
                PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
                PasswordVerificationResult result = Hasher.VerifyHashedPassword(logUser, checkingAccount.User.Pin, logUser.LoginPin); 
                //When the vertifcation runs, it will passed 1(successfully) or 0(password is incorrect)
                if (result == 0)
                {
                    ModelState.AddModelError("LoginPin", "Invalid login attempt");
                    return View("Login");
                }

                HttpContext.Session.SetInt32("UserId", checkingAccount.User.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("Login");
        }

        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            if(!isLoggedIn)
            {
                return RedirectToAction("LogIn");
            }

            User loggedUser = db.Users
                .Include(u => u.Accounts)
                .FirstOrDefault(u => u.UserId == (int)UUID);

            ViewBag.allTransactions = db.Transactions
                .Where(t => t.UserId == (int)UUID)
                .ToList();

            return View("Dashboard", loggedUser);
        }

        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
