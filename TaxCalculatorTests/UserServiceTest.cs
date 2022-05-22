using AutoMapper;
using DataManager.Data;
using DataServices.Authorization;
using DataServices.Authorization.Middleware;
using DataServices.Authorization.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace TaxCalculatorTests
{
    public class Tests
    {
        private UserService service;
        [SetUp]
        public void Setup()
        {
            var mockConfig = new Mock<IConfiguration>();
            var context = new SqliteDataContext(mockConfig.Object);

            var mockTokenGenerator = new Mock<IJwtTokenGenerator>();

            var mockOptions = new Mock<IOptions<AppSettings>>();
            var tokenGenerator = new JwtTokenGenerator(mockOptions.Object);
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapConfig);

            service = new UserService(context, mockTokenGenerator.Object, mapper);
        }

        [Test]
        public void Register_User_Test()
        {
            Assert.Catch<Exception>(() => service.Register(new AuthRequest { Email = "test", Password = "test" }));
        }

        [Test]
        public void GET_All_Users_Test()
        {
            var users = service.GetAll().ToList();
            Assert.GreaterOrEqual(users.Count, 1);
        }

        [Test]
        public void GET_Users_By_Id_Test()
        {
            var user = service.GetById(1);
            Assert.AreEqual(user.Email, "admin@testsite.com");
        }

        [Test]
        public void GET_Failed_Authorised_User_Token_No_Secret_Test()
        {
            var request = new AuthRequest
            {
                Email = "admin@testsite.com",
                Password = "nopass",
            };

            var user = service.Authenticate(request);
            Assert.IsNull(user.Token);
        }

    }
}