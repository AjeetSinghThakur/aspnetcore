using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Module5Demo.Validation;
using System.Collections.Generic;
using System.Globalization;

namespace Module5Demo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddMvc();

            mvcBuilder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            mvcBuilder.AddDataAnnotationsLocalization(options => 
            {
                options.DataAnnotationLocalizerProvider = (type, factory) => 
                {
                    return factory.Create(typeof(ErrorMessages));
                };
            });

            services.Configure<LocalizationOptions>(options => 
            {
                options.ResourcesPath = "Resources";
            });

            services.Configure<MvcOptions>(options => 
            {
                options.ModelMetadataDetailsProviders.Add(
                    new CustomValidationMetadataProvider());
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ru-RU"),
                    new CultureInfo("es-MX"),
                };
                options.DefaultRequestCulture = new RequestCulture("en-US");
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseRequestLocalization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
