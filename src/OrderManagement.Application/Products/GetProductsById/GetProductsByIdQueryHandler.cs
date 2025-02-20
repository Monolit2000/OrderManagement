using Dapper;
using MediatR;
using OrderManagement.Application.Contract;

namespace OrderManagement.Application.Products.GetProductsById
{
    public class GetProductsByIdQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT ""Id"" AS ProductId, ""Code"", ""Name"", ""Price""
                FROM ""Products""
                WHERE ""Id"" = @ProductId";

            var product = await connection.QuerySingleOrDefaultAsync<ProductDto>(sql, new { request.ProductId });

            if (product == null)
                throw new Exception($"Product with ID {request.ProductId} not found.");

            return product;
        }
    }
}
