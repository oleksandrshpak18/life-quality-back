using life_quality_back.Controllers.Authorization;
using life_quality_back.Data;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace life_quality_back.Test.AuthorizationTests
{
    public class AuthenticationProcessorTests
    {
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
        public void ProcessAuthentication_ReturnsSuccessDoctorId(string login, string password, int expectedId)
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContext();
            UserRepository userRepository = new UserRepository(context);
            List<IAuthenticationHandler> handlers = new List<IAuthenticationHandler>()
            {
                new InputValidationHandler(),
                new DatabaseAuthenticationHandler(userRepository)
            };
            AuthenticationProcessor processor = new AuthenticationProcessor(handlers);

            //ACT
            RespondAnswer result = processor.ProcessAuthentication(login, password);

            //ASSERT
            Assert.True(result.isOperationSuccess);
            Assert.Equal(expectedId, result.idUser);
            Assert.Equal("Authorization complete successfully!", result.outcomeMessage);
        }

        [Theory]
        [InlineData("user100@lq.com", "testpassword100", -1)]
        [InlineData("user200@lq.com", "testpassword200", -1)]
        [InlineData("user300@lq.com", "testpassword300", -1)]
        [InlineData("user400@lq.com", "testpassword400", -1)]
        [InlineData("user500@lq.com", "testpassword500", -1)]
        public void ProcessAuthentication_ReturnsFalseDoctorId(string login, string password, int expectedId)
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContext();
            UserRepository userRepository = new UserRepository(context);
            List<IAuthenticationHandler> handlers = new List<IAuthenticationHandler>()
            {
                new InputValidationHandler(),
                new DatabaseAuthenticationHandler(userRepository)
            };
            AuthenticationProcessor processor = new AuthenticationProcessor(handlers);

            //ACT
            RespondAnswer result = processor.ProcessAuthentication(login, password);

            //ASSERT
            Assert.False(result.isOperationSuccess);
            Assert.Equal(expectedId, result.idUser);
            Assert.Equal("Incorrect login or password!", result.outcomeMessage);
        }

        [Fact]
        public void ProcessAuthentication_ThrowArgumentNullException_ZeroHanderls()
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContext();
            UserRepository userRepository = new UserRepository(context);

            //ACT + ASSERT
            Assert.Throws<ArgumentNullException>(() => new AuthenticationProcessor(new List<IAuthenticationHandler>(){ }));
        }

        [Fact]
        public void ProcessAuthentication_ThrowArgumentNullException_NullHanderls()
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContext();
            UserRepository userRepository = new UserRepository(context);

            //ACT + ASSERT
            Assert.Throws<ArgumentNullException>(() => new AuthenticationProcessor(null));
        }

        [Theory]
        [InlineData("", "password", "Login string is empty!")]
        [InlineData("name.surname@lq.com", "", "Password string is empty!")]
        [InlineData("name.surname.com", "password", "Login string is incorrect! It doesn't contain '@lq'")]
        [InlineData("name.surname@lq@lq.com", "password", "Login string is incorrect! It contains more than 1 '@'!")]
        [InlineData("імя.прізвище@lq.com", "password", "Login string is incorrect! It contains not only ASKII symbols!")]
        [InlineData("name.surname@lq.com", "p", "Password lenth is incorrect!")]
        public void ProcessAuthentication_ReturnValidationErrors(string login, string password, string expectedOutcomeMessage)
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContext();
            UserRepository userRepository = new UserRepository(context);
            List<IAuthenticationHandler> handlers = new List<IAuthenticationHandler>()
            {
                new InputValidationHandler(),
                new DatabaseAuthenticationHandler(userRepository)
            };
            AuthenticationProcessor processor = new AuthenticationProcessor(handlers);

            //ACT
            RespondAnswer result = processor.ProcessAuthentication(login, password);

            //ASSERT
            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal(expectedOutcomeMessage, result.outcomeMessage);

        }
    }
}
