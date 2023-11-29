namespace life_quality_back.Controllers.Authorization
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
}
