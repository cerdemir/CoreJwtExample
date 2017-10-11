using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace SumService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
         public void ConfigureServices(IServiceCollection services)
        {
            var key = System.Text.Encoding.UTF8.GetBytes("gizli anahtar burada olacak");
            services.AddMvc();
            services.AddAuthentication(options=>{
                options.DefaultAuthenticateScheme="JwtBearer";//JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme="JwtBearer";//JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("JwtBearer", options=>{
                options.TokenValidationParameters= new TokenValidationParameters{
                    ValidateAudience = true,
                    ValidAudience = "oguz",
                    ValidateIssuer = true,
                    ValidIssuer = "AuthService",
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(key),
                    ValidateLifetime=true,
                    ClockSkew = TimeSpan.FromMinutes(10)
                };
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
