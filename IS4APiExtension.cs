using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZL.IdentityServer4ClientConfig;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IS4APiExtension
    {
        public static IServiceCollection AddIdentityServer4Api(this IServiceCollection services, IConfiguration configurateion)
        {
            var cfg = configurateion.GetSection("IdentityServer4Api");
            var opt = new ApiOption();
            ConfigurationBinder.Bind(cfg, opt);

            services.AddCors(option => option.AddPolicy("cors",
                policy => policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins(opt.CorsOrgins.ToArray())));

            // accepts any access token issued by identity server
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = opt.Authority;
                    options.RequireHttpsMetadata = opt.RequireHttpsMetadata;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            // adds an authorization policy to make sure the token is for scope 'api1'
            services.AddAuthorization(options =>
            {
                foreach(var p in opt.Policies)
                {
                    options.AddPolicy(p.Name, policy =>
                    {
                        if(p.RequireAuthenticatedUser)policy.RequireAuthenticatedUser();
                        foreach(var c in p.Claims)
                        {
                            policy.RequireClaim(c.ClaimType,c.AllowValues.ToArray());
                        }
                        
                    });
                }
                
            });
            return services;
        }
    }
}
