﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Proiect2.Data;

[assembly: HostingStartup(typeof(Proiect2.Areas.Identity.IdentityHostingStartup))]
namespace Proiect2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddIdentity<IdentityUser, IdentityRole>(options =>
                    options.SignIn.RequireConfirmedAccount = true)
                     .AddEntityFrameworkStores<IdentityContext>();

            });
        }
    }
}