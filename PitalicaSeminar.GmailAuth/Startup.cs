using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PitalicaSeminar.GmailAuth.Data;
using PitalicaSeminar.GmailAuth.Models;
using PitalicaSeminar.GmailAuth.Services;
using PitalicaSeminar.DAL.Entities;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using PitalicaSeminar.DAL.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace PitalicaSeminar.GmailAuth
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
            
            services.AddScoped<DbContext, PitalicaDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UsersConnection")));

            services.AddDbContext<PitalicaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EntitiesConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Auth:Google:client_id"];
                googleOptions.ClientSecret = Configuration["Auth:Google:client_secret"];
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["JwtAuth:ValidIssuer"],
                    ValidAudience = Configuration["JwtAuth:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtAuth:JwtSecurityKey"]))
                };
            });

            services.AddMvc()
                .AddJsonOptions(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;                       // Kod odgovora slati informaciju o kojoj verziji API-a se radi
                options.AssumeDefaultVersionWhenUnspecified = true;     // Ukoliko verzija nije ekplicitno odabrana, korisiti zadanu verziju
                options.DefaultApiVersion = new ApiVersion(1, 1);       // Postavljanje zadane verzije API-a. U ovom slucaju je odabrana zadnja verzija v1.1
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");   // Odabir verzije API-a pomocu header-a
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new Info { Title = "JWT API", Version = "v1.0" });   // Dodavanje swagger dokumenta za verziju 1.0
                options.SwaggerDoc("v1.1", new Info { Title = "JWT API", Version = "v1.1" });   // Dodavanje swagger dokumenta za verziju 1.0

                // Ovime se implementira logika za odlucivanje u koji dokument ce ici koja verzija servisa
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var versions = apiDesc.ControllerAttributes()
                                        .OfType<ApiVersionAttribute>()
                                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<PitalicaDbContext>().SeedInitial();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "JWT API v1.0");
                options.SwaggerEndpoint("/swagger/v1.1/swagger.json", "JWT API v1.1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "QuestionBodovi",
                    template: "Questions/bodovi/{n?}",
                    defaults: new { controller = "Questions", action = "Score" },
                    constraints: new { n = new IntRouteConstraint() });
            });
        }
    }
}
