using Discount.Grpc.Entities;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon counpan)
        {
            using var connection = new NpgsqlConnection(
              _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new { ProductName = counpan.ProductName, Description = counpan.Description, Amount = counpan.Amount });
            return affected > 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(
              _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductName = @ProductName)",
                new { ProductName = productName });
            return affected > 0;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(
                _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupan = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupan WHERE ProductName = @ProductName",
                new { ProductName = productName});

            if (coupan == null)
            return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };
            return coupan;

        }

        public async Task<bool> UpdateDiscount(Coupon counpan)
        {
            using var connection = new NpgsqlConnection(
             _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("Update Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id)",
                new { ProductName = counpan.ProductName, Description = counpan.Description, Amount = counpan.Amount, Id = counpan.Id });
            return affected > 0;
        }
    }
}
