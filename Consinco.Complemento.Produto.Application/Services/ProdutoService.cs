using Consinco.Complemento.Produto.Domain.Entities;
using Consinco.Complemento.Produto.Domain.Interfaces;
using Consinco.Complemento.Produto.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consinco.Complemento.Produto.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repo;

        public ProdutoService(IProdutoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProdutoModel>> ListarAtivos()
        {
            return await _repo.ListarAtivos();
        }
    }
}
