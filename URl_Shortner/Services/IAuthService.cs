using URl_Shortner.DTOs;

namespace URl_Shortner.Services
{
    public interface IAuthService
    {
        public Task<string> Register(RegisterRequest userRequest);

        public Task<string> Login(LoginRequest loginRequest);
   
    }
}
