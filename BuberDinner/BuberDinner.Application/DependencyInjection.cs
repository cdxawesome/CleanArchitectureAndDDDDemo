using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Common.Behaviors;
using BuberDinner.Application.Services.Authentication.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application
{
    /*
    使用这个类来处理Application类库的DI关系。在Program.cs文件中，只需要AddApplication,
    就可以将相关的DI关系全部添加
    */
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // 这里需要添加MediatR.Extensions.Microsoft.DependencyInjection这个包才能实现依赖注入
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            services.AddScoped<IPipelineBehavior<RegisterCommand, AuthenticationResult>, ValidateRegisterCommandBehavior>();
            return services;
        }
    }
}