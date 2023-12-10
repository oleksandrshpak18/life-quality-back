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
            databaseContext.Users.Add(
                new User
                {
                    Login = "user2@lq.com",
                    // testpassword2
                    Password = "c4d8a57e2ca5dc5d71d2cf3dbbbbaabe",
                    Doctor = new Doctor
                    {
                        FirstName = "TestUser2",
                        LastName = "TestUser2",
                        Email = "user2@lq.com",
                        Education = "TestUniversity",
                        Gender = "Male",
                        Speciality = "TestSpeciality"
                    }
                }
            );
            databaseContext.Users.Add(
                new User
                {
                    Login = "user3@lq.com",
                    // testpassword3
                    Password = "cb310eace3f52787ab5fc2cddf73bd2d",
                    Doctor = new Doctor
                    {
                        FirstName = "TestUser3",
                        LastName = "TestUser3",
                        Email = "user3@lq.com",
                        Education = "TestUniversity",
                        Gender = "Male",
                        Speciality = "TestSpeciality"
                    }
                }
            );
            databaseContext.Users.Add(
                new User
                {
                    Login = "user4@lq.com",
                    // testpassword4
                    Password = "6707ab0bacfb0ed824d2cb94b3ddd258",
                    Doctor = new Doctor
                    {
                        FirstName = "TestUser4",
                        LastName = "TestUser4",
                        Email = "user4@lq.com",
                        Education = "TestUniversity",
                        Gender = "Male",
                        Speciality = "TestSpeciality"
                    }
                }
            );
            databaseContext.Users.Add(
                new User
                {
                    Login = "user5@lq.com",
                    // testpassword5
                    Password = "e9ee57647c13c700bbfb69611955cb02",
                    Doctor = new Doctor
                    {
                        FirstName = "TestUser5",
                        LastName = "TestUser5",
                        Email = "user5@lq.com",
                        Education = "TestUniversity",
                        Gender = "Male",
                        Speciality = "TestSpeciality"
                    }
                }
            );

            databaseContext.SaveChanges();
            return databaseContext;
        }

        [Theory]
        [InlineData("user1@lq.com", "testpassword1", 1)]
        [InlineData("user2@lq.com", "testpassword2", 2)]
        [InlineData("user3@lq.com", "testpassword3", 3)]
        [InlineData("user4@lq.com", "testpassword4", 4)]
        [InlineData("user5@lq.com", "testpassword5", 5)]
        public void Authenticate_ReturnsTrue(string login, string password, int expectedResult)
        {
            //Act
            RespondAnswer result = handler.Authenticate(login, password);

            Assert.True(result.isOperationSuccess);
            Assert.Equal(expectedResult, result.idUser);
            Assert.Equal("Authorization complete successfully!", result.outcomeMessage);
        }

        [Theory]
        [InlineData("user100@lq.com", "testpassword100")]
        [InlineData("user200@lq.com", "testpassword200")]
        [InlineData("user300@lq.com", "testpassword300")]
        [InlineData("user400@lq.com", "testpassword400")]
        [InlineData("user500@lq.com", "testpassword500")]
        public void Authenticate_ReturnsFalse(string login, string password)
        {
            //Act
            RespondAnswer result = handler.Authenticate(login, password);

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Incorrect login or password!", result.outcomeMessage);
        }
    }
}
