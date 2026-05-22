using Consinco.Complemento.Produto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consinco.Complemento.Produto.Domain.Interfaces.Services
{
    /// <summary>
    /// Contrato de serviço para operações de negócio da entidade Complemento.
    /// Implementado na camada Application.
    /// </summary>
    public interface IComplementoService
    {
        Task Inserir(ComplementoModel complemento);

        Task Atualizar(ComplementoModel complemento);

        Task Excluir(int id);

        Task<IEnumerable<ComplementoModel>> Consultar(ComplementoFiltroRequest filtro);
    }
}
