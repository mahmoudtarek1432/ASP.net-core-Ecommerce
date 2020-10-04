using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AspIdentity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AngularAcessoriesBack.Services
{
    public interface IClientService
    {

        Task<UserManagerResponse> RegisterClientAsync(ClientRegisterDto clientRegDto);

        Task<UserManagerResponse> LoginClientAsync(ClientLoginDto clientLoginDto);

        Task<bool> SendConfirmationMail(string Email, string url);

        Task<UserManagerResponse> confirmMail(string userid, string token);

        Task<UserInfoDto> GetUserInfo(string userId);

        Task<UserManagerResponse> ChangePassword(string userId, string oldPassword, string newPassword);

        Task<UserManagerResponse> UpdateUserInfoAsync(string userId, UserEditReadDto userEditDto);

        Task<UserManagerResponse> AddToRecentlyViewed(string userId, int productId);

        Task<UserManagerResponse> AddToSavedItems(string userId, int productId);

        Task<UserManagerResponse> RemoveSavedItem(string userId, int productId);

        Task<string> AddUserName(string userid);
    }

}


