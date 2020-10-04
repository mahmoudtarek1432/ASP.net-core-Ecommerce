using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Services;
using AspIdentity.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AngularAcessoriesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IClientService _ClientService;
        private readonly IConfiguration _Configuration;

        public object JwtHandler { get; private set; }

        public AuthController(IClientService clientService, IConfiguration configuration)
        {
            _ClientService = clientService;
            _Configuration = configuration;
           
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserManagerResponse>> RegisterClient([FromBody]ClientRegisterDto clientRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _ClientService.RegisterClientAsync(clientRegisterDto);

                if (result.IsSuccessful)
                {
                    return Ok(result);
                }
            }

             return BadRequest("Register Properties Are Not Valid");
        
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserManagerResponse>> UserLogin([FromBody]ClientLoginDto clientLoginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _ClientService.LoginClientAsync(clientLoginDto);
                
                if (result.IsSuccessful)
                {
                    var UserInfo = await _ClientService.GetUserInfo(clientLoginDto.Email);
                    var cookieOptions = new CookieOptions()
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.Now.AddDays(30)
                    };
                    Response.Cookies.Append("LoginJwt", result.Message,cookieOptions);
                    result.Message = JsonConvert.SerializeObject(UserInfo); 
                    return Ok(result);
                }

                return BadRequest();
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpGet("Logout")]
        public async Task<ActionResult<UserManagerResponse>> UserLogout()
        {
            if(User != null) //user is logged in .. remove httponly token
            {
                
                Response.Cookies.Append("LoginJwt", "", new CookieOptions { Expires = DateTime.Now });
                return new UserManagerResponse
                {
                    IsSuccessful = true,
                    Message = "The user has been logged out successfully."
                };
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "User is not logged in."
            };
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> EmailConfirmation(string userid, string token)
        {
            if(string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await _ClientService.confirmMail(userid, token);

            if (result.IsSuccessful)
            {
                return Redirect($"{_Configuration["BaseUrl"]}/MailConfirmed.html");

            }

            return BadRequest();
        }

        //check if the token is valid
        [HttpGet("CheckAuth")]
        public async Task<ActionResult<UserManagerResponse>> CheckTokenValidity()
        {
            var cookie = Request.Cookies["LoginJwt"];
            if ( cookie == null)
            {
                return NotFound(new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "User is not Authenticated"
                });
            }
            var user = User.FindFirst("Email");
            var UserInfo = await _ClientService.GetUserInfo(user.Value);
            return Ok(new UserManagerResponse
            {
                IsSuccessful = true,
                Message = JsonConvert.SerializeObject(UserInfo)
            });
        }

        [HttpPost("ChangePassword")] //used to change password from account settings
        [Authorize]
        public async Task<ActionResult<UserManagerResponse>> ChangePassword([FromBody]ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                var result = await _ClientService.ChangePassword(userId.Value, changePasswordDto.CurrentPassword, changePasswordDto.newPassword);
                if (result.IsSuccessful)
                {
                    return Ok(result);
                }
                return (result);
            }
            return BadRequest(new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "model state is invalid"
            });
        }

        [HttpPost("EditInfo")]
        [Authorize]
        public async Task<ActionResult<UserManagerResponse>> EditUserInfo([FromBody] UserEditReadDto userEditReadDto)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                var result = await _ClientService.UpdateUserInfoAsync(userId.Value, userEditReadDto);

                if (result.IsSuccessful)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "The properties are not valid"
            };
        } 
    }
}