using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LS.Redis.Domain.Repository;
using LS.Redis.Domain.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LS.Redis
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.DataBase.ConnectionStringRedis;
                options.InstanceName = "TesteRedisCache";
            });

            services.AddSingleton(new MongoClient(Configuration.DataBase.ConnectionStringMongo));
            services.AddTransient(options => options.GetRequiredService<MongoClient>().GetDatabase("Api"));

            services.AddSingleton<IProdutoRepository, ProdutoRepository>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
