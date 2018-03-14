using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TipTapApi.Entities;
using TipTapApi.Models;
using TipTapApi.Models.StaffMemberDtos;
using TipTapApi.Services;
using TipTapApi.Services.RoleServices;
using FluentValidation.AspNetCore;
using FluentValidation;
using Application.Validators;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Persistence.Contexts;
using Persistence.Entities;
using Domain.StaffMembers;
using Common;
using Persistence.Repositories;
using Application.DTOs.StaffMemberDtos;
using Common.Entities;

namespace TipTapApi
{
    public class Startup
    {
        public static IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
            var connectionString = Startup.Configuration["connectionStrings:staffMemberDBConnectionString"];
            services.AddDbContext<StaffMemberContext>(o => o.UseSqlServer(connectionString));
            //services.AddDbContext<JobContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<IStaffMemberRepository, StaffMemberRepository>();
            services.AddScoped<IJobRepository, RoleRepository>();
            services.AddTransient<IValidator<StaffMemberDto>, StaffMemberValidator>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            app.UseCors(
                options => options.WithOrigins("http://localhost:3000").AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<StaffMemberDtoOLD, StaffMemberOLD>();
                cfg.CreateMap<StaffMemberOLD, StaffMemberDtoOLD>();
                cfg.CreateMap<Job, JobDto>();
                cfg.CreateMap<StaffMemberForCreation, StaffMemberOLD>();
                cfg.CreateMap<StaffMemberForUpdateOLD, StaffMemberOLD>();
                cfg.CreateMap<StaffMemberForUpdateOLD, StaffMemberDtoOLD>();
                cfg.CreateMap<StaffMemberEntity, StaffMemberDto>();
                cfg.CreateMap<StaffMemberDto, StaffMemberForUpdateDto>();
                cfg.CreateMap<StaffMemberForUpdateDto, StaffMemberEntity>();
            });
            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
