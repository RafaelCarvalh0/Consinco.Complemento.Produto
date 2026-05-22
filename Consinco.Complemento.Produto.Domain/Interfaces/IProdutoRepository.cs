using Consinco.Complemento.Produto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consinco.Complemento.Produto.Domain.Interfaces
{
    /// <summary>
    /// Contrato do repositório de Produtos.
    /// </summary>
    public interface IProdutoRepository
    {
        Task<IEnumerable<ProdutoModel>> ListarAtivos();
    }
}
