using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrganizacnaStruktura.Data;

namespace OrganizacnaStruktura
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CompaniesContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("CompaniesConnection")));

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IEmployeesRepo, SqlEmployeesRepo>();
            services.AddScoped<IDepartmentsRepo, SqlDepartmentRepo>();
            services.AddScoped<IProjectsRepo, SqlProjectsRepo>();
            services.AddScoped<IDivisionRepo, SqlDivisionsRepo>();
            services.AddScoped<ICompaniesRepo, SqlCompaniesRepo>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
