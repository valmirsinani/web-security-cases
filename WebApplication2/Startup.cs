using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using MySql.Data.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Http;

namespace WebApplication2
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
            services.AddControllersWithViews();//per session
            services.AddDistributedMemoryCache();//per session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });//per session

            //services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
            services.AddDbContext<EmployeeContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySQLConnection")).EnableSensitiveDataLogging());
            services.AddTransient<MySqlDatabase>(_ => new MySqlDatabase(Configuration.GetConnectionString("MySQLConnection")));
            string mySqlConnectionStr = Configuration.GetConnectionString("MySQLConnection");
           // services.AddEntityFrameworkMySQL();
           // services.AddDbContextPool<EmployeeContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            // CSP TO AVOID XXS
            /*default-src — Specifies the default for other sources
            script-src,style-src,object-src,img-src,media-src,frame-src,font-src,connect-src*/

            app.Use(async (context, next) =>
            {
                await next.Invoke();
                //After going down the pipeline check if we 404'd. 
                if (context.Response.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)
                {
                    await context.Response.WriteAsync("Page not found");
                }
            });

            app.UseExceptionHandler("/home/error");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();//per session

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }



    public class CustomExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {
        //write the code logic to store the error here

        RedirectToRouteResult result = new RedirectToRouteResult(
               new Microsoft.AspNetCore.Routing.RouteValueDictionary {  {"controller", "Error"}, {"action", "CustomError"}
               });
    }

}
