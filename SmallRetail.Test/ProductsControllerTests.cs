using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallRetail.Data;
using SmallRetail.Data.Models;
using SmallRetail.Services;
using SmallRetail.Web.Controllers;
using SmallRetail.Web.Mapping;
using SmallRetail.Web.Resources;

namespace SmallRetail.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {
        SmallRetailDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SmallRetailDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieListDatabase")
                .Options;
            _context = new SmallRetailDbContext(options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.RemoveRange(_context.Products);
            _context.SaveChanges();
        }

        [DataTestMethod]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void Get_ReturnCorrectAmountofData(int count)
        {
            // Arrange
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
            Assert.AreEqual(count, returnedProducts.Data.Count());
        }

        [DataTestMethod]
        [DataRow(100, 10, 10)]
        [DataRow(57, 7, 9)]
        [DataRow(15, 10, 2)]
        public void Get_ReturnCorrectAmountofPage(int totalData, int dataPerPage, int expectedPageCount)
        {
            // Arrange
            var fakeProducts = Enumerable
                .Range(0, totalData)
                .Select(x => new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Price = x,
                });
            _context.AddRange(fakeProducts);
            _context.SaveChanges();
            var logger = A.Fake<ILogger<ProductsController>>();
            var service = new ProductService(_context);
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = mapperConfig.CreateMapper();
            var controller = new ProductsController(logger, service, mapper);
            controller.Url = A.Fake<IUrlHelper>();

            // Act
            var actionResult = controller.Get(dataPerPage);

            // Assert
            var result = actionResult as OkObjectResult;
            var returnedProducts = result.Value as PagedResponse<IEnumerable<ProductResponse>>;
            Assert.AreEqual(expectedPageCount, returnedProducts.TotalPages);
        }
    }
}
