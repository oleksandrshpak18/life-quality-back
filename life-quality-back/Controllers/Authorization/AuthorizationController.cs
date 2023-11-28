using life_quality_back.Controllers.Authorization;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace life_quality_back.Controllers
{   
    public class AuthenticationProcessor
    {
        private readonly List<IAuthenticationHandler> handlers;

        public AuthenticationProcessor(List<IAuthenticationHandler> handlers)
        {
            this.handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        public RespondAnswer ProcessAuthentication(string? username, string? password)
        {
            foreach (var handler in handlers)
            {
                var respond = handler.Authenticate(username, password);
                if (!respond.isOperationSuccess)
                {
                    return respond; // Якщо хоча б один обробник не пройшов, автентифікація не вдалася
                }
                else if (respond.isOperationSuccess && respond.idUser >= 0)
                {
                    return respond;
                }
            }

            return new RespondAnswer
            {
                isOperationSuccess = false,
                idUser = -1,
                outcomeMessage = "Something went wrong!"
            };
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<int>> AuthorizationDoctor(string? login, string? password)
        {
            // Створення ланцюжка обробників
            var handlers = new List<IAuthenticationHandler>
            {
                new InputValidationHandler(),
                new DatabaseAuthenticationHandler()
            };

            // Створення об'єкту для обробки автентифікації
            var processor = new AuthenticationProcessor(handlers);

            // Виклик процесу автентифікації
            var respond = processor.ProcessAuthentication(login, password);

            if (respond.isOperationSuccess)
            {
                return Ok($"Authentication successed! Id user is {respond.idUser}");
            }
            else
            {
                return BadRequest(respond.outcomeMessage);
            }
        }
    }
}
