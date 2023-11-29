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

        [Fact]
        public void Authenticate_ReturnsTrue()
        {
            //Act
            RespondAnswer result = handler.Authenticate("name.surname@lq.com", "password");

            Assert.True(result.isOperationSuccess);
        }

        [Fact]
        public void Authenticate_ReturnsSuccessMessage()
        {
            RespondAnswer result = handler.Authenticate("name.surname@lq.com", "password");

            Assert.Equal("Input validation success!", result.outcomeMessage);
        }

        [Fact]
        public void Authenticate_ReturnsSuccessId()
        {
            RespondAnswer result = handler.Authenticate("name.surname@lq.com", "password");

            Assert.Equal(-1, result.idUser);
        }

        [Fact]
        public void Authenticate_ReturnsFailedMessageEpmtyLoginString()
        {
            RespondAnswer result = handler.Authenticate("", "password");

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Login string is empty!", result.outcomeMessage);
        }

        [Fact]
        public void Authenticate_ReturnsFailedMessageEpmtyPasswordString()
        {
            RespondAnswer result = handler.Authenticate("name.surname@lq.com", "");

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Password string is empty!", result.outcomeMessage);
        }

        [Fact]
        public void Authenticate_ReturnsFailedMessageLoginWithDomain()
        {
            RespondAnswer result = handler.Authenticate("name.surnamelq.com", "password");

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Login string is incorrect! It doesn't contain '@lq'", result.outcomeMessage);
        }

        [Fact]
        public void Authenticate_ReturnsFailedMessageLoginWithManyAt()
        {
            RespondAnswer result = handler.Authenticate("name.surname@lq@gmail.com", "password");

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Login string is incorrect! It contains more than 1 '@'!", result.outcomeMessage);
        }

        [Fact]
        public void Authenticate_ReturnsFailedMessageLoginContainsNotOnlyASKII()
        {
            RespondAnswer result = handler.Authenticate("імя.прізвище@lq.com", "password");

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Login string is incorrect! It contains not only ASKII symbols!", result.outcomeMessage);
        }

        [Fact]
        public void Authenticate_ReturnsFailedMessagePasswordLenghtTooShort()
        {
            RespondAnswer result = handler.Authenticate("name.surname@lq.com", "pas");

            Assert.False(result.isOperationSuccess);
            Assert.Equal(-1, result.idUser);
            Assert.Equal("Password lenth is incorrect!", result.outcomeMessage);
        }
    }
}
