using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proiect2.Data;
using Microsoft.EntityFrameworkCore;
using Proiect2.Hubs;
using Microsoft.AspNetCore.Identity;
using System;


namespace Proiect2
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
            services.AddControllersWithViews();
            services.AddDbContext<PhoneContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //lab 7
            services.AddSignalR();

            //lab 9 pct 6
            services.AddRazorPages();


            //PCT 3 PROIECT
            //realizarea a doua configurari diferite de setarile default
            //
            //

            //............................................................
            //lab8 te,a -- cod luat din cursul 8  ----configurare Lockout
            services.Configure<IdentityOptions>(options => {
                //  Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // ACCOUNT BLOCAT PT 15 MIN
                options.Lockout.MaxFailedAccessAttempts = 5;  //maxim 5 incercari de autentificare
                options.Lockout.AllowedForNewUsers = true;

                //  Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 1;
            });

            //..................................................


            //lab9 pct 16

            //sectiunea de magazine va putea fi vizualizata doar de catre persoanele care au rol de manager
           services.AddAuthorization(opts => {
                opts.AddPolicy("OnlyManagers", policy => {
                    policy.RequireRole("Manager");
                });
            });
            services.ConfigureApplicationCookie(opts =>
            {
                opts.AccessDeniedPath = "/Identity/Account/AccessDenied";

            });
  


            //lab 9
            /*In sectiunea Customers accesul va fi autorizat doar pentru utilizatorii cu rol de manager si care fac
            parte din departamentul Sales. Vom crea astfel o noua politica cu denumire SalesManager in clasa
            Startup.cs si vom configura aplicatia sa afiseze pagina AccesDenied existenta in libraria Identity in
            cazul in care accesul este restrictionat:*/

            /*services.AddAuthorization(opts => {
                opts.AddPolicy("SalesManager", policy => {
                    policy.RequireRole("Manager");
                    policy.RequireClaim("Department", "Sales");
                });
            });
            services.ConfigureApplicationCookie(opts =>
            {
                opts.AccessDeniedPath = "/Identity/Account/AccessDenied";

            });*/



            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            //lab 8
            app.UseAuthentication();

            /* services.Configure<IdentityOptions>(options =>
             {
                 // Default Lockout settings.
                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                 options.Lockout.MaxFailedAccessAttempts = 5;
                 options.Lockout.AllowedForNewUsers = true;
             });*/


            //
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");

                 //lab7
                 endpoints.MapHub<ChatHub>("/chathub");

                 //lab8
                 endpoints.MapRazorPages();  

            
            });








        }



        //services.AddScoped(typeof(IrepositoryTab<>), typeof(RepositoryTab<>));


    }
}
