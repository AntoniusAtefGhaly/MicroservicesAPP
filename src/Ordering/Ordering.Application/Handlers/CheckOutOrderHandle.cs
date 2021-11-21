using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Mapper;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class CheckOutOrderHandle : IRequestHandler<CheckOutOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _oderRepository;

        public CheckOutOrderHandle(IOrderRepository oderRepository)
        {
            this._oderRepository = oderRepository ?? throw new ArgumentNullException(nameof(oderRepository));
        }

        public async Task<OrderResponse> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderentity = OrderMapper.Mapper.Map<Order>(request);
            if (orderentity == null)
            {
                throw new ApplicationException("not mapped");
            }
            var newOrder =await _oderRepository.AddAsync(orderentity);
            var orderResponse = OrderMapper.Mapper.Map<OrderResponse>(newOrder);
            return orderResponse;
        }
    }
}
