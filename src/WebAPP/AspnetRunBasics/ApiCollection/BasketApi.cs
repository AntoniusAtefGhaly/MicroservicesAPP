using AspnetRunBasics.ApiCollection.Infrastucture;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using AspnetRunBasics.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspnetRunBasics.ApiCollection
{
    public class BasketApi : BaseHttpClientWithFactory, IBasketApi
    {
        private readonly IApiSettings _setting;

        public BasketApi(IApiSettings setting,IHttpClientFactory factory):base(factory)     
        {
            this._setting = setting ?? throw new ArgumentNullException(nameof(setting));
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var message = new HttpRequestBuilder(_setting.BaseAddress)
                .SetPath(_setting.BasketPath)
                .AddToPath(userName)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();
            return await SendRequest<BasketModel>(message);
        }
        public async Task<BasketModel> CreateBasket(BasketModel model)
        {
            var message = new HttpRequestBuilder(_setting.BaseAddress)
                .SetPath(_setting.BasketPath)
                .HttpMethod(HttpMethod.Post)
                .GetHttpMessage(); 
            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendRequest<BasketModel>(message);

        }
        

        public async Task Checkout(BasketCheckoutModel model)
        {
            var message = new HttpRequestBuilder(_setting.BaseAddress)
                .SetPath(_setting.BasketPath)
                .AddToPath("checkout")
                .HttpMethod(HttpMethod.Post)
                .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

             await SendRequest<BasketCheckoutModel>(message);
        }


    }
}
