using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojinhaServer.Collections;
using MongoDB.Driver;

namespace LojinhaServer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IMongoCollection<Product> collection)
        {
            _collection = collection;
        }

        public async Task CreateAsync(Product product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _collection.Find(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            await _collection.ReplaceOneAsync(x => x.Id == product.Id, product);
        }
    }
}