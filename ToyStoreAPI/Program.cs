using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ToyStoreAPI.Data;
using ToyStoreAPI.Helpers;

namespace ToyStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var allowAnyOriginPolicy = "_allowAnyOrigin";

            // Add services to the container.
            // Register ToyService as a singleton (since the toy data is static)
            builder.Services.AddScoped<DBSeeder>();
            builder.Services.AddScoped<ToyService>();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "ToyStoreAPI", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                                name: allowAnyOriginPolicy,
                                policy =>
                                {
                                    policy.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                });
            });

            builder.Services.AddDbContext<ToyStoreContext>(options =>
            {
                options.ConfigureWarnings(warnOpts =>
                {
                    // InMemory doesn't support transactions and we're ok with it
                    warnOpts.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                });

                options.UseInMemoryDatabase(databaseName: "ToyStore");
            });

            var app = builder.Build();

            app.UseCors(allowAnyOriginPolicy);

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();
            app.UseSeedDB();
            app.Run();
        }
    }
}