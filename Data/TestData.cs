using System;
using BannerApi.Data.DbModel;

namespace BannerApi.Data
{
    public static class TestData
    {
        public static void AddInitialData(this ApiDbContext context)
        {
            context.Banners.Add(new Banner {
                Id = 1,
                Html = "<div>This is a banner</div>",
                Created = DateTime.Now
            });

            context.Banners.Add(new Banner {
                Id = 2,
                Html = "<div>This is another banner</div>",
                Created = DateTime.Now
            });

            context.Banners.Add(new Banner {
                Id = 3,
                Html = "<div class='banner'>This is a better banner</div>",
                Created = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}