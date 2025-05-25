using CourseNotesManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;

namespace CourseNotesManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

            services.AddAutoMapper(typeof(AssemblyReference).Assembly);

            services.AddScoped<IPasswordHasher<Teacher>, PasswordHasher<Teacher>>();

            return services;
        }
    }
}