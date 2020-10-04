using System.Collections.Generic;
using AngularAcessoriesBack.Models;
using Microsoft.EntityFrameworkCore;
using AngularAcessoriesBack.Data;
using System.Linq;
using System;

namespace AngularAcessoriesBack.Data
{
    public class SqlBannerRepo : IBannerRepo
    {
        public readonly DbContexts _context;
        public SqlBannerRepo(DbContexts context)
        {
            _context = context;
        }

        public void createNewBanner(Banner banner)
        {
            if(banner == null)
            {
                throw new ArgumentNullException(nameof(banner));
            }
            _context.Banners.Add(banner);
        }


        public IEnumerable<Banner> getOnDisplayBanners()
        {
            return _context.Banners.Where(b => b.OnDisplay == true).ToList();
        }

        public void saveChanges()
        {
            _context.SaveChanges();
        }
    }
}