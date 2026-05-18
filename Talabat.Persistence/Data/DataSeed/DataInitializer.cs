using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Talabat.Domain.Contracts;
using Talabat.Domain.Models;
using Talabat.Domain.Models.ProductModule;
using Talabat.Persistence.Data.Context;

namespace Talabat.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _context;

        public DataInitializer(StoreDbContext context)
        {
            _context = context;
        }
        public async Task InitializeAsync()
        {
            try
            {
                var hasProducts = await _context.Products.AnyAsync();
                var hasBrands = await _context.ProductBrands.AnyAsync();
                var hasTypes = await _context.ProductTypes.AnyAsync();

                if (hasProducts && hasBrands && hasTypes)
                    return;

                if (!hasBrands)
                {
                    await SeedDataFromJson<ProductBrand, int>("brands.json", _context.ProductBrands);
                }

                if (!hasTypes)
                {
                    await SeedDataFromJson<ProductType, int>("types.json", _context.ProductTypes);
                }

                await _context.SaveChangesAsync();

                if (!hasProducts)
                {
                    await SeedDataFromJson<Product, int>("products.json", _context.Products);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while initializing data: {ex.Message}");
            }
        }
        private async Task SeedDataFromJson<T, Tkey>(string fileName, DbSet<T> dbSet) where T : BaseEntity<Tkey>
        {
            var filePath = @"..\Talabat.Persistence\Data\DataSeed\JsonFiles\" + fileName;

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"The file {fileName} was not found at path {filePath}.");

            try
            {
                await using var dataStream = File.OpenRead(filePath);
                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data is not null)
                {
                    // I need to use Async when I have database hits , or reading from files , exteral api calls 
                    // AddRange and AddRangeAsync are used to modify the state of the entities locally in memory 
                    // but AddRangeAsync in defferent scenarios send calls to database if the entities have auto incremented id => this scenario is database hit
                    await dbSet.AddRangeAsync(data);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"An error occurred while deserializing the file {fileName}: {ex.Message}");
            }
        }
    }

}