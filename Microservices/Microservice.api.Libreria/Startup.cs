using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.api.Libreria.Core;
using Microservice.api.Libreria.Core.ContextMongoDB;
using Microservice.api.Libreria.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Microservice.api.Libreria
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
            services.Configure<MongoSettings>( options => {
                options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoDb:Database").Value;
            });

            services.AddScoped(typeof(IMongoRepository<>),typeof(MongoRepository<>));
            services.AddSingleton<MongoSettings>();
            services.AddTransient<IAuthorContext, AuthorContext>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservice.api.Libreria", Version = "v1" });
            });


            services.AddCors(config =>
            {
                config.AddPolicy("CorsRule", rule =>
                 {
                     rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                 });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservice.api.Libreria v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsRule");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
