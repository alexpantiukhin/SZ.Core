using Al.Components.Identity.Abstractions.Interfaces;
using Al.Components.UserManager.Abstraction.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;

using SZ.Core.Abstractions.Interfaces;
using SZ.Core.Models.Db;

namespace SZ.Core
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddSZCore<TUserSession, TDbFactory>(this IServiceCollection services)
            where TUserSession : class, IUserSessionService
            where TDbFactory : class, IDbContextFactory<SZDb>
        {
            services.AddSingleton<ISZSingletonEnvironment, SingletonEnvironment>();
            services.AddSingleton<IUserManager<User, Guid, SZDb>, UserManager>();
            services.AddSingleton<IZemstvaManager, ZemstvaManager>();

            services.AddScoped<IDbContextFactory<SZDb>, TDbFactory>();

            services.AddScoped(x => { var factory = x.GetRequiredService<IDbContextFactory<SZDb>>(); return factory.CreateDbContext(); });

            services.AddScoped<IUserSessionService, TUserSession>();
            services.AddScoped<IActiveZemstvoService, ActiveZemstvoService>();

            return services;
        }
    }
}
