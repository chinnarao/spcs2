using System;
using DbContexts;
using File;
using Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Article
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
            services.AddDbContext<ArticleDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton(new AutoMapper.MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); }).CreateMapper());

            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IFileRead, FileRead>();
            services.AddScoped<IGoogleStorage, GoogleStorage>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            #endregion
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
            #region Swagger
            //http://localhost:<port>/swagger
            //http://localhost:<port>/swagger/v1/swagger.json

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            #endregion
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
