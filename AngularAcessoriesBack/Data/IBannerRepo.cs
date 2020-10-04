using AngularAcessoriesBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Data
{
    public interface IBannerRepo
    {
        IEnumerable<Banner> getOnDisplayBanners();
        void createNewBanner(Banner banner);
        void saveChanges();
    }
}
