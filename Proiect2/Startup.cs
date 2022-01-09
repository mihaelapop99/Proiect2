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
            
            services.Configure<IdentityOptions>(options => {
                //  Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // ACCOUNT BLOCAT PT 15 MIN
                options.Lockout.MaxFailedAccessAttempts = 5;  //maxim 5 incercari de autentificare
                options.Lockout.AllowedForNewUsers = true;  // orice utiilizator nou poate fi blocat

                //  Password settings.
                options.Password.RequireDigit = true;  //parola trebuie sa contina obligatoriu numere de la 0-9
                options.Password.RequireLowercase = true;  //trebuie sa contina litere mici
                options.Password.RequireNonAlphanumeric = true; //trebuie sa contina un caracter special
                options.Password.RequireUppercase = true; //trebuie sa contina litere mari
                options.Password.RequiredLength = 10;  //parola trebuie sa aiba minim 10 caractere
                options.Password.RequiredUniqueChars = 3;  //trebuie sa foloseeasca cel putin 3 caractere diferite

                // User settings.
                options.User.AllowedUserNameCharacters ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                //options.User.RequireUniqueEmail = true;

               //options.SignIn.RequireConfirmedEmail = true;  -- default
            });

            //..................................................



            //  AUTORIZARE

            //lab9 pct 16

            //sectiunea de magazine va putea fi editata doar de catre persoanele care au rol de manager
           services.AddAuthorization(opts => {
                opts.AddPolicy("OnlyManagers", policy => {
                    policy.RequireRole("Manager");
                });
            });
            services.ConfigureApplicationCookie(opts =>
            {
                opts.AccessDeniedPath = "/Identity/Account/AccessDenied";

            });

            // telefoanele pot fi editate doar de catre personalul din dep Sales care au su rol de employee (vezi PhonesController.cs)
            services.AddAuthorization(opts => {
                opts.AddPolicy("OnlySales", policy => {
                    policy.RequireClaim("Department", "Sales");
                });
            });
            services.ConfigureApplicationCookie(opts =>
            {
                opts.AccessDeniedPath = "/Identity/Account/AccessDenied";

            });

            





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
                // by default avem 30 zile
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            
            app.UseAuthentication();

           


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



        


    }
}
