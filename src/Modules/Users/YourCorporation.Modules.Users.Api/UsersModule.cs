﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Modules.Users.Api.Database;
using YourCorporation.Modules.Users.Api.Features;
using YourCorporation.Modules.Users.Api.Services;
using YourCorporation.Shared.Abstractions.Modules;

namespace YourCorporation.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public const string BasePath = "users-module";

        public string Name => "users";

        public string Path => BasePath;

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFeatures();
            services.AddSqlServer();
            services.AddServices();
        }

        public void ConfigureModule(IApplicationBuilder builder)
        {
        }
    }
}
