using Api_Properties.Interfaces;
using Api_Properties.Models;
using Api_Properties.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_Properties.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AuthenticateController : Controller
    {        
        private readonly IDatabaseService _database;
        public AuthenticateController(IDatabaseService database)
        {            
            _database = database;
        }
        
        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateToken(OwnerName userCredentials)
        {
            try
            {
                bool validate = await _database.ExistsOwnerName(userCredentials.nameOwner);
                if (!validate)
                    return Unauthorized();

                var token = _database.GenerateJwtToken(userCredentials.nameOwner);

                return Ok(new { token });
            }
            catch (Exception ex) 
            { 
                return HttpResponseHelper.Response(StatusCodes.Status500InternalServerError, new ResponseResult() { IsError = true, Message = ex.Message });
            }            
        }
    }
}
