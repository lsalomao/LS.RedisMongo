using LS.Redis.Domain.Entity;
using LS.Redis.Domain.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.Redis.Domain.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private IMongoDatabase mongoDatabase;

        public ProdutoRepository(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutos()
        {
            return await mongoDatabase.GetCollection<Produto>("Produto").Find(Builders<Produto>.Filter.Empty).ToListAsync();
        }
        
        public async Task<IEnumerable<Produto>> GetByMarcaProdutos(string marca)
        {
            var filter = Builders<Produto>.Filter.Eq("Marca", marca);

            return await mongoDatabase.GetCollection<Produto>("Produto").Find(filter).ToListAsync();
        }

        public async Task<Produto> Inserir(Produto produto)
        {
            var colletions = mongoDatabase.GetCollection<Produto>("Produto");

            await colletions.InsertOneAsync(produto);

            return produto;
        }
    }
}
