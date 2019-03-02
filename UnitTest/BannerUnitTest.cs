using System;
using Xunit;
using BannerApi.Logic;
using Microsoft.EntityFrameworkCore;
using BannerApi.Data;
using System.Linq;
using BannerApi.Data.DbModel;

namespace BannerApi.UnitTest
{
    public class BannerUnitTest
    {
        private BannerLogic _bannerLogic;

        public BannerUnitTest()
        {
            if(_bannerLogic == null)
            {
                var options = new DbContextOptionsBuilder<ApiDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDB")
                    .Options;

                var context = new ApiDbContext(options);
                if(!context.Banners.Any())
                    context.AddInitialData();
                    
                _bannerLogic = new BannerLogic(context);
            }
        }

        [Theory]
        [InlineData("<div Invalid HTML", false)]
        [InlineData("<p> Valid Html <br /> Nice</p>", true)]
        public void ValidateHtmlTest(string input, bool expectedResult)
        {
            Assert.Equal(input.ValidateHtml(), expectedResult);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(5, false)]
        public void GetTest(int id, bool shouldExist)
        {
            var banner = _bannerLogic.Get(id);
            if(shouldExist)
                Assert.NotNull(banner);
            else
                Assert.Null(banner);
        }

        [Fact]
        public void GetAllTest()
        {
            Assert.NotEmpty(_bannerLogic.GetAll());
        }

        [Theory]
        [InlineData(2, "<div> valid Html </div>")]
        [InlineData(null, "<html><body><p>Hello</p></body></html>")]
        public void SetTest(int? id, string html)
        {            
            var banner = new Banner() { Id = id ?? default(int), Html = html };
            
            var savedId = _bannerLogic.Set(banner);
            var saved = _bannerLogic.Get(savedId);
            Assert.NotNull(saved);
        }

        [Theory]
        [InlineData(1, "<invalid / html")]
        [InlineData(6, "<div> valid Html </div>")]
        public void SetThrowsExceptionTest(int? id, string html)
        {
            var banner = new Banner() { Id = id ?? default(int), Html = html };
            Assert.ThrowsAny<Exception>(() => {
                _bannerLogic.Set(banner);
            });
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeleteTest(int id)
        {
            _bannerLogic.Delete(id);
            var deleted = _bannerLogic.Get(id);
            Assert.Null(deleted);
        }

        [Theory]
        [InlineData(10)]
        public void DeleteThrowsExceptionTest(int id)
        {
            Assert.ThrowsAny<Exception>(() => {
                _bannerLogic.Delete(id);
            });
        }
    }
}
