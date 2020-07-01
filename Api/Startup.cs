using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Email;
using Api.Helpers;
using Application.Commands;
using Application.Interfaces;
using Application.Login;
using EfCommands;
using EfDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<EfContext>();
            //roles
            services.AddTransient<IGetRoleCommand, EfGetRoleCommand>();
            services.AddTransient<IGetRolesCommand, EfGetRolesCommand>();
            services.AddTransient<IAddRoleCommand, EfAddRoleCommand>();
            services.AddTransient<IEditRoleCommand, EfEditRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();
            //categories
            services.AddTransient<IGetCategoryCommand, EfGetCategoryCommand>();
            services.AddTransient<IGetCategoriesCommand, EfGetCategoriesCommand>();
            services.AddTransient<IAddCategoryCommand, EfAddCategoryCommand>();
            services.AddTransient<IEditCategoryCommand, EfEditCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            //users
            services.AddTransient<IGetUserCommand, EfGetUserCommand>();
            services.AddTransient<IGetUsersCommand, EfGetUsersCommand>();
            services.AddTransient<IAddUserCommand, EfAddUserCommand>();
            services.AddTransient<IEditUserCommand, EfEditUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            //posts
            services.AddTransient<IGetPostCommand, EfGetPostCommand>();
            services.AddTransient<IGetPostsCommand, EfGetPostsCommand>();
            services.AddTransient<IAddPostCommand, EfAddPostCommand>();
            services.AddTransient<IEditPostCommand, EfEditPostCommand>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();
            //tags
            services.AddTransient<IGetTagsCommand, EfGetTagsCommand>();
            services.AddTransient<IGetTagCommand, EfGetTagCommand>();
            services.AddTransient<IAddTagCommand, EfAddTagCommand>();
            services.AddTransient<IEditTagCommand, EfEditTagCommand>();
            services.AddTransient<IDeleteTagCommand, EfDeleteTagCommand>();
            //login
            services.AddTransient<ILoginUserCommand, EfLoginUserCommand>();
            //encryption
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            var key = Configuration.GetSection("Encryption")["key"];

            var enc = new Encryption(key);
            services.AddSingleton(enc);


            services.AddTransient(s => {
                var http = s.GetRequiredService<IHttpContextAccessor>();
                var value = http.HttpContext.Request.Headers["Authorization"].ToString();
                var encryption = s.GetRequiredService<Encryption>();

                try
                {
                    var decodedString = encryption.DecryptString(value);
                    decodedString = decodedString.Replace("\f", "");
                    var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);
                    user.IsLogged = true;
                    return user;
                }
                catch (Exception)
                {
                    return new LoggedUser
                    {
                        IsLogged = false
                    };
                }
            });

            //email
            var section = Configuration.GetSection("Email");

            var sender =
                new SmtpEmailSender(section["host"], Int32.Parse(section["port"]), section["fromaddress"], section["password"]);

            services.AddSingleton<IEmailSender>(sender);

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "BlogPost API", Version = "v1" });
               // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
               // c.IncludeXmlComments(xmlPath);
            });

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();


            //swagger
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
