using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace VueJsAspnetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(x =>
            {
                x.RootPath = "dist"; // set static files to dist
      });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSpaStaticFiles(new StaticFileOptions()
            
            {
#if !DEBUG
      OnPrepareResponse = ctx =>
        {
          // https://developers.google.com/web/fundamentals/performance/webpack/use-long-term-caching
          ctx.Context.Response.Headers[HeaderNames.CacheControl] = "max-age=" + 31536000;
        }  
#endif

            });
            app.UseSpa(x =>
            {
                x.Options.DefaultPage = "/index.html";
            });
        }
    }
}
