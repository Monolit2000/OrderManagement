using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace OrderManagement.Application.Orders.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<List<OrderDto>>   
    {
    }
}
