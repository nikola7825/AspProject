using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IEmailSender sender;

        public EmailController(IEmailSender sender)
        {
            this.sender = sender;
        }

        // GET: api/Email
        [HttpGet]
        public void Get(string email)
        {
            sender.Subject = "Uspesna registracija";
            sender.ToEmail = email;
            sender.Body = "Uspesna registracija";
        }

        // GET: api/Email/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Email
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Email/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
