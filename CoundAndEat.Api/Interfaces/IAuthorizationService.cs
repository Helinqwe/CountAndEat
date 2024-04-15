using CoundAndEat.Dtos.Auth;

namespace CoundAndEat.Api.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<LoginResponse> Login(LoginModel model);
        public Task<Status> Registration(RegistrationModel model);
        public Task<LoginResponse> Login(string email, string pass);
    }
}
