using System;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Services;
using Xunit;
using CM.ChampagneApp.Business.Configuration;
using Shouldly;
using System.Linq;

namespace CM.ChampagneApp.Business.IntegrationTests
{
    public class BrandDataService_Should
    {
        public BrandDataService_Should()
        {
        }

        [Fact]
        public async Task Return_All_Brands()
        {
			/*
            //Arrange
            var config = new ConfigurationProxy();
            var sut = new BrandDataService(config);

            //Act
            var brands = await sut.GetAllBrands();

            //Assert
            brands.ShouldNotBeNull();
            brands.Count().ShouldBeGreaterThan(0);
            */
        }
    }
}
