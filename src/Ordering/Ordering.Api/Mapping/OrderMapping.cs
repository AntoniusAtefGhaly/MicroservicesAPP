using AutoMapper;
using Events.EventBusRabbitMQ;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Api.Mapping
{
    public class OrderMapping : Profile
    {
        public  OrderMapping()
        {
            CreateMap<BasketCheckOutEvent, CheckOutOrderCommand>().ReverseMap();
        }
    }
}
