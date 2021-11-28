using System;
using System.Security.Claims;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using StreetPatch.API.Controllers;
using StreetPatch.Data;
using StreetPatch.Data.Entities;
using StreetPatch.Data.Entities.DTO;
using StreetPatch.Data.Mapping;
using StreetPatch.Data.Repositories;
using StreetPatch.Tests.MockObjects;
using Xunit;

namespace StreetPatch.Tests.Controllers
{
    public class ReportControllerCreateTests
    {
        [Fact]
        public async void Create_ReturnsCreated_WhenModelStateIsValid()
        {
            // Arrange 

            var mockUserManager = new Mock<MockUserManager>();
            mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser { Email = "test@test.dk" });

            var usersRepository = new UsersRepository(mockUserManager.Object);

            Thread.CurrentPrincipal = new TestPrincipal(new Claim("sub", "test@test.dk"));
            var options = new DbContextOptionsBuilder<StreetPatchDbContext>()
                .UseInMemoryDatabase(databaseName: "testdb")
                .Options;

            var reportsRepository = new ReportRepository(new StreetPatchDbContext(options));

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new(ClaimTypes.Name, "")
            }, "TestAuthentication"));

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ReportMapping>());

            var controller = new ReportsController(reportsRepository, usersRepository,
                mapperConfig.CreateMapper())
            {
                ControllerContext =
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            // Act
            var result = await controller.CreateAsync(new CreateReportDto());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async void Create_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange 

            var mockUserManager = new Mock<MockUserManager>();
            mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser { Email = "test@test.dk" });

            var usersRepository = new UsersRepository(mockUserManager.Object);

            Thread.CurrentPrincipal = new TestPrincipal(new Claim("sub", "test@test.dk"));
            var options = new DbContextOptionsBuilder<StreetPatchDbContext>()
                .UseInMemoryDatabase(databaseName: "testdb")
                .Options;

            var reportsRepository = new ReportRepository(new StreetPatchDbContext(options));

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new(ClaimTypes.Name, "")
            }, "TestAuthentication"));

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ReportMapping>());

            var controller = new ReportsController(reportsRepository, usersRepository,
                mapperConfig.CreateMapper())
            {
                ControllerContext =
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            controller.ModelState.AddModelError("Latitude", "Required");

            // Act
            var result = await controller.CreateAsync(new CreateReportDto());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
