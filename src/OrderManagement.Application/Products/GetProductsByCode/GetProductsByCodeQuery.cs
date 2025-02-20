using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.Products.GetProductsByCode
{
    public class GetProductsByCodeQuery : IRequest<ProductDto>
    {
        public string Code { get; set; }
    }
}
