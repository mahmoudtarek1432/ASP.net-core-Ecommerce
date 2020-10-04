using AngularAcessoriesBack.Models;
using AspIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Data
{
    public class SqlOrderRepo: IOrderRepo
    {
        private readonly DbContexts _context;

        public SqlOrderRepo(DbContexts context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orders>> getOrdersbyUserId(string userId)
        {
            return _context.Orders.Where(o => o.OrderClientId == userId);
        }

        public async Task<OrderDetails> getOrderDetails(int OrderId)
        {
            return _context.OrderDetails.Where(orderDetails => orderDetails.OrderId == OrderId).FirstOrDefault();
        }

        public async Task<OrderProducts> getOrderProduct(int OrderId, int ProductId)
        {
            return _context.OrderProducts.Where(o => o.OrderId == OrderId && o.ProductId == ProductId).FirstOrDefault();
        }

        public async Task<IEnumerable<OrderProducts>> getOrderProducts(int OrderId)
        {
            return _context.OrderProducts.Where(o => o.OrderId == OrderId);
        }

        public async Task<string> getOrderStatus(int orderId)
        {
            return _context.Orders.Where(od => od.OrderId == orderId)
                                  .Select(o => o.OrderStatus)
                                  .FirstOrDefault();
        }

        public async Task<UserManagerResponse> CreateOrder(Orders order, int FirstProductId)
        {
            int orderId = new Random().Next(10000, 99999);
            order.OrderId = orderId;

            Product firstproduct = _context.Products.Where(p => p.Id == FirstProductId).FirstOrDefault();
            order.FirstItemName = firstproduct.Name;
            order.FirstItemImage = firstproduct.ImagePathsArr[0];

            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "A dbexception has occured (Order table)"
                };
            }

            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = orderId+""
            }; //to use in processes
        }

        public async Task<UserManagerResponse> CreateOrder(OrderDetails orderdetails, IEnumerable<OrderProducts> orderProducts, int orderid)
        {

            orderdetails.OrderId = orderid;
            foreach (var p in orderProducts)
            {
                p.OrderId = orderid;
            }
            _context.OrderProducts.AddRange(orderProducts);
            _context.OrderDetails.Add(orderdetails);
            try
            {
                int result = await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "A dbexception has occured (OrderDetails or orderProducts table)"
                };
            }

            return new UserManagerResponse
            {
                IsSuccessful = true,
                Message = "The Order has been made Successfully"
            }; //to use in processes
        }
    }
}
