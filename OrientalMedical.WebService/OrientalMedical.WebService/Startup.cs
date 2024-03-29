using AutoMapper;
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
using OrientalMedical.Core.Repositories;
using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Context;
using OrientalMedical.Services.Interfaces;
using OrientalMedical.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrientalMedical.WebService
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
            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddDbContext<OrientalMedicalSystemDBContext>(confi =>
            confi.UseSqlServer(Configuration.GetConnectionString("Connection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medical Oriental API", Version = "v1" });
            });

            services.AddScoped<IRepositoriesWrapper, RepositoriesWrapper>();
            services.AddScoped<IDoctorServices, DoctorServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IOperadorServices, OperadorServices>();
            services.AddScoped<IEspecialidadServices, EspecialidadServices>();
            services.AddScoped<IAdministradorServices, AdministradorServices>();
            services.AddScoped<IPacienteServices, PacienteServices>();
            services.AddScoped<ICitasServices, CitasServices>();
            services.AddScoped<ICienciasMedicasServices, CienciasMedicasServices>();
            services.AddScoped<IHorarioServices, HorarioServices>();
            services.AddScoped<IHorarioTrabajoServices, HorarioTrabajoServices>();
            services.AddScoped<IAusenciasServices, AusenciasServices>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medical Oriental API");
            }
            );

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
