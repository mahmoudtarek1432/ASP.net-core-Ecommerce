using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using AngularAcessoriesBack.Data;
using AngularAcessoriesBack.Models;

namespace AngularAcessoriesBack.Data
{
    public class SqlProductRepo : IProductRepo
    {
        private readonly DbContexts _context;
        public SqlProductRepo(DbContexts context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Where(p => p.OnDisplay == false); //return only the products that may be viewed
        }

        public Product getProductById(int id)
        {
            var test = _context.Products.FirstOrDefault(p => p.Id == id/* && p.OnDisplay == true*/);
            return test;
        }

        public void createProduct(Product NewProduct)
        {
            if( NewProduct == null)
            {
                throw new ArgumentNullException(nameof(NewProduct));
            }

             _context.Products.Add(NewProduct);
        }

        public void saveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetListOfProducts(string category, int page, NameValueCollection filters)
        {
            int listSize = 15;

            List<int> productsFilters = (!string.IsNullOrEmpty(category) || !(filters == null)) ? _context.ProductFilters.Where(Filter => Filter.Category == "CATEGORY" && Filter.Value == category.ToUpper())
                                                        .Select(p => p.productId)
                                                        .ToList() :
                                                        _context.ProductFilters.Where(Filter => Filter.Category == "CATEGORY")
                                                        .Select(p => p.productId)
                                                        .ToList();
            if (!(filters == null))
            {
                listSize = (filters["listSize"] != null) ? int.Parse(filters["listSize"]) : 15;
                foreach (string item in filters)
                {
                    var test = filters[item];
                    var filterItems = _context.ProductFilters.Where(PF => PF.Category == item.ToUpper() && PF.Value == filters[item].ToUpper()).Select(PF => PF.productId);
                    productsFilters = productsFilters.Intersect(filterItems).ToList();
                }
            }
            List<Product> products = new List<Product>();
            List<Product> temp = GetAllProducts().ToList();
            temp.ForEach(p => {
                if (productsFilters.Contains(p.Id)){
                    products.Add(p);
                }
            });

            return  products.Skip((page-1) * listSize)
                            .Take(listSize);
        }

        public IDictionary<string, List<string>> getCategoryFilters(string category)
        {
            List<int> productsFilters = _context.ProductFilters.Where(Filter => Filter.Category == "CATEGORY" && Filter.Value == category.ToUpper())
                                                               .Select(p => p.productId)
                                                               .ToList();

            Dictionary<string, List<string>> FiltersAndValues = new Dictionary<string, List<string>>();
            productsFilters.ForEach(pid =>
            {
                var list = _context.ProductFilters.Where(pf => pf.productId == pid && pf.Category != "CATEGORY").ToList();
                list.ForEach(pf =>
                {
                    if (!FiltersAndValues.ContainsKey(pf.Category))
                    {
                        FiltersAndValues.Add(pf.Category, new List<string>());
                    }
                    if (!FiltersAndValues[pf.Category].Contains(pf.Value))
                    {
                        FiltersAndValues[pf.Category].Add(pf.Value);
                    }
                    
                });
            });
            return FiltersAndValues;
        }

        public int getProductQuantity(int id)
        {
            return _context.Products.Where(p => p.Id == id)
                             .Select(p => p.QuantityAvailable)
                             .FirstOrDefault();
        }
    }
}
