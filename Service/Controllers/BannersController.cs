using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [HttpGet("html/{id}")]
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
        public ActionResult Post([FromBody] Banner banner)
        {
            try 
            {
                _bannerLogic.Set(banner);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Banner banner)
        {
            try 
            {
                banner.Id = id;
                _bannerLogic.Set(banner);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try 
            {
                _bannerLogic.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
