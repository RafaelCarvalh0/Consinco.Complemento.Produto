using Consinco.Complemento.Produto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consinco.Complemento.Produto.Domain.Interfaces.Services
{
    /// <summary>
    /// Contrato do serviço de Produtos.
    /// </summary>
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoModel>> ListarAtivos();
    }
}
