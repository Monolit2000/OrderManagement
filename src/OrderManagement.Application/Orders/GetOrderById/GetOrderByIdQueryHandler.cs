using Dapper;
using MediatR;
using OrderManagement.Application.Contract;

namespace OrderManagement.Application.Orders.GetOrderById
{
    public class GetOrderByIdQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory) : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = sqlConnectionFactory.GetOpenConnection();

            const string orderSql = @"
                SELECT ""Id"" AS OrderId , ""CustomerFullName"", ""CustomerPhone""
                FROM ""Orders""
                WHERE ""Id"" = @OrderId";

            const string orderProductsSql = @"
                SELECT op.""ProductId"", p.""Name"" AS ProductName
                FROM ""OrderProducts"" op
                INNER JOIN ""Products"" p ON op.""ProductId"" = p.""Id""
                WHERE op.""OrderId"" = @OrderId";

            var order = await connection.QuerySingleOrDefaultAsync<OrderDto>(orderSql, new { request.OrderId });

            if (order == null)
                throw new Exception($"Order with ID {request.OrderId} not found.");

            var orderProducts = await connection.QueryAsync<OrderProductDto>(orderProductsSql, new { request.OrderId });

            order.OrderProducts = orderProducts.ToList();

            return order;
        }
    }
}
