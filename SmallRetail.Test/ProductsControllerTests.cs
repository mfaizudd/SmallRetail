using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmallRetail.Data.Models;
using SmallRetail.Services;
using SmallRetail.Web.Controllers;
using SmallRetail.Web.Mapping;
using SmallRetail.Web.Resources;
using Xunit;

namespace SmallRetail.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public void Get_ReturnCorrectAmountofData()
        {
            // Arrange
            const int count = 5;
            var logger = A.Fake<ILogger<ProductsController>>();
            var service = A.Fake<IProductService>();
            var fakeProducts = A.CollectionOfDummy<Product>(count).AsEnumerable();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = mapperConfig.CreateMapper();
            A.CallTo(() => service.GetAll(count, 1)).Returns(fakeProducts);
            A.CallTo(() => service.Count).Returns(count);
            var controller = new ProductsController(logger, service, mapper);

            // Act
            var actionResult = controller.Get(count);

            // Assert
            var result = actionResult as OkObjectResult;
            var returnedProducts = result.Value as PagedResponse<IEnumerable<ProductResponse>>;
            Assert.Equal(count, returnedProducts.Data.Count());
        }
    }
}
