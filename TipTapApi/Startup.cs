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
using Common.Entities;
using Common.DTOs.StaffMemberDtos;
using Common.DTOs.ShiftDtos;
using Domain.ShiftManager;
using Domain.Groups;
using Common.DTOs.GroupDtos;
using Common.DTOs.TeamDtos;
using Domain.Teams;
using Common.DTOs.JobDtos;
using Domain.Jobs;

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
            services.AddDbContext<ShiftContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly("TipTapApi")));
            services.AddScoped<IStaffMemberRepository, StaffMemberRepository>();
            services.AddTransient<IValidator<StaffMemberDto>, StaffMemberValidator>();
            services.AddCors();
            services.AddScoped<IShiftRepository, ShiftRepository>();
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
                cfg.CreateMap<StaffMemberEntity, StaffMemberDto>();
                cfg.CreateMap<StaffMemberDto, StaffMemberForUpdateDto>();
                cfg.CreateMap<StaffMemberForUpdateDto, StaffMemberEntity>();
                cfg.CreateMap<StaffMemberDto, StaffMember>();
                cfg.CreateMap<StaffMember, StaffMemberDto>();

                cfg.CreateMap<ShiftDto, Shift>();
                cfg.CreateMap<ShiftDto, ShiftEntity>();
                cfg.CreateMap<ShiftEntity, Shift>()
                .ConstructUsing(src => new Shift(src.ShiftDate, src.LunchOrDinner));
                cfg.CreateMap<ShiftEntity, ShiftDto>();
                cfg.CreateMap<Shift, ShiftDto>();
                cfg.CreateMap<Shift, ShiftEntity>();

                cfg.CreateMap<ServerGroupDto, ServerGroup>();
                cfg.CreateMap<ServerGroup, ServerGroupDto>();
                cfg.CreateMap<ServerGroupDto, ServerGroupEntity>();
                cfg.CreateMap<ServerGroupEntity, ServerGroup>();
                cfg.CreateMap<ServerGroup, ServerGroupEntity>();
                cfg.CreateMap<ServerGroupEntity, ServerGroupDto>();

                cfg.CreateMap<ServerTeamDto, ServerTeam>();
                cfg.CreateMap<ServerTeam, ServerTeamDto>();
                cfg.CreateMap<ServerTeam, ServerTeamEntity>();
                cfg.CreateMap<ServerTeamDto, ServerTeamEntity>();
                cfg.CreateMap<ServerTeamEntity, ServerTeam>();
                cfg.CreateMap<ServerTeamEntity, ServerTeamDto>();

                cfg.CreateMap<ServerDto, Server>();
                cfg.CreateMap<Server, ServerDto>();
                cfg.CreateMap<Server, ServerEntity>();
                cfg.CreateMap<ServerEntity, Server>();
                cfg.CreateMap<ServerDto, ServerEntity>();
                cfg.CreateMap<ServerEntity, ServerDto>();
            });
            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
