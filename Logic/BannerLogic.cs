using System;
using System.Collections.Generic;
using System.Linq;
using BannerApi.Data;
using BannerApi.Data.DbModel;

namespace BannerApi.Logic
{
    public class BannerLogic : IBannerLogic
    {
        private readonly ApiDbContext _context;

        public BannerLogic(ApiDbContext context)
        {
            _context = context;
        }

        public List<Banner> GetAll() => _context.Banners.ToList();

        public Banner Get(int id) => _context.Banners.FirstOrDefault(banner => banner.Id == id);

        public void Set(Banner banner)
        {
            if (!banner.Html.ValidateHtml())
                throw new Exception("Invalid html!");

            if (banner.Id > 0)
            {
                Update(banner);
            }
            else
            {
                Insert(banner);
            }
        }

        public void Delete(int id)
        {
            var entry = _context.Banners.FirstOrDefault(b => b.Id == id);
            if (entry == null)
                throw new KeyNotFoundException($"Banner with id '{id}' not found");
            
            _context.Banners.Remove(entry);
            _context.SaveChanges();
        }

        private void Update(Banner banner)
        {
            var entry = _context.Banners.FirstOrDefault(b => b.Id == banner.Id);
            if (entry == null)
                throw new KeyNotFoundException($"Banner with id '{banner.Id}' not found");

            entry.Html = banner.Html;
            entry.Modified = DateTime.Now;

            _context.Banners.Update(entry);
            _context.SaveChanges();
        }

        private void Insert(Banner banner)
        {
            var lastId = _context.Banners.Max(p => p.Id);
            banner.Id = lastId + 1;
            banner.Created = DateTime.Now;

            _context.Banners.Add(banner);
            _context.SaveChanges();
        }
    }
}
