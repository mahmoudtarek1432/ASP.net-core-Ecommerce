using AngularAcessoriesBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Data
{
    public interface IReviewRepo
    {
        //product id is used as a key for a subset of reviews
        IEnumerable<Review> getAllReviews(int ProductId);
        Review getReviewById(int productId, int ReviewId);
        void CreateProductReview(string UserId,Review review);
        void saveChanges();
    }
}
