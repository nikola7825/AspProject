using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Application.Commands;
using Application.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginUserCommand _loginUser;
        private readonly Encryption _enc;

        public AuthController(Encryption enc, 
                              ILoginUserCommand loginUser)
        {
            _enc = enc;
            _loginUser = loginUser;
        }


        //POST: api/Auth
        [HttpPost]
        public IActionResult Post([FromBody] LoginUser dto)
        {
            var user = _loginUser.Execute(dto);

            var stringObjekat = JsonConvert.SerializeObject(user);

            var encrypted = _enc.EncryptString(stringObjekat);

            return Ok(new { token = encrypted });
        }

        [HttpGet("decode")]
        public IActionResult Decode(string value)
        {
            var decodedString = _enc.DecryptString(value);
            decodedString = decodedString.Replace("\f", "");
            var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);

            return null;
        }

    }
}