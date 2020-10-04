using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AngularAcessoriesBack.Data;
using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AspIdentity.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AngularAcessoriesBack.Services
{
    public class ClientService : IClientService
    {
        private readonly UserManager<CustomIdentityUser> _UserManager;
        private readonly ICartRepo _CartRepo;
        private readonly IConfiguration _Configurations;
        private readonly IEMailService _mailService;
        private readonly IMapper _mapper;
        private readonly DbContexts _DbContext;
        private readonly IProductRepo _ProductRepo;

        public object WebEncoder { get; private set; }
        public object Request { get; private set; }

        public ClientService(UserManager<CustomIdentityUser> usermanager, IEMailService mailService, ICartRepo CartService,
                            IConfiguration configurations, IHttpContextAccessor httpContextAccessor, IMapper mapper, DbContexts dbContexts)
        {
            _UserManager = usermanager;
            _CartRepo = CartService;
            _Configurations = configurations;
            _mailService = mailService;
            _mapper = mapper;
            _DbContext = dbContexts;
        }

        public async Task<UserManagerResponse> RegisterClientAsync(ClientRegisterDto model)
        {

            if(model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "Confirm Password doesn't match the password"
                };
            }

            var IdentityUser = new CustomIdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _UserManager.CreateAsync(IdentityUser, model.Password);

            if (result.Succeeded)
            {
                var confirmationToken = await _UserManager.GenerateEmailConfirmationTokenAsync(IdentityUser);
                var validWebToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));
                var url = $"{_Configurations["BaseUrl"]}/api/Auth/ConfirmEmail/?userid={IdentityUser.Id}&token={validWebToken}";
                await SendConfirmationMail(IdentityUser.Email, url);

                return new UserManagerResponse
                {
                    IsSuccessful = true,
                    Message = "The user Has Been Created Successfully",
                };
            }

            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "User didn't create",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> LoginClientAsync(ClientLoginDto model)
        {
            if(model == null) { throw new ArgumentNullException("The model is null");}

            var user = await _UserManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "There is no user with this email found",
                };
            }

            var result = await _UserManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "Invalid Password",
                };
            }

            var claims = new[]
            {
                new Claim("Email",model.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configurations["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _Configurations["AuthSettings:Issuer"],
                audience: _Configurations["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
           
           

            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = tokenAsString,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<bool> SendConfirmationMail(string Email, string url)
        {
            var SenderUser = await _UserManager.FindByEmailAsync(Email);
            string confirmationHtml = $"<h1>Please Confirm Your Mail Address</h1>" +
                                      $"<a href={url}>click here</a>";
            var confirmationMail = await _mailService.SendEMailAsync(_Configurations["SMTPEmail:Email"], _Configurations["SMTPEmail:Password"],
                                                               Email, confirmationHtml, "Email Confimation", true);

            return confirmationMail;
        }

        public async Task<UserManagerResponse> confirmMail(string userid, string token)
        {
            var user = await _UserManager.FindByIdAsync(userid);
            if(user == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "user not found"
                };
            }
            //reverse of encoding in register
            var decodeToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodeToken);

            var result = await _UserManager.ConfirmEmailAsync(user,normalToken);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = true,
                    Message = "Email Confirmed"
                };
            }

            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "Email not confirmed",
                Errors = result.Errors.Select(e=>e.Description)
            };
        }



        public async Task<UserInfoDto> GetUserInfo(string userEmail)
        {
            var user = await _UserManager.FindByEmailAsync(userEmail);

            if(user != null)
            {
                var userInfoDto = _mapper.Map<UserInfoDto>(user);
                userInfoDto.UserCart = _CartRepo.getUserCart(user.Id).ToList();
                return userInfoDto;
            }

            return null;
        }

        public async Task<UserManagerResponse> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var user = await _UserManager.FindByIdAsync(userId);
            if(user == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "Wrong credentials 'Id not found'"
                };
            }

            if(oldPassword == newPassword)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "Use a new password"
                };
            }
            
            var result = await _UserManager.ChangePasswordAsync(user,oldPassword,newPassword);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "Process Failed",
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = "Password Changed Successfully"
            };
        }

        public async Task<UserManagerResponse> UpdateUserInfoAsync(string userId, UserEditReadDto userEditDto)
        {
            var user = await _UserManager.FindByIdAsync(userId);

            if(user == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "user not found"
                };
            }

            if (!string.IsNullOrEmpty(userEditDto.FirstName))
            {
                user.FirstName = userEditDto.FirstName;
            }
            if (!string.IsNullOrEmpty(userEditDto.LastName))
            {
                user.LastName = userEditDto.LastName;
            }
            if (!string.IsNullOrEmpty(userEditDto.Address))
            {
                user.Address = userEditDto.Address;
            }
            if (!string.IsNullOrEmpty(userEditDto.City))
            {
                user.City = userEditDto.City;
            }
            if (!string.IsNullOrEmpty(userEditDto.AdditionalInfo))
            {
                user.AdditionalInfo = userEditDto.AdditionalInfo;
            }
            if (!string.IsNullOrEmpty(userEditDto.PhoneNumber))
            {
                user.PhoneNumber = userEditDto.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(userEditDto.PhoneNumber))
            {
                user.Region = userEditDto.Region;
            }

            var result = await _UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "Process Failed",
                    Errors = result.Errors.Select(e => e.Description)
                };
            }
            await _DbContext.SaveChangesAsync();
            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = "User info updated successfully"
            };
        }

       

        public async Task<UserManagerResponse> AddToRecentlyViewed(string userId, int productId)
        {
            var user = await _UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "User not found"
                };
            }
            var list = user.RecentlyViewedArr.ToList();
            list.Remove(productId.ToString()); //if the item was present remove it
            list.Add(productId.ToString());

            user.RecentlyViewedArr = list.ToArray();

            var result = await _UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "process not successful",
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = "process Successfull"
            };
        }

        public async Task<UserManagerResponse> AddToSavedItems(string userId, int productId)
        {
            var user = await _UserManager.FindByIdAsync(userId);
            List<string> list = user.SavedItemsArr.ToList();

            if (user == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "User not found"
                };
            }

            

            if(list.Any(s => s == productId.ToString()))
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "Item Is Already Saved"
                };
            }


         
            list.Add(productId.ToString());
            user.SavedItemsArr = list.ToArray();

            var result = await _UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "process not successful",
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = "process Successfull"
            };
        }

        public async Task<UserManagerResponse> RemoveSavedItem(string userId, int productId)
        {
            var user = await _UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "User not found"
                };
            }
            if(user.SavedItemsArr == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "No SavedItems were found"
                };
            }

            if (!user.SavedItemsArr.Any(s => s == productId.ToString()))
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "Item Is not saved"
                };
            }

            var list = user.SavedItemsArr.ToList();
            list.Remove(productId.ToString());
            user.SavedItemsArr = list.ToArray();

            var result = await _UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "process not successful",
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = "process Successfull"
            };
        }

        public async Task<string> AddUserName(string userid)
        {
            var user = await _UserManager.FindByIdAsync(userid);
            if (user != null)
            {
                
                return user.FirstName + " " + user.LastName;
            }
            return null;
        }
    }
}
