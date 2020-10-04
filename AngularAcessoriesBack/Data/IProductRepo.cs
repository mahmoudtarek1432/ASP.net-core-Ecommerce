using AngularAcessoriesBack.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Data
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetListOfProducts(string category, int page, NameValueCollection constraints);
        IDictionary<string, List<string>> getCategoryFilters(string category);
        Product getProductById(int id);
        int getProductQuantity(int id);
        void createProduct(Product product);
        void saveChanges();
    }
}
