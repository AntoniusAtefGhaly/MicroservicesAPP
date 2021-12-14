using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics.ApiCollection.Interfaces
{
    public interface ICatalogApi
    {
        Task<IEnumerable<CatalogModel>> GetCtalog();
        Task<CatalogModel> GetCtalog(string id);
        Task<IEnumerable<CatalogModel>> GetCtalogByCatalog( string catalog);
        Task<CatalogModel> CreateCtalog(CatalogModel model);
    }
}
