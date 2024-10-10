using Microsoft.OpenApi.Models;

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
            builder.Services.AddSingleton<ToyService>();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "ToyStoreAPI", Version = "v1" });
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

            var app = builder.Build();

            app.UseCors(allowAnyOriginPolicy);

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}