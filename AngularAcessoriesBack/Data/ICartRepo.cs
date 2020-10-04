using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AspIdentity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Data
{
    public interface ICartRepo
    {
        IEnumerable<CartItem> getUserCart(string UserId);

        Task addToUserCart(CartItem item);

        UserManagerResponse UpdateCartItem(int productId, string userId, int updatedQuantity, int AvailableQuantity);

        UserManagerResponse AddToCartQuantity(CartItem cartItem, int AvailableQuantity);

        UserManagerResponse removeItem(string userId, int productId);

        void saveContext();
    }
}
