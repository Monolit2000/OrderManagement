using Dapper;
using MediatR;
using OrderManagement.Application.Contract;

namespace OrderManagement.Application.Products.GetAllProducts
{
    public class GetAllProductsQueryHandler(
       ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            using var connection = sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT ""Id"" AS ProductId, ""Code"", ""Name"", ""Price""
                FROM ""Products""";

            var products = await connection.QueryAsync<ProductDto>(sql);

            return products.ToList();
        }
    }
}
