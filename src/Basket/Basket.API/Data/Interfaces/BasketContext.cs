using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Data.Interfaces
{
    public class BasketContext : IBasketContext
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public BasketContext(ConnectionMultiplexer redisConnection)
        {
            this._redisConnection = redisConnection;
            Redis = redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}
