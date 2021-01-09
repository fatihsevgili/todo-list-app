using System;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoListDataService.Data.Model.Context;
using ToDoListDataService.Data.Repository.TodoItemRepository;
using ToDoListDataService.Data.Repository.TodoItemRepository.Impl;
using ToDoListDataService.Dto;
using ToDoListDataService.Exceptions;
using ToDoListDataService.Service.Impl;
using ToDoListDataService.Service.Interface;
using ToDoListDataService.ValidationRules.FluentValidation;

namespace ToDoListDataService
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
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<Startup>(); });
            services.AddDbContextPool<DataContext>(optionsAction: options =>
                options.UseNpgsql(connectionString));
            services.AddAutoMapper(GetType().Assembly);
            services.AddScoped<ITodoListServices, TodoListServicesImpl>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<IValidator<TodoItemDto>, TodoItemDtoValidator>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.ConfigureExceptionHandler();
            app.UseMvc();
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<DataContext>().Database.Migrate();
            }
        }
    }
}