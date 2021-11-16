﻿using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data.Interfaces
{
    public interface ICatalogContext
    {
       public IMongoCollection<Product> Products { get; set; } 
    }
}