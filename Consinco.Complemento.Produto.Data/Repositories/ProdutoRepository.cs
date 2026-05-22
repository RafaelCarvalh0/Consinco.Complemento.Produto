using Consinco.Complemento.Produto.Data.Configuration;
using Consinco.Complemento.Produto.Domain.Entities;
using Consinco.Complemento.Produto.Domain.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Consinco.Complemento.Produto.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IUniversal _repo;

        public ProdutoRepository(IUniversal universal)
        {
            _repo = universal;
        }

        public async Task<IEnumerable<ProdutoModel>> ListarAtivos()
        {
            DataTable dataTable = await _repo.ExecuteDataTableAsync(
                "CONSINCO.PRC_LISTAR_PRODUTOS_ATIVOS",
                CommandType.StoredProcedure,
                new OracleParameter("p_cursor_out", OracleDbType.RefCursor) { Direction = ParameterDirection.Output }
            );

            List<ProdutoModel> model = new List<ProdutoModel>();

            foreach (DataRow row in dataTable.Rows)
                model.Add(Parse(row));

            return model;
        }

        private ProdutoModel Parse(DataRow row)
        {
            ProdutoModel model = new ProdutoModel
            {
                Id = Convert.ToInt32(row["PRD_ID"]),
                Codigo = row["PRD_CODIGO"].ToString(),
                Descricao = row["PRD_DESCRICAO"].ToString()
            };

            return model;
        }
    }
}
