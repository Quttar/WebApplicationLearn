using HttpBasicAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplicationLearn
{
    public class Startup
    {
        IServiceCollection _services;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("hello world");
                });
                
                endpoints.MapGet("/table", async context =>
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<table>");
                    sb.AppendLine("<tr><th>ServiceType</th><th>Lifetime</th><th>ImplementationType</th></tr>");

                    List<ServiceDescriptor> serviceList = _services.ToList<ServiceDescriptor>();
                    foreach (ServiceDescriptor serviceDescriptor in serviceList)
                    {
                        sb.AppendLine($@"<tr>
                                            <td>{serviceDescriptor.ServiceType}</td>
                                            <td>{serviceDescriptor.Lifetime}</td>
                                            <td>{serviceDescriptor.ImplementationType}</td>
                                        </tr>");
                    }

                    sb.AppendLine("</table>");
                    

                    await context.Response.WriteAsync(sb.ToString());
                });
            });
        }
    }
}
