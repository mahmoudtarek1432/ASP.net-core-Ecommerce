using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AspIdentity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Data
{
    public class SqlCartRepo : ICartRepo
    {
        private DbContexts _Context;

        public SqlCartRepo(DbContexts Context)
        {
            _Context = Context;
        }

        public async Task addToUserCart(CartItem item)
        {
            await _Context.UserCart.AddAsync(item);
        }

        public UserManagerResponse AddToCartQuantity(CartItem cartItem, int AvailableQuantity)
        {
            var savedCartItem = _Context.UserCart.Where(cart => cart.ProductId == cartItem.ProductId && cart.UserId == cartItem.UserId).FirstOrDefault();
            if(savedCartItem == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "no dups found"
                };
            }

            savedCartItem.InCartQuantity = (savedCartItem.InCartQuantity + cartItem.InCartQuantity > AvailableQuantity) ?
                                                    AvailableQuantity : savedCartItem.InCartQuantity + cartItem.InCartQuantity;

            _Context.UserCart.Update(savedCartItem);
            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = "Cart Updated"
            };
        }

        public UserManagerResponse UpdateCartItem(int productId, string userId, int updatedQuantity, int AvailableQuantity)
        {
            var savedCartItem = _Context.UserCart.Where(cart => cart.ProductId == productId && cart.UserId == userId).FirstOrDefault();
            if (savedCartItem == null)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "no dups found"
                };
            }

            savedCartItem.InCartQuantity = (updatedQuantity > AvailableQuantity) ?
                                                    AvailableQuantity : updatedQuantity;

            _Context.UserCart.Update(savedCartItem);
            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = "Cart Updated"
            };
        }

        public IEnumerable<CartItem> getUserCart(string UserId)
        {
            return _Context.UserCart.Where(p => p.UserId == UserId);
        }

        public UserManagerResponse removeItem(string userId, int productId)
        {
            var removedProduct = _Context.UserCart.Where(C => C.UserId == userId && C.ProductId == productId).FirstOrDefault();
            if (removedProduct != null)
            {
                _Context.UserCart.Remove(removedProduct);
                saveContext();
                return new UserManagerResponse
                {
                    IsSuccessful = true,
                    Message = "The item was removed successfully"
                };
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "The item is not in the cart"
            };
        }

        public void saveContext()
        {
             _Context.SaveChanges();
        }
    }
}
