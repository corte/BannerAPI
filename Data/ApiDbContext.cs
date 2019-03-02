using System;
using BannerApi.Data.DbModel;
using Microsoft.EntityFrameworkCore;

namespace BannerApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Banner> Banners { get; set; }
    }
}
