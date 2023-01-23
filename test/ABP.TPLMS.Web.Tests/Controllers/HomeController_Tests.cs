using System.Threading.Tasks;
using ABP.TPLMS.Models.TokenAuth;
using ABP.TPLMS.Web.Controllers;
using Shouldly;
using Xunit;

namespace ABP.TPLMS.Web.Tests.Controllers
{
    public class HomeController_Tests: TPLMSWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}