using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication.Queries
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerate;
        private readonly IUserRepository _userRepository;

        public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerate, IUserRepository userRepository)
        {
            _jwtTokenGenerate = jwtTokenGenerate;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            // 1. Validate the user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                // 生产环境中不会返回这样的消息，这里作为一个测试
                throw new Exception("User with this email does not exist");
            }
            // 2. Validate the password is correct
            if (user.Password != password)
            {
                throw new Exception("Password is incorrect");
            }
            // 3. Create JWT token
            var token = _jwtTokenGenerate.GenerateToken(user);
            return new AuthenticationResult(user,
                                            token);
        }
    }
}