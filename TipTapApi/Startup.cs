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
using Domain.StaffMembers;
using Common;
using Persistence.Repositories;
using Common.Entities;
using Common.DTOs.StaffMemberDtos;
using Common.DTOs.TeamDtos;
using Domain.Teams;
using Common.DTOs.JobDtos;
using Domain.Jobs;
using Common.RepositoryInterfaces;
using Common.DTOs.CheckOutDtos;
using Domain.Checkouts;
using Common.DTOs.TipOutDtos;
using Domain.TipOuts;
using Common.DTOs.EarningsDtos;
using Domain.StaffEarnings;

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
            services.AddDbContext<CheckoutManagerContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly("TipTapApi")).EnableSensitiveDataLogging());
            services.AddScoped<IStaffMemberRepository, StaffMemberRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<ICheckoutRepository, CheckoutRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IEarningsRepository, EarningsRepository>();
            services.AddTransient<IValidator<StaffMemberDto>, StaffMemberValidator>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            app.UseCors(
                options => options.WithOrigins("http://localhost:3000").AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<StaffMemberEntity, StaffMemberDto>();
                cfg.CreateMap<StaffMemberDto, UpdateStaffMemberName>();
                cfg.CreateMap<UpdateStaffMemberName, StaffMemberEntity>();
                cfg.CreateMap<StaffMemberDto, StaffMemberEntity>();
                cfg.CreateMap<StaffMemberDto, StaffMember>();
                cfg.CreateMap<StaffMember, StaffMemberDto>();
                cfg.CreateMap<AddStaffMemberDto, StaffMemberDto>();
                cfg.CreateMap<StaffMemberDto, AddStaffMemberDto>();
                cfg.CreateMap<StaffMember, StaffMemberEntity>();

                cfg.CreateMap<ServerTeamDto, ServerTeam>();
                cfg.CreateMap<ServerTeam, ServerTeamDto>();
                cfg.CreateMap<ServerTeam, TeamEntity>();
                cfg.CreateMap<ServerTeamDto, TeamEntity>();
                cfg.CreateMap<TeamEntity, ServerTeam>();
                cfg.CreateMap<TeamEntity, ServerTeamDto>();

                cfg.CreateMap<TeamEntity, BarTeam>()
                .ForCtorParam("shiftDate", opt => opt.MapFrom(src => src.ShiftDate));
                cfg.CreateMap<BarTeam, TeamEntity>();

                cfg.CreateMap<CreateCheckoutDto, Checkout>();
                cfg.CreateMap<Checkout, CheckoutEntity>();
                cfg.CreateMap<CheckoutEntity, Checkout>()
                .ForCtorParam("job", opt => opt.MapFrom(src => src.Job))
                .ForCtorParam("staffMember", opt => opt.MapFrom(src => src.StaffMember))
                .ForCtorParam("shiftDate", opt => opt.MapFrom(src => src.ShiftDate));
                cfg.CreateMap<Checkout, CheckoutDto>();
                cfg.CreateMap<UpdateCheckoutDto, CheckoutEntity>();
                cfg.CreateMap<CheckoutEntity, CheckoutDto>();
                cfg.CreateMap<CheckoutEntity, CheckoutOverviewDto>()
                .ForCtorParam("fullName", opt => opt.MapFrom(src => src.StaffMember.FirstName + " " + src.StaffMember.LastName))
                .ForCtorParam("jobTitle", opt => opt.MapFrom(src => src.Job.Title));

                cfg.CreateMap<JobEntity, JobDto>();
                cfg.CreateMap<JobDto, Job>();
                cfg.CreateMap<Job, JobEntity>();
                cfg.CreateMap<JobEntity, Job>();
                cfg.CreateMap<Job, JobDto>();

                cfg.CreateMap<TipOutEntity, TipOutDto>();
                cfg.CreateMap<TipOutEntity, TipOut>();
                cfg.CreateMap<TipOut, TipOutEntity>();

                cfg.CreateMap<EarningDto, EarningsEntity>();
                cfg.CreateMap<Earnings, EarningDto>();
                cfg.CreateMap<Earnings, EarningsEntity>();
                cfg.CreateMap<EarningsEntity, Earnings>();
            });
            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
