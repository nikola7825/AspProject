using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Commands.CategoryCommands;
using Application.Commands.RoleCommands;
using Application.Commands.TagCommands;
using Application.Commands.UserCommands;
using Application.Interfaces;
using EfCommands;
using EfCommands.EfCategoryCommands;
using EfCommands.EfRoleCommands;
using EfCommands.EfTagCommands;
using EfCommands.EfUserCommands;
using EfDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMVC.Email;

namespace WebMVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<EfContext>();
            //categories
            services.AddTransient<IAddCategoryCommand, EfAddCategoryCommand>();
            services.AddTransient<IGetCategoriesCommand, EfGetCategoriesCommand>();
            services.AddTransient<IGetCategoryCommand, EfGetCategoryCommand>();
            services.AddTransient<IEditCategoryCommand, EfEditCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            //users
            services.AddTransient<IGetUserCommand, EfGetUserCommand>();
            services.AddTransient<IGetUsersCommand, EfGetUsersCommand>();
            services.AddTransient<IAddUserCommand, EfAddUserCommand>();
            services.AddTransient<IEditUserCommand, EfEditUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IGetRolesWithoutPaginationCommand, EfGetRolesWithoutPaginationCommand>();
            //roles
            services.AddTransient<IGetRolesCommand, EfGetRolesCommand>();
            services.AddTransient<IGetRoleCommand, EfGetRoleCommand>();
            services.AddTransient<IAddRoleCommand, EfAddRoleCommand>();
            services.AddTransient<IEditRoleCommand, EfEditRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();
            //posts
            services.AddTransient<IGetPostsCommand, EfGetPostsCommand>();
            services.AddTransient<IGetPostCommand, EfGetPostCommand>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();
            services.AddTransient<IAddPostCommand, EfAddPostCommand>();
            services.AddTransient<IGetCategoriesWithoutPaginationCommand, EfGetCategoriesWithoutPaginationCommand>();
            services.AddTransient<IGetUsersWithoutPaginationCommand, EfGetUsersWithoutPaginationCommand>();
            services.AddTransient<IGetTagsWithoutPaginationCommand, EfGetTagsWithoutPaginationCommand>();
            services.AddTransient<IEditPostCommand, EfEditPostCommand>();

            //pagination
            services.AddCloudscribePagination();

            //email
            var section = Configuration.GetSection("Email");
            var sender =
                new SmtpEmailSender(section["host"], Int32.Parse(section["port"]), section["fromaddress"], section["password"]);
            services.AddSingleton<IEmailSender>(sender);

            //login
            services.AddTransient<ILoginUserCommand, EfLoginUserCommand>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
