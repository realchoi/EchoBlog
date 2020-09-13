using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using EchoLab.Api.Attribute;
using EchoLab.Api.Extensions;
using EchoLab.Infrastructures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EchoLab.Api
{
    public class Startup
    {
        /// <summary>
        /// Api ����
        /// </summary>
        public string ApiName { get; set; } = "EchoLab";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ������ݿ�����
            services.AddMySqlDomainContext(Configuration.GetConnectionString("Default"));
            // ��� Swagger ����
            services.AddSwaggerService();
            // �����Ȩ����
            services.AddJwtService(Configuration);
            // ��� AutoMapper
            services.AddAutoMapperService(Configuration);
            // ��� MediatR
            services.AddMediatRService();
            // ���ҵ����
            services.AddCustomerService();

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

            // ȷ�����ݿⴴ��
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DomainContext>();
                context.Database.EnsureCreated();
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
