using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PruebaTecnicaGalaxy.AutoMapper;
using PruebaTecnicaGalaxy.Middlewares;
using PruebaTecnicaGalaxy.Modelos.EntityFrameworkCore;
using PruebaTecnicaGalaxy.Modelos.Interfaces;
using PruebaTecnicaGalaxy.Modelos.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prueba_Tecnica_Galaxy", Version = "v1" });
            });

            var conexion = Configuration["ConnectionStrings:conexion_MySQL"];
            services.AddDbContext<AplicationDbContext>(options => options.UseMySql(conexion, ServerVersion.AutoDetect(conexion)));

            services.AddOptions();
            // Dependencias AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));
            //Ignorar las referencias circulares
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            //COnfiguracion de los servicios e interfaces
            services.AddScoped<ITrabajadoresServicio,TrabajadoresServicio>();
            services.AddScoped<IContratosServicio, ContratosServicio>();

            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("http://localhost:3000") //aqui colocas la url y el puerto que usa react
                  .AllowAnyMethod()
                  .AllowAnyHeader());
                  //.AllowCredentials());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ManejadorDeErroresMiddleware>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PruebasTecnicasGalaxy v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
