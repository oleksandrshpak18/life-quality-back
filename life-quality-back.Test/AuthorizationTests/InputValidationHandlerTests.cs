using life_quality_back.Controllers.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace life_quality_back.Test.AuthorizationTests
{
    public class InputValidationHandlerTests
    {
        InputValidationHandler handler = new InputValidationHandler();

        [Theory]
        [InlineData("name.surname@lq.com", "password")]
        [InlineData("n.s@lq.com", "password")]
        [InlineData("name@lq.c", "passw")]
        [InlineData("name2.surname@lq.com", "password")]
        [InlineData("name3.surnamesurnamesurnamesurnamesurnamesurname@lq.com", "passwordpasswordpasswordpasswordpasswordpassword")]
        public void Authenticate_ReturnsTrue(string login, string password)
        {
            //Act
            RespondAnswer result = handler.Authenticate(login, password);

            Assert.True(result.isOperationSuccess);
            Assert.Equal("Input validation success!", result.outcomeMessage);
            Assert.Equal(-1, result.idUser);
        }

        [Theory]
        [InlineData("", "password")]
        [InlineData(null, "password")]
        [InlineData("", "")]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void Authenticate_ReturnsFailedMessageEpmtyLoginString(string login, string password)
        {
            RespondAnswer result = handler.Authenticate(login, password);

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Login string is empty!", result.outcomeMessage);
        }

        [Theory]
        [InlineData("name.surname@lq.com", "")]
        [InlineData("name.surname@lq.com", null)]

        public void Authenticate_ReturnsFailedMessageEpmtyPasswordString(string login, string password)
        {
            RespondAnswer result = handler.Authenticate(login, password);

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Password string is empty!", result.outcomeMessage);
        }

        [Theory]
        [InlineData("name.surname.com", "password")]
        [InlineData("name.surname@gmail.com", "password")]
        [InlineData("name.surname.lq.com", "password")]
        [InlineData("name.surname@llq.com", "password")]
        [InlineData("name.surname@lq", "password")]
        [InlineData("name.surname@lqlq.com", "password")]

        public void Authenticate_ReturnsFailedMessageLoginWithDomain(string login, string password)
        {
            RespondAnswer result = handler.Authenticate(login, password);

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Login string is incorrect! It doesn't contain '@lq'", result.outcomeMessage);
        }

        [Theory]
        [InlineData("name.surname@lq@lq.com", "password")]
        [InlineData("name.surname@lq.@gmail.com", "password")]
        [InlineData("name.surname@gmail@lq.com", "password")]
        [InlineData("n@me.surname@lq.com", "password")]
        [InlineData("name.surname@@lq.com", "password")]
        public void Authenticate_ReturnsFailedMessageLoginWithManyAt(string login, string password)
        {
            RespondAnswer result = handler.Authenticate(login, password);

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Login string is incorrect! It contains more than 1 '@'!", result.outcomeMessage);
        }

        [Theory]
        [InlineData("імя.прізвище@lq.com", "password")]
        [InlineData("ስም.የአያትስም@lq.com", "password")]
        [InlineData("اسماللقب@lq.com", "password")]
        [InlineData("নাম.উতি@lq.com", "password")]
        [InlineData("tɔgɔ.tɔgɔ@lq.com", "password")]
        public void Authenticate_ReturnsFailedMessageLoginContainsNotOnlyASKII(string login, string password)
        {
            RespondAnswer result = handler.Authenticate(login, password);

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Login string is incorrect! It contains not only ASKII symbols!", result.outcomeMessage);
        }

        [Theory]
        [InlineData("name.surname@lq.com", "p")]
        [InlineData("name.surname@lq.com", "pa")]
        [InlineData("name.surname@lq.com", "pas")]
        [InlineData("name.surname@lq.com", "pass")]
        public void Authenticate_ReturnsFailedMessagePasswordLenghtTooShort(string login, string password)
        {
            RespondAnswer result = handler.Authenticate(login, password);

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Password lenth is incorrect!", result.outcomeMessage);
        }
    }
}
