using Microsoft.EntityFrameworkCore;
using ECommerce.API.Extensions;
using ECommerce.Domain.Contracts;
using ECommerce.Persistence.Data.Context;
using ECommerce.Persistence.Data.DataSeed;
using ECommerce.Persistence.Repositories;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Implementation;

namespace ECommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Register DI Container [Register Services]
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                );
            });

            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(cfg => { },typeof(ServiceAssemblyReference).Assembly);

            builder.Services.AddScoped<IProductService, ProductService>();
            #endregion

            var app = builder.Build();

            await app.MigrateDataBaseAsync();
            await app.SeedDataAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            await app.RunAsync();
        }
    }
}
