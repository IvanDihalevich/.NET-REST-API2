using FluentValidation;
using FluentValidation.AspNetCore;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories; 

namespace Api.Modules;

public static class SetupModule
{
    public static void SetupServices(this IServiceCollection services)
    {
        services.AddValidators();
        services.AddScoped<ICourseRepository, CourseRepository>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<Program>();
    }
}