using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace BookStoreAPI.Controllers
{
    /* this controller allows to authenticate the user before accessing the data */
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<User> roleManager;
        private readonly IConfiguration _configuration;


        public AuthenticationController( UserManager<User> userManager, RoleManager<User> roleManager, IConfiguration configuration )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            var userExist = await userManager.FindByNameAsync(model.Username);
            if( userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User ALready Exists" });
            }
            
            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Creation Failed" });
            }

            return Ok(new Response { Status = "Success", Message = "User Created Successfully" })  ;


        }

        
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login( [FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if ( user != null && await userManager.CheckPasswordAsync(user,model.Password)  )
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach ( var userRole in userRoles )
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256)
                );

                return Ok( new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        return Unauthorized();

        }
    }
}