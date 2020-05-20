using MongoDB.Bson;
using System;
using System.Text.Json.Serialization;

namespace LS.Redis.Domain.Entity
{
    public class Produto
    {
        [JsonIgnore]
        public ObjectId _id { get; set; }
        public Guid Codigo { get; private set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public double Preco { get; set; }


        public Produto()
        {
            Codigo = Guid.NewGuid();
        }
    }


    public class ProdutoVO
    {
        public Guid Codigo { get; private set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public double Preco { get; set; }
    }
}
