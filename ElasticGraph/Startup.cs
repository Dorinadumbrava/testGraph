using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticGraph.GraphQl;
using ElasticGraph.GraphQL;
using ElasticGraph.Services;
using ElasticGraph.Services.Interfaces;
using GraphQL;
using GraphQL.Client;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ElasticGraph
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
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddControllers();
            services.AddScoped(x => new GraphQLClient(Configuration["GraphQLURI"]));
            services.AddSwaggerGen(c =>
            c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "TestAPI", Version = "v1" }));

            services.AddScoped<IDependencyResolver>(s=> new FuncDependencyResolver(
                s.GetRequiredService));
            services.AddScoped<ElasticGraphSchema>();
            services.AddGraphQL(o => { o.ExposeExceptions = true; }).AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(httpContext=>httpContext.User)
                .AddDataLoader();
            
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IElasticService, ElasticService>();
            services.AddScoped<IRecepyService, RecepyService>();
            services.AddScoped<IRecepyGraphClient, RecepyGraphClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<ElasticGraphSchema>();

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Test app V1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
