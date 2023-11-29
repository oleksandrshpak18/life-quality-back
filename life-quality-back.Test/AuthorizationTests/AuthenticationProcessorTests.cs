using life_quality_back.Controllers.Authorization;
using life_quality_back.Data;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace life_quality_back.Test.AuthorizationTests
{
    public class AuthenticationProcessorTests
    {
        AuthenticationProcessor processor;

        public AuthenticationProcessorTests()
        {
            AppDbContext context = GetDataBaseContext();

            UserRepository userRepository = new UserRepository(context);

            List<IAuthenticationHandler> handlers = new List<IAuthenticationHandler>()
            {
                new InputValidationHandler(),
                new DatabaseAuthenticationHandler(userRepository)
            };

            processor = new AuthenticationProcessor(handlers);
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
        public void ProcessAuthentication_ReturnsSuccessDoctorId()
        {
            RespondAnswer result = processor.ProcessAuthentication("user1@lq.com", "testpassword1");

            Assert.True(result.isOperationSuccess);
            Assert.Equal(1, result.idUser);
            Assert.Equal("Authorization complete successfully!", result.outcomeMessage);
        }

        //Додати більше однакових тестів
    }
}
