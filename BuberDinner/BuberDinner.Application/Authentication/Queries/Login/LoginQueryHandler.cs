using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Entities;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerate;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerate, IUserRepository userRepository)
        {
            _jwtTokenGenerate = jwtTokenGenerate;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // 1. Validate the user exists
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                // 生产环境中不会返回这样的消息，这里作为一个测试
                throw new Exception("User with this email does not exist");
            }
            // 2. Validate the password is correct
            if (user.Password != query.Password)
            {
                throw new Exception("Password is incorrect");
            }
            // 3. Create JWT token
            var token = _jwtTokenGenerate.GenerateToken(user);
            await Task.CompletedTask;
            return new AuthenticationResult(user,
                                            token);
        }
    }
}