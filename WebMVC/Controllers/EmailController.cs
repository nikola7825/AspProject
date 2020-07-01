using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class EmailController : Controller
    {
        private IEmailSender sender;

        public EmailController(IEmailSender sender)
        {
            this.sender = sender;
        }

        // GET: Email
        public void Get(string email)
        {
            sender.Subject = "Uspesna registracija";
            sender.ToEmail = email;
            sender.Body = "Uspesna registracija";
        }

        
    }
}