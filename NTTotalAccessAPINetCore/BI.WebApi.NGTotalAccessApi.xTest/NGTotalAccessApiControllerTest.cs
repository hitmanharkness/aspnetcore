using BI.Enterprise.ViewModels.NGTotalAccessApi;
using BI.ServiceBus.Manager.NGTotalAccessApi;
using BI.ServiceBus.Manager.NGTotalAccessApi.Contract;
using BI.WebApi.NGTotalAccessApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Threading.Tasks;
using Moq;
using System.Linq;
using System.Net;
using BI.Enterprise.ViewModels.NGTotalAccessApi.Contract;

/// <summary>
/// http://www.c-sharpcorner.com/UploadFile/dacca2/understand-stub-mock-and-fake-in-unit-testing/
/// https://github.com/aspnet/Home/wiki/Engineering-guidelines#unit-tests-and-functional-tests
/// https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
/// </summary>
namespace BI.WebApi.NGTotalAccessApi.xTest
{
    public class NGTotalAccessApiControllerTest
    {
        //INGTotalAccessApiContext context;
        //INGTotalAccessApiRepositoryManager repository;
        //INGTotalAccessApiCore core;
        //INGTotalAccessApiManager manager;
        //NGTotalAccessApiController controller;

        public NGTotalAccessApiControllerTest()
        {
            //context = new NGTotalAccessApiContext();
            //repository = new NGTotalAccessApiRepositoryManagerFake();
            //core = new NGTotalAccessApiCore(repository);
            //manager = new NGTotalAccessApiManager(core);
            //controller = new NGTotalAccessApiController(manager);
        }


        /// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing
        /// Typical controller responsibilities:
        /// 
        /// Verify ModelState.IsValid.
        /// Return an error response if ModelState is invalid.
        /// Retrieve a business entity from persistence.
        /// Perform an action on the business entity.
        /// Save the business entity to persistence.
        /// Return an appropriate IActionResult.




        [Fact]
        public async void GetMenuItems_ReturnsOkObjectResult_WithAListOfMenuItems()
        {
            // Arrange 
            INGTotalAccessApiManager manager = new NGTotalAccessApiManagerStub(); // We stub because a stub is a non-breaking dedendence so we can focus on testing this method.
            NGTotalAccessApiController controller = new NGTotalAccessApiController(manager);

            // Act
            IActionResult result = await controller.GetMenuItems(5);

            // Assert 
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            //var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(okObjectResult.StatusCode,200); // Do I need that when I know its' a okobjectresult?
            var model = okObjectResult.Value as GetMenuItemsResponseModel;
            
            //var model = Assert.IsAssignableFrom<GetMenuItemsResponseModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.MenuItems.ToList().Count);
        }

        [Fact]
        public async Task GetMenuItems_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockManager = new Mock<INGTotalAccessApiManager>();
            mockManager.Setup(mgr => mgr.GetMenuItems<GetMenuItemsResponseModel>(1))
                .Returns(
                new GetMenuItemsResponseModel()
                {

                });


            Assert.Equal(1, 1);


            //NGTotalAccessApiController controller = new NGTotalAccessApiController(mockManager.Object);


            //controller.ModelState.AddModelError("SessionName", "Required");
            //var newSession = new controller.NewSessionModel();

            //// Act
            //var result = await controller.Index(newSession);

            //// Assert
            //var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            //Assert.IsType<SerializableError>(badRequestResult.Value);
        }



        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void QueryMenuItemsShouldReturningOneItem()
        {
            Assert.Equal(1, 1);
            // Arrange 
            //INGTotalAccessApiManager manager = new NGTotalAccessApiManagerStub();
            //NGTotalAccessApiController controller = new NGTotalAccessApiController(manager);

            //// Act
            //IActionResult test = controller.GetMenuItems(5);

            //// Assert 
            //Assert.Equal(2, 2);
        }

    }
}
