using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Crypto.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace Crypto
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(
                    "DefaultConnection")));
            services.AddTransient<ITransactions, EFTransactions>();
            services.AddTransient<IUser, EFUsers>();
            services.AddTransient<IReport, EFReport>();
            services.AddTransient<IWallet, EFWallet>();
            services.AddSingleton<ISearchedTransactions, SearchedTransaction>();
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddTransient<ILoginUser, EFLoginUsers>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            services.AddMvc();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Transaction}/{action=List}/{id?}");

            });

        }
    }
}
