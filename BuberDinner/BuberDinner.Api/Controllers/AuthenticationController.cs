using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Contracts.Authentication;
using MapsterMapper;
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
        private readonly IMapper _mapper;

        public AuthenticationController(ISender ISender, IMapper mapper)
        {
            _sender = ISender;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // 使用mapper将RegisterRequest映射为RegisterCommand
            var command = _mapper.Map<RegisterCommand>(request);
            var authResult = await _sender.Send(command);

            // 使用mapper将RegisterResult映射为AuthenticationResponse
            var response = _mapper.Map<AuthenticationResponse>(authResult);
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            // 使用mapper将LoginRequest映射为LoginQuery
            var query = _mapper.Map<LoginQuery>(request);
            var authResult = await _sender.Send(query);

            // 使用mapper将LoginResult映射为AuthenticationResponse
            var response = _mapper.Map<AuthenticationResponse>(authResult);
            return Ok(response);
        }
    }
}