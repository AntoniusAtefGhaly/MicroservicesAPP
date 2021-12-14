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
using System.Threading.Tasks;

namespace AspnetRunBasics.ApiCollection
{
    public class CatalogApi : BaseHttpClientWithFactory, ICatalogApi
    {
        private readonly IApiSettings _settings;

        public CatalogApi(IApiSettings settings, IHttpClientFactory factory) : base(factory)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }
        public async Task<IEnumerable<CatalogModel>> GetCtalog()
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                          .SetPath(_settings.CatalogPath)
                          .HttpMethod(HttpMethod.Get)
                          .GetHttpMessage();
            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }

        public async Task<CatalogModel> GetCtalog(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                 .SetPath(_settings.CatalogPath)
                 .AddToPath(id)
                 .HttpMethod(HttpMethod.Get)
                 .GetHttpMessage();
             return await SendRequest<CatalogModel>(message);
        }

        public async Task<IEnumerable<CatalogModel>> GetCtalogByName(string name)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .AddToPath("GetProductByName")
                .AddToPath(name)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();
                 return await SendRequest<IEnumerable<CatalogModel>>(message);
        }

        public async Task<IEnumerable<CatalogModel>> GetCtalogByCatalog(string catalog)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                          .SetPath(_settings.CatalogPath)
                          .AddToPath("GetProductByCategory")
                          .AddToPath(catalog)
                          .HttpMethod(HttpMethod.Get)
                          .GetHttpMessage();
            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }


        public async Task<CatalogModel> CreateCtalog(CatalogModel model)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
             .SetPath(_settings.CatalogPath)
             .HttpMethod(HttpMethod.Post)
             .GetHttpMessage();
            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "APPLICATION/JSON");
            return await SendRequest<CatalogModel>(message);
        }
    }
}
