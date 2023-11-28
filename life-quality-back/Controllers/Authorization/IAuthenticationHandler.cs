namespace life_quality_back.Controllers.Authorization
{
    public interface IAuthenticationHandler
    {
        RespondAnswer Authenticate(string? login, string? password);
    }
}
