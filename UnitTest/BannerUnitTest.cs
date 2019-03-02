using System;
using Xunit;
using BannerApi.Logic;

namespace BannerApi.UnitTest
{
    public class BannerUnitTest
    {
        [Theory]
        [InlineData("<div Invalid HTML", false)]
        [InlineData("<p> Valid Html <br /> Nice</p>", true)]
        public void ValidateHtmlTest(string input, bool expectedResult)
        {
            Assert.Equal(input.ValidateHtml(), expectedResult);
        }
    }
}
