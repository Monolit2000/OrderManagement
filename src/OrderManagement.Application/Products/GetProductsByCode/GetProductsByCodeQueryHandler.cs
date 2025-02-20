using Dapper;   
using MediatR;
using OrderManagement.Application.Contract;

namespace OrderManagement.Application.Products.GetProductsByCode
{
    public class GetProductsByCodeQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetProductsByCodeQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductsByCodeQuery request, CancellationToken cancellationToken)
        {
            using var connection = sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT ""Id"" AS ProductId, ""Code"", ""Name"", ""Price""
                FROM ""Products""
                WHERE ""Code"" = @Code";

            var product = await connection.QuerySingleOrDefaultAsync<ProductDto>(sql, new { request.Code});

            if (product == null)
                throw new Exception($"Product with Code {request.Code} not found.");

            return product;
        }
    }
}
