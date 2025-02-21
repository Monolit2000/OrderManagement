using MediatR;
using OrderManagement.Application.Contract;
using Dapper;
using System.Data;

namespace OrderManagement.Application.Orders.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllOrdersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT 
                    o.""Id"" AS OrderId, 
                    o.""CustomerFullName"", 
                    o.""CustomerPhone"", 
                    op.""ProductId"", 
                    op.""Amount"", 
                    p.""Name"" AS ProductName
                FROM ""Orders"" o
                LEFT JOIN ""OrderProducts"" op ON o.""Id"" = op.""OrderId""
                LEFT JOIN ""Products"" p ON op.""ProductId"" = p.""Id""
                ORDER BY o.""CreatedOn"" DESC";

            var orderDictionary = new Dictionary<Guid, OrderDto>();

            var result = await connection.QueryAsync<OrderDto, OrderProductDto, OrderDto>(
                sql,
                (order, orderProduct) =>
                {
                    if (!orderDictionary.TryGetValue(order.OrderId, out var orderEntry))
                    {
                        orderEntry = new OrderDto
                        {
                            OrderId = order.OrderId,
                            CustomerFullName = order.CustomerFullName,
                            CustomerPhone = order.CustomerPhone,
                            OrderProducts = new List<OrderProductDto>()
                        };
                        orderDictionary.Add(order.OrderId, orderEntry);
                    }

                    if (orderProduct != null)
                    {
                        orderEntry.OrderProducts.Add(new OrderProductDto
                        {
                            ProductId = orderProduct.ProductId,
                            ProductName = orderProduct.ProductName,
                            Amount = orderProduct.Amount
                        });
                    }

                    return orderEntry;
                },
                splitOn: "ProductId"
            );

            return orderDictionary.Values.ToList();
        }
    }
}
