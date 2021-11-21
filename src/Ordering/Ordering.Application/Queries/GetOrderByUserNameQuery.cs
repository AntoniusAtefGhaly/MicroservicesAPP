using MediatR;
using Ordering.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Queries
{
    public class GetOrderByUserNameQuery:IRequest<IEnumerable<OrderResponse>>
    {
        public string userName { get; set; }
        public GetOrderByUserNameQuery(string userName)
        {
            this.userName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
