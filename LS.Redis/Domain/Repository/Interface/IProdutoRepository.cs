using LS.Redis.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LS.Redis.Domain.Repository.Interface
{
    public interface IProdutoRepository
    {
        Task<Produto> Inserir(Produto produto);
        Task<IEnumerable<Produto>> GetAllProdutos();
        Task<IEnumerable<Produto>> GetByMarcaProdutos(string marca);
    }
}
