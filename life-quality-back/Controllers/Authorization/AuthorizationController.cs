using life_quality_back.Controllers.Authorization;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using life_quality_back.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace life_quality_back.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private UserRepository _repository;

        public AuthorizationController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<RespondAnswer>> AuthorizationDoctor([FromBody]UserCredentialsVM credentials)
        {

            // Створення ланцюжка обробників
            var handlers = new List<IAuthenticationHandler>
            {
                new InputValidationHandler(),
                new DatabaseAuthenticationHandler(_repository)
            };

            // Створення об'єкту для обробки автентифікації
            var processor = new AuthenticationProcessor(handlers);

            // Виклик процесу автентифікації
            var respond = processor.ProcessAuthentication(credentials.Login, credentials.Password);

            if (respond.isOperationSuccess)
            {
                return Ok(new RespondAnswer
                {
                    idUser = respond.idUser,
                    isOperationSuccess = respond.isOperationSuccess,
                    outcomeMessage = respond.outcomeMessage
                });
            }
            else
            {
                return BadRequest(new RespondAnswer
                {
                    idUser = respond.idUser,
                    isOperationSuccess = respond.isOperationSuccess,
                    outcomeMessage = respond.outcomeMessage
                });
            }
        }
    }
}
