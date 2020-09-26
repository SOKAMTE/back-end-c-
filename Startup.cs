using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using offreService.Data;
using Microsoft.EntityFrameworkCore;
using offreService.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace offreService
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

            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<OffreServiceContext>(options =>
                options.UseMySql(connection));

            //Configure Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "SwaggerDemo", Version = "v1"});
                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v2",
                    Title = "SwaggerDemo API",
                });
                var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "SwaggerDemo.xml");
 
                c.IncludeXmlComments(filePath);
            });

            //Configuration du namespace repository
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleTypeRepository, ArticleTypeRepository>();
            services.AddScoped<ICommentaireRepository, CommentaireRepository>();
            services.AddScoped<ICompteRepository, CompteRepository>();
            services.AddScoped<IDiscussionRepository, DiscussionRepository>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("v1/swagger.json", "SwaggerDemo v1");
                c.SwaggerEndpoint("v2/swagger.json", "SwaggerDemo v2");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                    //spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
