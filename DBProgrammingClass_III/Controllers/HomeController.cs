using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace DBProgrammingClass_III.Controllers
{
    public class HomeController : Controller
    {
        public NetworkCredential Credentials { get; private set; }

        public ActionResult Index()
        {
            //var client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("ahsana.tasnim@gmail.com", "Shamma123"),
            //    EnableSsl = true
            //};
            //client.Send("shammatasnim99@gmail.com", "ahsana.tasnim@gmail.com", "test", "testbody");
            //Console.WriteLine("Sent");
            //Console.ReadLine();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(string receiver, string subject, string message)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress(ConfigurationManager.AppSettings.Get("SenderEmail"), "Ahsana Tasnim");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = ConfigurationManager.AppSettings.Get("SenderEmailPassword"); 
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient {
                        Host = ConfigurationManager.AppSettings.Get("emailSMTPServer"),
                        Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("emailSMTPServerPort")),
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail) {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "";
            }
            return View();
        }
    }
}