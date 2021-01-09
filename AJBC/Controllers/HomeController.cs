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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;

namespace AJBC.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment environment)
        {
            _environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {

            AJBCContext context = new AJBCContext();
            Review review = new Review();

            //var id = review.Picture.Length;
            //review = context.Review.FirstOrDefault();
            review = context.Review.OrderBy(r => Guid.NewGuid()).Take(1).First();
            /*            var myfile = Convert.ToBase64String(review.Picture)*/
            ;
            //File(review.Picture, "data:Image/png;base64","picture.png");

            return View(review);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string Firstname, string Lastname, string Subject, string Emailaddress, string Message)
        {
            ViewData["Title"] = "Home Page";
            if (ModelState.IsValid)
            {
                //EF core code
                AJBCContext Context = new AJBCContext();
                var Contact = new Contact { Firstname = Firstname, Lastname = Lastname, Subject = Subject, Email = Emailaddress, Message = Message };
                Context.Contact.Add(Contact);
                Context.SaveChanges();
                //EF core code ends

                ///////    Send Email   mail:ajbbuilding49@gmail.com pass:Awedxzs09Am  ///////
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("AJ Building ", "send email to ...");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("AJ Building", "send email to ...");
                message.To.Add(to);

                message.Subject = " Contact";

                BodyBuilder bodyBuilder = new BodyBuilder();
                var usericfile = System.IO.File.OpenRead(_environment.WebRootPath + @"\Email\newuser.png");
                MemoryStream newms = new MemoryStream();
                await usericfile.CopyToAsync(newms);


                var mybody = @System.IO.File.ReadAllText(_environment.WebRootPath + @"\Email\emailbody-contact.html");

                mybody = mybody.Replace("Value00", Contact.Firstname + Contact.Lastname);
                mybody = mybody.Replace("Value01", Contact.Firstname + Contact.Lastname);
                mybody = mybody.Replace("Value02", Contact.Email);
                //mybody = mybody.Replace("Value03", Contact.PhoneNumber);
                mybody = mybody.Replace("Value04", Contact.Subject);
                mybody = mybody.Replace("Value05", Contact.Message);



                bodyBuilder.HtmlBody = mybody;

                var usericon = bodyBuilder.LinkedResources.Add(_environment.WebRootPath + @"/Email/newuser.png");
                usericon.ContentId = MimeUtils.GenerateMessageId();

                bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("{", "{{");
                bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("}", "}}");
                bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("{{0}}", "{0}");

                bodyBuilder.HtmlBody = string.Format(bodyBuilder.HtmlBody, usericon.ContentId);

                message.Body = bodyBuilder.ToMessageBody();


                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("ajbbuilding49@gmail.com", "Awedxzs09Am");


                client.Send(message);
                //First email


                MimeMessage message2 = new MimeMessage();

                MailboxAddress from2 = new MailboxAddress("AJ Building", "send email to ...");
                message2.From.Add(from2);

                MailboxAddress to2 = new MailboxAddress(Contact.Firstname + Contact.Lastname + " " + Contact.Subject, Contact.Email);
                message2.To.Add(to2);

                message2.Subject = "Message received";


                BodyBuilder bobu = new BodyBuilder
                {
                    HtmlBody = @System.IO.File.ReadAllText(_environment.WebRootPath + @"\Email\emailreply-contact.html")
                };




                // var logo = System.IO.File.OpenRead(_environment.WebRootPath + @"/img/logo.png");
                MemoryStream myms = new MemoryStream();
                await usericfile.CopyToAsync(myms);

                var embedlogo = bobu.LinkedResources.Add(_environment.WebRootPath + @"/img/logo.png");
                embedlogo.ContentId = MimeUtils.GenerateMessageId();

                bobu.HtmlBody = bobu.HtmlBody.Replace("{", "{{");
                bobu.HtmlBody = bobu.HtmlBody.Replace("}", "}}");
                bobu.HtmlBody = bobu.HtmlBody.Replace("{{0}}", "{0}");

                bobu.HtmlBody = string.Format(bobu.HtmlBody, embedlogo.ContentId);


                message2.Body = bobu.ToMessageBody();

                SmtpClient client2 = new SmtpClient();
                client2.Connect("smtp.gmail.com", 465, true);
                client2.Authenticate("ajbbuilding49@gmail.com", "Awedxzs09Am");


                client2.Send(message2);
                client2.Disconnect(true);
                client2.Dispose();
                ///////   End Send Email    //////////

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
        public IActionResult Contact1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(Contact? contact, string? Firstname, string? Lastname, string? Emailaddress, string? Subject, string? Message)
        {
            ViewData["Title"] = "Contact";


            AJBCContext context = new AJBCContext();
            if (Firstname != null & Lastname != null & Emailaddress != null & Subject != null & Message != null)
            {
                if (!ModelState.IsValid)
                {
                    return new RedirectResult("/Home/Index");
                }
                else
                {
                    var Contact1 = new Contact { Firstname = Firstname, Lastname = Lastname, Subject = Subject, Email = Emailaddress, Message = Message };
                    context.Contact.Add(Contact1);
                    context.SaveChanges();

                    ///////    Send Email   mail:ajbbuilding49@gmail.com pass:Awedxzs09Am  ///////
                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress("AJ Building ", "send email to ...");
                    message.From.Add(from);

                    MailboxAddress to = new MailboxAddress("AJ Building", "send email to ...");
                    message.To.Add(to);

                    message.Subject = " Contact";

                    BodyBuilder bodyBuilder = new BodyBuilder();
                    var usericfile = System.IO.File.OpenRead(_environment.WebRootPath + @"\Email\newuser.png");
                    MemoryStream newms = new MemoryStream();
                    await usericfile.CopyToAsync(newms);


                    var mybody = @System.IO.File.ReadAllText(_environment.WebRootPath + @"\Email\emailbody-contact.html");

                    mybody = mybody.Replace("Value00", Contact1.Firstname + Contact1.Lastname);
                    mybody = mybody.Replace("Value01", Contact1.Firstname + Contact1.Lastname);
                    mybody = mybody.Replace("Value02", Contact1.Email);
                    //mybody = mybody.Replace("Value03", Contact1.PhoneNumber);
                    mybody = mybody.Replace("Value04", Contact1.Subject);
                    mybody = mybody.Replace("Value05", Contact1.Message);



                    bodyBuilder.HtmlBody = mybody;

                    var usericon = bodyBuilder.LinkedResources.Add(_environment.WebRootPath + @"/Email/newuser.png");
                    usericon.ContentId = MimeUtils.GenerateMessageId();

                    bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("{", "{{");
                    bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("}", "}}");
                    bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("{{0}}", "{0}");

                    bodyBuilder.HtmlBody = string.Format(bodyBuilder.HtmlBody, usericon.ContentId);

                    message.Body = bodyBuilder.ToMessageBody();


                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("ajbbuilding49@gmail.com", "Awedxzs09Am");


                    client.Send(message);
                    //First email


                    MimeMessage message2 = new MimeMessage();

                    MailboxAddress from2 = new MailboxAddress("AJ Building", "send email to ...");
                    message2.From.Add(from2);

                    MailboxAddress to2 = new MailboxAddress(Contact1.Firstname + Contact1.Lastname + " " + Contact1.Subject, Contact1.Email);
                    message2.To.Add(to2);

                    message2.Subject = "Message received";


                    BodyBuilder bobu = new BodyBuilder
                    {
                        HtmlBody = @System.IO.File.ReadAllText(_environment.WebRootPath + @"\Email\emailreply-contact.html")
                    };




                    // var logo = System.IO.File.OpenRead(_environment.WebRootPath + @"/img/logo.png");
                    MemoryStream myms = new MemoryStream();
                    await usericfile.CopyToAsync(myms);

                    var embedlogo = bobu.LinkedResources.Add(_environment.WebRootPath + @"/img/logo.png");
                    embedlogo.ContentId = MimeUtils.GenerateMessageId();

                    bobu.HtmlBody = bobu.HtmlBody.Replace("{", "{{");
                    bobu.HtmlBody = bobu.HtmlBody.Replace("}", "}}");
                    bobu.HtmlBody = bobu.HtmlBody.Replace("{{0}}", "{0}");

                    bobu.HtmlBody = string.Format(bobu.HtmlBody, embedlogo.ContentId);


                    message2.Body = bobu.ToMessageBody();

                    SmtpClient client2 = new SmtpClient();
                    client2.Connect("smtp.gmail.com", 465, true);
                    client2.Authenticate("ajbbuilding49@gmail.com", "Awedxzs09Am");


                    client2.Send(message2);
                    client2.Disconnect(true);
                    client2.Dispose();
                    ///////   End Send Email    //////////


                    return new RedirectResult("/Home/Index");


                }

            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(!ModelState.IsValid ? contact : new Contact());
                }
                else
                {
                    context.Contact.Add(contact);
                    context.SaveChanges();
                    return View();
                }
            }
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
            Review NewReview = new Review();
            NewReview.Date = DateTime.Now;
            NewReview.Name = review.Name;
            NewReview.Message = review.Message;
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
                    NewReview.Picture = FileByte;
                }
            }
            Context.Review.Add(NewReview);
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
