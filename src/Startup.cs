using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using testing_net.Repositories.Database;
using System.Globalization;
using testing_net.Repositories.Interfaces;

namespace testing_net
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(Configuration["ConnectionString"]));
            services.AddScoped<DataBaseContext>();

            services.AddJsonLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddViewLocalization();
<<<<<<< 1e6d8ce4b189c5204686af36493363f0e71582c1
<<<<<<< 3e454d72121da361e73dc66db8f1be83ae7080ad
            CultureInfo.CurrentUICulture = new CultureInfo(Configuration["DefaultLang"]);   
        }
=======
            CultureInfo.CurrentCulture = new CultureInfo(Configuration["DefaultLang"]);

            services.AddScoped<IUnitOfWork, UnitOfWork>();  
            
            }
>>>>>>> Repository and UnitOfWork patterns. Repository structure. MovieRepository.
=======
            CultureInfo.CurrentUICulture = new CultureInfo(Configuration["DefaultLang"]);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
>>>>>>> Spacing and CultureInfo.CurrentUICulture.

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
