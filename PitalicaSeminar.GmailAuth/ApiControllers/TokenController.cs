using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PitalicaSeminar.DAL.Entities;
using PitalicaSeminar.GmailAuth.Models;
using PitalicaSeminar.GmailAuth.Tokens;

namespace PitalicaSeminar.GmailAuth.ApiControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/Token")]
    [Route("api/Token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        private readonly PitalicaDbContext _context;
        public IConfiguration Configuration { get; }

        public TokenController(PitalicaDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        [HttpPost("RequestToken")]
        public IActionResult RequestToken([FromBody] TokenRequestModel tokenRequest)
        {
            if (_context.Users.Any(c => c.UserName == tokenRequest.UserName))
            {
                JwtSecurityToken token = JwsTokenCreator.CreateToken(tokenRequest.UserName,
                    Configuration["JwtAuth:JwtSecurityKey"],
                    Configuration["JwtAuth:ValidIssuer"],
                    Configuration["JwtAuth:ValidAudience"]);
                string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenStr);
            }
            return Unauthorized();
        }

        [HttpGet("RequestTokenVersion")]
        [HttpPost("RequestTokenVersion")]
        [MapToApiVersion("1.0"), MapToApiVersion("1.1")]
        public string GetApiVersion() => HttpContext.GetRequestedApiVersion().ToString();

    }

    [Produces("application/json")]
    [ApiVersion("1.1")]
    //[Route("api/v{version:apiVersion}/Token")]
    [Route("api/Token")]
    [AllowAnonymous]
    public class TokenControllerV1_1 : Controller
    {
        private readonly PitalicaDbContext _context;
        public IConfiguration Configuration { get; }

        public TokenControllerV1_1(PitalicaDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        [HttpPost("RequestToken")]
        public IActionResult RequestToken([FromBody] TokenRequestModel tokenRequest)
        {
            if (_context.Users.Any(c => c.UserName + c.UserName == tokenRequest.DoubleName))
            {
                JwtSecurityToken token = JwsTokenCreator.CreateToken(tokenRequest.DoubleName,
                    Configuration["JwtAuth:JwtSecurityKey"],
                    Configuration["JwtAuth:ValidIssuer"],
                    Configuration["JwtAuth:ValidAudience"]);
                string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenStr);
            }
            return Unauthorized();
        }
    }
}