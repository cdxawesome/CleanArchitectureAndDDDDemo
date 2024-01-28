using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerate;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerate, IUserRepository userRepository)
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

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // 1. Validate the user doesn't exist
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new DuplicateEmailException();
            }

            // 2. Create user (generate unique ID) & Persist to DB
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
            _userRepository.Add(user);

            // 3. Create JWT token
            var token = _jwtTokenGenerate.GenerateToken(user);
            return new AuthenticationResult(user,
                                            token);
        }
    }
}