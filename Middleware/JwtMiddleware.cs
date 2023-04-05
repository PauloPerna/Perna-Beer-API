using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Middleware
{
    public static class JwtMiddleware
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                                    .GetBytes(Environment.GetEnvironmentVariable("TOKEN"))),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            return services;
        }
    }
}