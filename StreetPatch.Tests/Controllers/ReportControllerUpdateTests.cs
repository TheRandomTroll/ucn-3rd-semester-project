using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class ReportControllerUpdateTests
    {
        [Fact]
        public async void Update_ReturnsOk_WhenEverythingIsOkayWithInput()
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

            EntityEntry<Report> entry;
            await using (var tempContext = new StreetPatchDbContext(options))
            {
                entry = await tempContext.Reports.AddAsync(new Report { Creator = await mockUserManager.Object.FindByEmailAsync("") });
                await tempContext.SaveChangesAsync();
            }

            var context = new StreetPatchDbContext(options);

            var reportsRepository = new ReportRepository(context);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new(ClaimTypes.Name, "")
            }, "TestAuthentication"));

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ReportMapping>());

            var mapper = mapperConfig.CreateMapper();
            var controller = new ReportsController(reportsRepository, usersRepository,
                mapper)
            {
                ControllerContext =
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            var report = entry.Entity;

            // Act
            var result = await controller.UpdateAsync(new UpdateReportDto { Id = report.Id.ToString() });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Update_ReturnsBadRequest_WhenInputIsNotOk()
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

            var context = new StreetPatchDbContext(options);

            var reportsRepository = new ReportRepository(context);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new(ClaimTypes.Name, "")
            }, "TestAuthentication"));

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ReportMapping>());

            var mapper = mapperConfig.CreateMapper();
            var controller = new ReportsController(reportsRepository, usersRepository,
                mapper)
            {
                ControllerContext =
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            controller.ModelState.AddModelError("Id", "Required");

            // Act
            var result = await controller.UpdateAsync(new UpdateReportDto());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Update_ReturnsNotFound_WhenIdDoesNotExist()
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

            EntityEntry<Report> entry = null;
            await using (var tempContext = new StreetPatchDbContext(options))
            {
                entry = await tempContext.Reports.AddAsync(new Report { Creator = await mockUserManager.Object.FindByEmailAsync("") });
                await tempContext.SaveChangesAsync();
            }

            var context = new StreetPatchDbContext(options);

            var reportsRepository = new ReportRepository(context);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new(ClaimTypes.Name, "")
            }, "TestAuthentication"));

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ReportMapping>());

            var mapper = mapperConfig.CreateMapper();
            var controller = new ReportsController(reportsRepository, usersRepository,
                mapper)
            {
                ControllerContext =
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            var report = entry.Entity;

            // Act
            var result = await controller.UpdateAsync(new UpdateReportDto { Id = Guid.Empty.ToString() });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
