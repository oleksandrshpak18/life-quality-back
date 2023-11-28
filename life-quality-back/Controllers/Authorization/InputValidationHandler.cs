namespace life_quality_back.Controllers.Authorization
{
    public class InputValidationHandler : IAuthenticationHandler
    {
        public RespondAnswer Authenticate(string login, string password)
        {
            // 1. Перевірка на пустоту
            RespondAnswer checkEmptyStrings = StringsCheck(login, password);
            if (!checkEmptyStrings.isOperationSuccess)
            {
                return checkEmptyStrings;
            }

            // 2. Перевірка логіна
            RespondAnswer checkLogin = LoginCheck(login);
            if (!checkLogin.isOperationSuccess)
            {
                return checkLogin;
            }

            // 3. Перевірка пароля
            RespondAnswer checkPassword = PasswordCheck(password);
            if (!checkPassword.isOperationSuccess)
            {
                return checkPassword;
            }

            // Перевірка валідації пройдена
            return new RespondAnswer
            {
                isOperationSuccess = true,
                idUser = -1,
                outcomeMessage = "Input validation success!"
            };
        }

        private RespondAnswer StringsCheck(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
            {
                return new RespondAnswer
                {
                    isOperationSuccess = false,
                    idUser = -1,
                    outcomeMessage = "Login string is empty!"
                };
            }
            else if (string.IsNullOrEmpty(password))
            {
                return new RespondAnswer
                {
                    isOperationSuccess = false,
                    idUser = -1,
                    outcomeMessage = "Password string is empty!"
                };
            }

            return new RespondAnswer
            {
                isOperationSuccess = true,
                idUser = -1,
                outcomeMessage = ""
            };
        }

        private RespondAnswer LoginCheck(string login)
        {
            if (!login.Contains("@lq"))
            {
                return new RespondAnswer
                {
                    isOperationSuccess = false,
                    idUser = -1,
                    outcomeMessage = "Login string is incorrect!"
                };
            }
            else if (login.IndexOf('@') != login.LastIndexOf('@'))
            {
                return new RespondAnswer
                {
                    isOperationSuccess = false,
                    idUser = -1,
                    outcomeMessage = "Login string contains more than 1 '@'!"
                };
            }
            else if (!StringContainsASKIISymbols(login)) 
            {
                return new RespondAnswer
                {
                    isOperationSuccess = false,
                    idUser = -1,
                    outcomeMessage = "Login string must contains only ASKII symbols!"
                };
            }

            return new RespondAnswer
            {
                isOperationSuccess = true,
                idUser = -1,
                outcomeMessage = ""
            };
        }
        
        private RespondAnswer PasswordCheck(string password)
        {
            if (password.Length <= 4)
            {
                return new RespondAnswer
                {
                    isOperationSuccess = false,
                    idUser = -1,
                    outcomeMessage = "Password lenth is incorrect!"
                };
            }

            return new RespondAnswer
            {
                isOperationSuccess = true,
                idUser = -1,
                outcomeMessage = ""
            };
        }

        private bool StringContainsASKIISymbols(string line)
        {
            // Якщо символ не є друкованим ASCII символом, повертаємо false.
            bool isASKII = true;

            foreach (char c in line)
            {
                if ((int)c < 32 || (int)c > 127)
                {
                    isASKII = false;
                    break;
                }
            }

            return isASKII;
        }
    }
}
