using Consinco.Complemento.Produto.Domain.Entities;
using Consinco.Complemento.Produto.Domain.Interfaces;
using Consinco.Complemento.Produto.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consinco.Complemento.Produto.Application.Services
{
    public class ComplementoService : IComplementoService
    {
        private readonly IComplementoRepository _repo;

        public ComplementoService(IComplementoRepository repo)
        {
            _repo = repo;
        }

        public async Task Inserir(ComplementoModel complemento)
        {
            ValidarComplemento(complemento);

            await _repo.Inserir(complemento);
        }

        public async Task Atualizar(ComplementoModel complemento)
        {
            if (complemento.Id <= 0)
                throw new ArgumentException("ID do complemento inválido para atualização.");

            ValidarComplemento(complemento);

            await _repo.Atualizar(complemento);
        }

        public async Task Excluir(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID do complemento inválido para exclusão.");

            await _repo.Excluir(id);
        }

        public async Task<IEnumerable<ComplementoModel>> Consultar(ComplementoFiltroRequest filtro)
        {
            if (filtro == null)
                throw new ArgumentNullException(nameof(filtro), "O filtro de consulta não pode ser nulo.");

            if (!FiltroTemAoMenosUmCampo(filtro))
                throw new ArgumentException("Informe ao menos um filtro para realizar a consulta.");

            return await _repo.Consultar(filtro);
        }

        private void ValidarComplemento(ComplementoModel complemento)
        {
            if (complemento == null)
                throw new ArgumentNullException(nameof(complemento));

            if (complemento.ProdutoId <= 0)
                throw new ArgumentException("Produto não informado ou inválido.");

            if (string.IsNullOrWhiteSpace(complemento.LoteFabricacao))
                throw new ArgumentException("Lote de fabricação é obrigatório.");

            if (complemento.DataCriacao == default)
                throw new ArgumentException("Data de criação é obrigatória.");

            if (string.IsNullOrWhiteSpace(complemento.DescricaoResumida))
                throw new ArgumentException("Descrição resumida é obrigatória.");

            if (complemento.DescricaoResumida.Length > 500)
                throw new ArgumentException("Descrição resumida não pode ultrapassar 500 caracteres.");
        }

        private bool FiltroTemAoMenosUmCampo(ComplementoFiltroRequest filtro)
        {
            return !string.IsNullOrWhiteSpace(filtro.ProdutoCodigo)
                || !string.IsNullOrWhiteSpace(filtro.ProdutoDescricao)
                || !string.IsNullOrWhiteSpace(filtro.LoteFabricacao)
                || filtro.DataCriacaoDe.HasValue
                || filtro.DataCriacaoAte.HasValue;
        }
    }
}
