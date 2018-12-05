using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TailendersApi.Repository;
using Microsoft.EntityFrameworkCore;
using TailendersApi.WebApi.Managers;
using TailendersApi.WebApi.Services;

namespace TailendersApi.WebApi
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
            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
                    .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var conn = Configuration["AzureDbConnection"];
            services.AddDbContext<TailendersContext>(opts => opts.UseSqlServer(conn));

            services.AddScoped<IMatchesRepository, MatchesRepository>();
            services.AddScoped<IPairingsRepository, PairingsRepository>();
            services.AddScoped<IProfileImagesRepository, ProfileImagesRepository>();
            services.AddScoped<IProfilesRepository, ProfilesRepository>();

            services.AddScoped<IMatchesManager, MatchesManager>();
            services.AddScoped<IPairingsManager, PairingsManager>();
            services.AddScoped<IProfilesManager, ProfilesManager>();
            services.AddTransient<IImageStorageService, ImageStorageService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
