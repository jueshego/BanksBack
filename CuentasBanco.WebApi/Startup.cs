using CuentasBanco.Aplicacion.CasosDeUso;
using CuentasBanco.Aplicacion.Contratos;
using CuentasBanco.Aplicacion.DTO.Request;
using CuentasBanco.Aplicacion.DTO.Response;
using CuentasBanco.Dominio.Contratos;
using CuentasBanco.Dominio.Entidades;
using CuentasBanco.Infraestructura.ContextoBD;
using CuentasBanco.Infraestructura.Repositorios;
using CuentasBanco.WebApi.Extensions;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuentasBanco.WebApi
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
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Logger>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CuentasBanco.WebApi", Version = "v1" });
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddDbContext<DbContext, CuentasBancoDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CuentasBancoDBConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepositorioGenerico<Cliente>, RepositorioGenerico<Cliente>>();
            services.AddScoped<IRepositorioGenerico<Cuenta>, RepositorioGenerico<Cuenta>>();
            //services.AddScoped<IRepositorio<Movimiento>, Repositorio<Movimiento>>();
            //services.AddScoped<IRepositorioReportes, RepositorioReportes>();

            services.AddScoped<IServicioListar<DTOCliente>, ClienteServicio>();
            services.AddScoped<IServicioObtenerPorId<DTOCliente>, ClienteServicio>();
            services.AddScoped<IServicioCrear<DTOGuardarCliente>, ClienteServicio>();
            services.AddScoped<IServicioEditar<DTOGuardarCliente>, ClienteServicio>();
            services.AddScoped<IServicioBorrar, ClienteServicio>();

            services.AddScoped<IServicioListar<DTOCuenta>, CuentaServicio>();
            services.AddScoped<IServicioCrear<DTOGuardarCuenta>, CuentaServicio>();
            services.AddScoped<IServicioEditar<DTOGuardarCuenta>, CuentaServicio>();
            services.AddScoped<IServicioBorrar, CuentaServicio>();

            //services.AddScoped<IServicioListar<DTOMovimientoListado>, MovimientoServicio>();
            //services.AddScoped<IServicioCrear<DTOGuardarMovimiento>, MovimientoServicio>();

            //services.AddScoped<IServicioReportes, ReporteServicio>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CuentasBanco.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseErrorHandlingMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
