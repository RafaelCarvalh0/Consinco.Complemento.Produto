using Consinco.Complemento.Produto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consinco.Complemento.Produto.Domain.Interfaces
{
    /// <summary>
    /// Contrato de acesso a dados para a entidade Complemento.
    /// Implementado na camada Data via ADO.NET + Oracle Stored Procedures.
    /// </summary>
    public interface IComplementoRepository
    {
        Task Inserir(ComplementoModel complemento);

        Task Atualizar(ComplementoModel complemento);

        Task Excluir(int id);

        Task<IEnumerable<ComplementoModel>> Consultar(ComplementoFiltroRequest filtro);
    }
}
