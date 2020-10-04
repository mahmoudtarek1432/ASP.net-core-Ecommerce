using AngularAcessoriesBack.Data;
using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AngularAcessoriesBack.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class productsController : ControllerBase
    {
        private readonly IProductRepo _ProductRepo;
        private readonly IReviewRepo _ReviewRepo;
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public productsController(IProductRepo productRepo, IReviewRepo reviewRepo, IMapper mapper, IClientService clientService)
        {
            _ProductRepo = productRepo;
            _ReviewRepo = reviewRepo;
            _mapper = mapper;
            _clientService = clientService;
        }

        //http://localhost:58517/api/products/op/work/2/?filters=Id;1 
        [HttpGet("{orderBy}/{category}/{page}")]
        public ActionResult<IEnumerable<ProductReadDto>> GetListProducts(string orderBy, string category, int page, string filters)
        {
            NameValueCollection filtersAssoc = new NameValueCollection();
            if (filters != null)
            {
                filtersAssoc = arrayToAssociativeArray(filters.Split(";"));
            }
            //to be used... operation can be retrive or search..
            var p = FillProductReviewsList(_ProductRepo.GetListOfProducts(category, page, filtersAssoc).ToList());
            if (p == null || p.Count == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(p));
        }

        [HttpGet("CategoryFilters/{category}")] //get the filters of a category
        public ActionResult<Dictionary<string,List<string>>> getListFilters(string category)
        {
            if (ModelState.IsValid)
            {
                var filters = _ProductRepo.getCategoryFilters(category);
                if(filters == null || filters.Count ==0)
                {
                    return NotFound();
                }
                return Ok(filters);
            }
            return BadRequest();
        }


        [HttpGet("{id}",Name = "getProductById")]
        public async Task<ActionResult<ProductReadDto>> getProductById(int id)
        {
            var p = FillProductReviews(_ProductRepo.getProductById(id));

            if (p != null)
            {
                return Ok(_mapper.Map<ProductReadDto>(p));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<ProductReadDto> createNewProduct(ProductCreateDto newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);
            _ProductRepo.createProduct(product);
            _ProductRepo.saveChanges();

            var productReadDto = _mapper.Map<ProductReadDto>(product);

            return CreatedAtRoute(nameof(getProductById), new { id = product.Id }, productReadDto);
        }

        [HttpGet("productQuantity/{productId}")]
        public async Task<int> getProductAvailable(int productId)
        {
            return _ProductRepo.getProductById(productId).QuantityAvailable;
        }

        public List<Product> FillProductReviewsList(List<Product> products) //fills product reviews list
        {
            if (products == null) { return null; }

            var temp = products;
            temp.ForEach(p => p.Reviews = _ReviewRepo.getAllReviews(p.Id).ToList());
            return temp;
        }

        public Product FillProductReviews(Product products) //fills product reviews object
        {
            if (products == null) { return null; }

            var temp = products;
            temp.Reviews = _ReviewRepo.getAllReviews(temp.Id).ToList();
            return temp;
        }

        public NameValueCollection arrayToAssociativeArray(string[] array) //used to change an itrative array of key and value to accosiative array
        {
            NameValueCollection assocArray = new NameValueCollection();
            if (array.Length%2 == 0) //is odd
            {
                for (int i = 0; i < array.Length; i = i + 2)
                {
                    assocArray[array[i]] = array[i + 1];
                }
            }
            return assocArray;
        }
    }
}


