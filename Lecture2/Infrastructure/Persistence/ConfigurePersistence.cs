using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure.Persistence;

public static class ConfigurePersistence
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuild = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Default"));
        dataSourceBuild.EnableDynamicJson();
        var dataSource = dataSourceBuild.Build();

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(
                    dataSource,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IUserQueries>(provider => provider.GetRequiredService<UserRepository>());

        services.AddScoped<FacultyRepository>();
        services.AddScoped<IFacultyRepository>(provider => provider.GetRequiredService<FacultyRepository>());
        services.AddScoped<IFacultyQueries>(provider => provider.GetRequiredService<FacultyRepository>());
        
        services.AddScoped<CourseRepository>();
        services.AddScoped<ICourseRepository>(provider => provider.GetRequiredService<CourseRepository>());
        services.AddScoped<ICourseQueries>(provider => provider.GetRequiredService<CourseRepository>());
        
        services.AddScoped<UserCourseRepository>();
        services.AddScoped<IUserCourseRepository>(provider => provider.GetRequiredService<UserCourseRepository>());
        services.AddScoped<IUserCourseQueries>(provider => provider.GetRequiredService<UserCourseRepository>());
    }
}