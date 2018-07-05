using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Google;
using File;
using DbContexts;
using Services;
//https://github.com/dotnet-architecture/eShopOnWeb
//https://github.com/aspnet/Docs/blob/master/aspnetcore/test/integration-tests/samples/2.x/IntegrationTestsSample/src/RazorPagesProject/Startup.cs
//https://github.com/dotnet-presentations/home/tree/master/ASP.NET%20Core/ASP.NET%20Core%20-%20What-s%20New
//https://www.linode.com/pricing
namespace Ad
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
            services.AddCors();
            services.AddDbContext<AdDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddUnitOfWork<AdDbContext>();

            services.AddSingleton(new AutoMapper.MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); }).CreateMapper());

            services.AddScoped<IAdService, AdService>();
            services.AddScoped<IFileRead, FileRead>();
            services.AddScoped<IGoogleStorage, GoogleStorage>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Ad API", Version = "v1" });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

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

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
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
