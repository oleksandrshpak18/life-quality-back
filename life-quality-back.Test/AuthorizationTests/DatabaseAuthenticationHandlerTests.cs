using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using life_quality_back.Controllers.Authorization;
using life_quality_back.Data;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using FluentAssertions;

namespace life_quality_back.Test.AuthorizationTests
{
    public class DatabaseAuthenticationHandlerTests
    {
        DatabaseAuthenticationHandler handler;
        public DatabaseAuthenticationHandlerTests() 
        {
            AppDbContext context = GetDataBaseContext();

            UserRepository userRepository = new UserRepository(context);

            handler = new DatabaseAuthenticationHandler(userRepository);
        }

        private AppDbContext GetDataBaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureCreated();

            databaseContext.Users.Add(
                new User
                {
                    Login = "user1@lq.com",
                    // testpassword1
                    Password = "b7e055c6165da55c3e12c49ae5207455",
                    Doctor = new Doctor
                    {
                        FirstName = "TestUser1",
                        LastName = "TestUser1",
                        Email = "user1@lq.com",
                        Education = "TestUniversity",
                        Gender = "Female",
                        Speciality = "TestSpeciality"
                    }
                }
            );

            databaseContext.SaveChanges();
            return databaseContext;
        }

        [Fact]
        public void Authenticate_ReturnsTrue()
        {
            //Act
            RespondAnswer result = handler.Authenticate("user1@lq.com", "testpassword1");

            Assert.True(result.isOperationSuccess);
            Assert.Equal(1, result.idUser);
            Assert.Equal("Authorization complete successfully!", result.outcomeMessage);
        }

        [Fact]
        public void Authenticate_ReturnsFalse()
        {
            //Act
            RespondAnswer result = handler.Authenticate("user2@lq.com", "testpassword1");

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Incorrect login or password!", result.outcomeMessage);
        }
    }
}
