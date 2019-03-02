using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BannerApi.Data;
using BannerApi.Data.DbModel;
using BannerApi.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BannerApi.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerLogic _bannerLogic;

        public BannersController(IBannerLogic bannerLogic)
        {
            _bannerLogic = bannerLogic;
        }

        public ActionResult<string> GetBannerHtml(int id) 
        {
            var bannerHtml = _bannerLogic.Get(id)?.Html;
            if (bannerHtml == null)
                return NotFound();
            return Ok(bannerHtml);
        }

        [HttpGet]
        public ActionResult<List<Banner>> Get() => Ok(_bannerLogic.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Banner> Get(int id)
        {
            var banner = _bannerLogic.Get(id);
            if (banner == null)
                return NotFound();
            return Ok(banner);
        }

        [HttpPost]
        public void Post([FromBody] Banner banner)
        {
            _bannerLogic.Set(banner);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Banner banner)
        {
            banner.Id = id;
            _bannerLogic.Set(banner);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bannerLogic.Delete(id);
        }
    }
}
