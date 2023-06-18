using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FrogExebitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthTestController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(401)]
        public string Get()
        {
            return "you hit me Admin!";
        }
    }
}
