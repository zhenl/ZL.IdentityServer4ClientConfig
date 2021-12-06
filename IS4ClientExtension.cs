using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ZL.IdentityServer4ClientConfig;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IS4ClientExtension
    {
        public static AuthenticationBuilder AddIS4OpenIdConnect(this IServiceCollection builder, IConfiguration Configuration)
        {
            var cfg = Configuration.GetSection("IdentityServer4Client");
            var opt = new ClientOption();
            ConfigurationBinder.Bind(cfg, opt);
            var b=builder.AddAuthentication(options =>
             {
                 options.DefaultScheme = "Cookies";
                 options.DefaultChallengeScheme = "oidc";

             })
                .AddCookie("Cookies").
            AddOpenIdConnect("oidc", options =>
            {
                options.Authority = opt.Authority;
                options.RequireHttpsMetadata = opt.RequireHttpsMetadata;
                options.ClientId = opt.ClientId;
                options.ClientSecret = opt.ClientSecret;
                options.ResponseType = opt.ResponseType;
                options.SaveTokens = opt.SaveTokens;
                
                var scopes = opt.Scopes;
                if (scopes!=null)
                {
                    foreach(var s in scopes)
                    {
                        options.Scope.Add(s);
                    }
                }
                var jsonkeys = opt.JsonKeys;
                if (jsonkeys!=null)
                {
                    foreach(var s in jsonkeys)
                    {
                        var claimtype = s.ClaimType;
                        var key = !string.IsNullOrEmpty(s.Key) ? s.Key : s.ClaimType;
                        options.ClaimActions.MapUniqueJsonKey(claimtype, key);

                    }
                }
                options.GetClaimsFromUserInfoEndpoint = true;
            });
            return b;
        }



    }
}
