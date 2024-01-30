using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Common;
using MediatR;

namespace BuberDinner.Application.Common.Behaviors
{
    public class ValidateRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(RegisterCommand request, RequestHandlerDelegate<AuthenticationResult> next, CancellationToken cancellationToken)
        {
            // before the handler
            var result = await next();
            // after the handler

            return result;
        }
    }
}