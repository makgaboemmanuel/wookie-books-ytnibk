using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreAPI.BookData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using BookStoreAPI.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


namespace BookStoreAPI
{
    /* to configure Services used by the Application, inlucing Database, Swagger UI and Authentication */
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
            services.AddControllers().AddXmlDataContractSerializerFormatters();  ; /* orginal line: services.AddControllers(); this will allow the application to send json as well as xml data types */

            /* user added  */
            services.AddSingleton<IBookData, MockBookData>();
            /*   user added  */
            services.AddDbContextPool<BookContext>(options => options.UseSqlServer( 
                Configuration.GetConnectionString("BookContextConnectionString") ));

            /* this is for identity */
            services.AddIdentity<User, Microsoft.AspNetCore.Identity.IdentityRole>()
                .AddEntityFrameworkStores<BookContext>()
                .AddDefaultTokenProviders();

            
            /* this is for authentication */
            services.AddAuthentication(option =>
           {
               option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
           })
            /* adding the Jwt Token */
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes( Configuration["JWT:Key"]))
                 };
            });

            /* user added  */
            services.AddScoped<IBookData, SqlBookData>();
            /* user added: to add the Swagger Package */
            services.AddMvc();
            services.AddSwaggerGen( c=> { c.SwaggerDoc("v1", new Info { Title="My API", Version="V1" } ); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseSwagger(); /* this is supposed to create the swagger document, but is is causing an error  */
            app.UseSwaggerUI(  c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Version 1");
            }  ) ; 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
