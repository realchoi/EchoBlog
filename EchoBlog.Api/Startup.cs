using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using AutoMapper;
using EchoBlog.Api.Attribute;
using EchoBlog.Api.Extension;
using EchoBlog.Api.Util.Auth;
using EchoBlog.Api.Util.AutoMapper;
using EchoBlog.Repository.DbConfig;
using EchoBlog.Repository.Def;
using EchoBlog.Repository.Impl;
using EchoBlog.Service.Def;
using EchoBlog.Service.Impl;
using EchoBlog.Util.AppConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace EchoBlog.Api
{
    public class Startup
    {
        /// <summary>
        /// Api ����
        /// </summary>
        public string ApiName { get; set; } = "EchoBlog";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;

            // ��ʼ�����ݿ������ַ���
            BaseDbConfig.ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region ע���õ�����

            services.AddSingleton(new AppSetting(Env.ContentRootPath));
            services.AddSingleton<IArticleRepository, ArticleRepository>();
            services.AddSingleton<IArticleService, ArticleService>();

            #endregion

            // ��� Swagger ����
            services.AddSwaggerConfiguration();
            // �����Ȩ����
            services.AddJwtConfiguration();
            // ��� AutoMapper
            services.AddAutoMapperConfiguration();

            services.AddControllers();

            // ע������������
            services.ConfigureDynamicProxy(cfg =>
            {
                cfg.Interceptors.AddTyped<LogAttribute>(Predicates.ForService("*Service"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ���� Swagger �м��
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"{ApiName} v1");
                c.RoutePrefix = "";
            });

            app.UseRouting();

            // ����֤
            app.UseAuthentication();
            // ����Ȩ
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
