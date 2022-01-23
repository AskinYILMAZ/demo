using demo.Business.IServices;
using demo.Model.Dto;
using demo.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace demo.api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        public TokenController(IConfiguration configuration, IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }
        /// <summary>
        /// Check api status.
        /// </summary>
        [MapToApiVersion("1.0")]
        [HttpGet, Route("alive")]
        public string Alive()
        {
            return "1.0 Alive and Running.";
        }
        /// <summary>
        /// Token Controller
        /// </summary>
        /// <response code="200">You got the jwt.</response>
        [MapToApiVersion("1.0")]
        [HttpPost("/api/v{version:apiVersion}/Token")]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult Post(UserDto model)
        {
            if (model != null && model.UserName != null && model.Password != null)
            {
                var result = new TokenDto();
                UserViewModel user = null;
                user = this.userService.AuthorizeUserAsync(new UserViewModel
                {
                    UserName = model.UserName,
                    Password = model.Password
                }).Result;
                if (user != null)
                {
                    var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user))
                        };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:JwtSecretKey"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(null, null, claims, expires: DateTime.UtcNow.AddMonths(1), signingCredentials: signIn);
                    result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    result.Expiration = token.ValidTo;
                    return Ok(result);
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }
            
        }
    }
}
