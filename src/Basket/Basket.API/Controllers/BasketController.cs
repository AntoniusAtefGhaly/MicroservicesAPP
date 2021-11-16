using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories.Interface;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Producer;
using Events.EventBusRabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _eventBus;

        public BasketController(IBasketRepository repository, IMapper mapper, EventBusRabbitMQProducer eventBus)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasket(string userName)
        {
            var basket= await _repository.GetBasket(userName);
            return Ok(basket??new BasketCart("username"));
        }
        [HttpPost]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task< ActionResult<BasketCart>> UpdateBasket([FromBody]BasketCart userName)
        {
            var basket= await _repository.UpdateBasket(userName);
            return Ok(basket);
        }
        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasket(string userName)
        {
           var res = await _repository.DeleteBasket(userName);
            return Ok(res);
        }
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType( (int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BasketCart>> CheckOut([FromBody]BasketCheckout checkout)
        {
            var basket = await _repository.GetBasket(checkout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            var basketRemoved = await _repository.DeleteBasket(basket.UserName);
            if (!basketRemoved)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckOutEvent>(checkout);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.TotalPrice = basket.TotalPrice;

            try
            {
                _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            }
            catch (Exception)
            {
                throw;
            }

            return Accepted();
        }
    }
}