using life_quality_back.Data.Repositories;
using System.Text;

namespace life_quality_back.Controllers.Authorization
{
    public class DatabaseAuthenticationHandler : IAuthenticationHandler
    {
        private UserRepository _repository;

        public DatabaseAuthenticationHandler(UserRepository repository)
        {
            _repository = repository;
        }
        public RespondAnswer Authenticate(string? login, string? password)
        {
            //Хешуємо пароль
            var hashPassword = HashPassword(Encoding.UTF8.GetBytes(password ?? ""));

            //Отримуємо усі id наших user в системі
            var usersId = GetAllUsersId(_repository);

            foreach (var id in  usersId)
            {
                // Перевірка користувача в базі даних
                if (string.Equals(_repository.GetAuthorizationDataById(id).Item1, login) && string.Equals(_repository.GetAuthorizationDataById(id).Item2, hashPassword))
                {

                    return new RespondAnswer
                    {
                        isOperationSuccess = true,
                        idUser = _repository.GetDoctorIdById(id),
                        outcomeMessage = "Authorization complete successfully!"
                    };
                }
            }

            return new RespondAnswer
            {
                isOperationSuccess = false,
                idUser = -1,
                outcomeMessage = "Incorrect login or password!"
            };
        }

        private List<int> GetAllUsersId(UserRepository repository)
        {
            var users = repository.GetAll();

            return users.Select(user => user.UserId).ToList();
        }

        private string HashPassword(byte[] password)
        {
            var md5Result = MD5Algorythm.Algorithm(password);

            var result = MD5Algorythm.MakeStringFromArrUInt(md5Result);

            return result;
        }
    }
}
