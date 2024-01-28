using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        // private readonly IAuthenticationCommandService _authenticationCommandService;
        // private readonly IAuthenticationQueryService _authenticationQueryService;
        private readonly ISender _sender;

        public AuthenticationController(ISender ISender)
        {
            _sender = ISender;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
            var authResult = await _sender.Send(command);

            var response = new AuthenticationResponse(
               authResult.user.Id,
               authResult.user.FirstName,
               authResult.user.LastName,
               authResult.user.Email,
               authResult.Token
            );
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authResult = await _sender.Send(new LoginQuery(request.Email, request.Password));
            var response = new AuthenticationResponse(
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.Token
            );
            return Ok(response);
        }
    }
}