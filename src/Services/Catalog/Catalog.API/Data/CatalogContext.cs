using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IConfiguration configuration;
        public IMongoCollection<Product> Products { get; }

        public CatalogContext( IConfiguration configuration)
        {
          
            this.configuration = configuration;
            var client = new MongoClient(this.configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            // following line will get the databse from mongo. if no db exists it will create.
            var database = client.GetDatabase(this.configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

           Products = database.GetCollection<Product>(this.configuration.GetValue<string>("DatabaseSettings:CollectionName"));
           
            // following will seed table with some initial data.
            CatalogContextSeed.Seed(Products);
        }
        
    }
}
