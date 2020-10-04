using System.Collections.Generic;
using AngularAcessoriesBack.Data;
using System.Linq;
using AngularAcessoriesBack.Models;
using AngularAcessoriesBack.Dtos;
using System;
using AngularAcessoriesBack.Services;

namespace AngularAcessoriesBack.Data
{
    public class SqlReviewRepo : IReviewRepo
    {
        private readonly DbContexts _context;
        private readonly IClientService _ClientService;

        public SqlReviewRepo(DbContexts context)
        {
            _context = context;
        }

        public IEnumerable<Review> getAllReviews(int ProductId)
        {

            return _context.Reviews.Where(r => r.ProductId == ProductId).ToList();
            
        }

        public Review getReviewById(int productId, int ReviewId)
        {
            return _context.Reviews.FirstOrDefault(r => r.ProductId == productId && r.ReviewId == ReviewId);
        }

        public void CreateProductReview(string userid, Review review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            review.UserId = userid;
            _context.Reviews.Add(review);
        }

        public void saveChanges()
        {
            _context.SaveChanges();
        }
    }
}