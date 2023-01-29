using ErsaCase.Service.Abstract;
using ErsaCase.Service.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Service
{
    public static class ServicesDI
    {
        public static IServiceCollection AddCustomeServices(this IServiceCollection services)
        {
            //services.AddSingleton<CurrencyMemeory>();

            //services.AddScoped<ICredentialService, CredentialService>();
            services.AddScoped<IRoleService, RoleService>();
            
            return services;
        }
    }
}
