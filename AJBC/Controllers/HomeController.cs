using AJBC.Data;
using AJBC.Models;
using AJBC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AJBC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string Firstname ,string Lastname, string Subject, string Emailaddress , string Message)
        {
            ViewData["Title"] = "Home Page";
            if (ModelState.IsValid)
            {
                //EF core code
                AJBCContext Context = new AJBCContext();
                var Contact = new Contacts { Firstname = Firstname ,Lastname= Lastname, Subject = Subject, Email = Emailaddress, Message = Message };
                Context.Contact.Add(Contact);
                Context.SaveChanges();
                //EF core code ends
            }

            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(Contacts contact)
        {
            ViewData["Title"] = "Contact";

            AJBCContext context = new AJBCContext();

            context.Contact.Add(contact);

            context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Review()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review(ReviewViewModel review)
        {
            AJBCContext Context = new AJBCContext();
            Review reviews = new Review();
            reviews.Date = DateTime.Now;
            if (review.Picture == null)
            {
                return View();
                ModelState.AddModelError("", "Please select a file.");
            }
            else if (review.Picture.Length != null)
            {
                using (var ms = new MemoryStream())
                {
                    review.Picture.CopyTo(ms);
                    var FileByte = ms.ToArray();
                    reviews.Picture = FileByte;
                }
            }
            Context.Review.Add(reviews);
            Context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult Services()
        {
            return View();
        }
    }
}
