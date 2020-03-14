using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner2.Models;

namespace WeddingPlanner2.Controllers
{
    public class HomeController : Controller
    {
        private int? UserSession
        {
            get{ return HttpContext.Session.GetInt32("UserId"); }
            set{ HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        private Context dbContext;
        public HomeController(Context context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult LandingPage()
        {
            return View();
        }


        [HttpGet("register")]
        public IActionResult Reg()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Reg(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email has been taken");
                    return View("Reg");
                } else{
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    UserSession = newUser.UserId;
                    return RedirectToAction("Dashboard");
                }
            }
            return View("Reg");
        }


        [HttpGet("login")]
        public IActionResult Log()
        {
            return View();
        }
        [HttpPost("login")]
        public IActionResult Log(Login user)
        {
            if(ModelState.IsValid)
            {
                User UserExists = dbContext.Users.FirstOrDefault(r => r.Email == user.LogEmail);
                if(UserExists == null)
                {
                    ModelState.AddModelError("LogEmail", "Invalid credentials");
                    return View("Log");
                }
                var Hasher = new PasswordHasher<Login>();
                var Result = Hasher.VerifyHashedPassword(user, UserExists.Password, user.LogPassword);
                if(Result == 0)
                {
                    ModelState.AddModelError("LogPassword", "Invalid Credentials");
                    return View("Log");
                } else{
                    UserSession = UserExists.UserId;
                    return RedirectToAction("Dashboard");
                }
            }
            return View("Log");
        }


        [HttpGet("register/login")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register/user")]
        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email has been taken");
                    return View("Index");
                } else{
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    UserSession = newUser.UserId;
                    return RedirectToAction("Dashboard");
                }
            }
            return View("Index");
        }


        [HttpPost("login/user")]
        public IActionResult Login(Login user)
        {
            if(ModelState.IsValid)
            {
                User UserExists = dbContext.Users.FirstOrDefault(r => r.Email == user.LogEmail);
                if(UserExists == null)
                {
                    ModelState.AddModelError("LogEmail", "Invalid credentials");
                    return View("Index");
                }
                var Hasher = new PasswordHasher<Login>();
                var Result = Hasher.VerifyHashedPassword(user, UserExists.Password, user.LogPassword);
                if(Result == 0)
                {
                    ModelState.AddModelError("LogPassword", "Invalid Credentials");
                    return View("Index");
                } else{
                    UserSession = UserExists.UserId;
                    return RedirectToAction("Dashboard");
                }
            }
            return View("Index");
        }


        [HttpGet("dashboard/user")]
        public IActionResult Dashboard()
        {
            if(UserSession == null)
            {
                return RedirectToAction("Index");
            }
            User user = dbContext.Users.FirstOrDefault(r => r.UserId == UserSession);
            List<Wedding> allW = dbContext.Weddings.Include(w => w.RSVPs).Include(u => u.Creator).ToList();
            Dashboard model = new Dashboard();
            model.UserId = user;
            model.WeddingList = allW;
            return View(model);
        }


        [HttpGet("wedding/new")]
        public IActionResult NewWedPage()
        {
            return View("NewWedding");
        }


        [HttpPost("wedding/create")]
        public IActionResult CreateWedding(Wedding createInvite)
        {
            if(UserSession == null)
            {
                return RedirectToAction("Index");
            }
            if(ModelState.IsValid)
            {
                createInvite.UserId = (int) UserSession;
                dbContext.Add(createInvite);
                dbContext.SaveChanges();
                return RedirectToAction("WeddingDetails", new{weddingId = createInvite.WeddingId});
            } else{
                return View("NewWedding");
            }
        }


        [HttpGet("wedding/details/{weddingId}")]
        public IActionResult WeddingDetails(int weddingId)
        {
            if(UserSession == null)
            {
                return View("Index");
            }
            Wedding showWedding = dbContext.Weddings.Include(r => r.RSVPs).ThenInclude(r => r.Guest).FirstOrDefault(r => r.WeddingId == weddingId);
            return View(showWedding);
        }


        [HttpPost("rsvp/going/{weddingId}")]
        public IActionResult RSVP(int weddingId)
        {
            if(UserSession == null)
            {
                return RedirectToAction("Index");
            }
            RSVP going = new RSVP();
            going.WeddingId = weddingId;
            going.UserId = (int)UserSession;
            dbContext.RSVPs.Add(going);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        [HttpPost("rsvp/not-going/{weddingId}")]
        public IActionResult unRSVP(int weddingId)
        {
            if(UserSession == null)
            {
                return RedirectToAction("Index");
            }
            RSVP NotGoing = dbContext.RSVPs.FirstOrDefault(r => r.WeddingId == weddingId && r.UserId == UserSession);
            dbContext.RSVPs.Remove(NotGoing);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        [HttpPost("wedding/delete/{weddingId}")]
        public IActionResult Delete(int weddingId)
        {
            if(UserSession == null)
            {
                return RedirectToAction("Index");
            }
            Wedding WedToDelete = dbContext.Weddings.FirstOrDefault(r => r.WeddingId == weddingId);
            dbContext.Weddings.Remove(WedToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
