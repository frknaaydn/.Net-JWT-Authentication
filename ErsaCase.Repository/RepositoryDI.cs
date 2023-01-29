using ErsaCase.Repository.Abstract;
using ErsaCase.Repository.Concrete;
using ErsaCase.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ErsaCase.Repository
{
    public static class RepositoryDI
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            var connectionInfo = configuration.GetConnectionString("ErsaDbConnection");
            services.AddDbContext<ErsaDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("ErsaDbConnection"));
                options.EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}