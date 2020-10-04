using AngularAcessoriesBack.Models;
using AspIdentity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Data
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Orders>> getOrdersbyUserId(string userId);

        Task<OrderDetails> getOrderDetails(int OrderId);

        Task<IEnumerable<OrderProducts>> getOrderProducts(int OrderId);

        Task<OrderProducts> getOrderProduct(int OrderId, int ProductId);

        Task<string> getOrderStatus(int orderId);

        Task<UserManagerResponse> CreateOrder(Orders order,int FirstProductId);

        Task<UserManagerResponse> CreateOrder(OrderDetails orderDetails, IEnumerable<OrderProducts> orderProducts, int orderid);
    }
}
