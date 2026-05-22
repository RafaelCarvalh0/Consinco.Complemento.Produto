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
    public class ComplementoRepository : IComplementoRepository
    {
        private readonly IUniversal _repo;

        public ComplementoRepository(IUniversal universal)
        {
            _repo = universal;
        }

        public async Task Inserir(ComplementoModel complemento)
        {
            await _repo.ExecuteNonQueryAsync(
                "CONSINCO.PRC_INSERIR_COMPLEMENTO",
                CommandType.StoredProcedure,
                new OracleParameter("p_prd_id", OracleDbType.Int32) { Value = complemento.ProdutoId },
                new OracleParameter("p_lote_fabricacao", OracleDbType.Varchar2) { Value = complemento.LoteFabricacao },
                new OracleParameter("p_data_criacao", OracleDbType.Date) { Value = complemento.DataCriacao },
                new OracleParameter("p_descricao_resumida", OracleDbType.Varchar2) { Value = complemento.DescricaoResumida }
            );
        }

        public async Task Atualizar(ComplementoModel complemento)
        {
            await _repo.ExecuteNonQueryAsync(
                "CONSINCO.PRC_ATUALIZAR_COMPLEMENTO",
                CommandType.StoredProcedure,
                new OracleParameter("p_cmp_id", OracleDbType.Int32) { Value = complemento.Id },
                new OracleParameter("p_lote_fabricacao", OracleDbType.Varchar2) { Value = complemento.LoteFabricacao },
                new OracleParameter("p_data_criacao", OracleDbType.Date) { Value = complemento.DataCriacao },
                new OracleParameter("p_descricao_resumida", OracleDbType.Varchar2) { Value = complemento.DescricaoResumida }
            );
        }

        public async Task Excluir(int id)
        {
            await _repo.ExecuteNonQueryAsync(
                "CONSINCO.PRC_EXCLUIR_COMPLEMENTO",
                CommandType.StoredProcedure,
                new OracleParameter("p_cmp_id", OracleDbType.Int32) { Value = id }
            );
        }

        public async Task<IEnumerable<ComplementoModel>> Consultar(ComplementoFiltroRequest request)
        {
            DataTable dataTable = await _repo.ExecuteDataTableAsync(
                "CONSINCO.PRC_CONSULTAR_COMPLEMENTOS",
                CommandType.StoredProcedure,
                new OracleParameter(parameterName: "p_prd_codigo", OracleDbType.Varchar2) { Value = Utils.DBNullParse(request.ProdutoCodigo) },
                new OracleParameter(parameterName: "p_prd_descricao", OracleDbType.Varchar2) { Value = Utils.DBNullParse(request.ProdutoDescricao) },
                new OracleParameter(parameterName: "p_lote_fabricacao", OracleDbType.Varchar2) { Value = Utils.DBNullParse(request.LoteFabricacao) },
                new OracleParameter(parameterName: "p_data_criacao_de", OracleDbType.Date) { Value = Utils.DBNullParse(request.DataCriacaoDe) },
                new OracleParameter(parameterName: "p_data_criacao_ate", OracleDbType.Date) { Value = Utils.DBNullParse(request.DataCriacaoAte) },
                new OracleParameter(parameterName: "p_cursor_out", OracleDbType.RefCursor) { Direction = ParameterDirection.Output }
            );

            List<ComplementoModel> model = new List<ComplementoModel>();

            foreach (DataRow row in dataTable.Rows)
                model.Add(Parse(row));
            
            return model;
        }

        private ComplementoModel Parse(DataRow row)
        {
            ComplementoModel model = new ComplementoModel
            {
                Id = Convert.ToInt32(row["CMP_ID"]),
                ProdutoId = Convert.ToInt32(row["PRD_ID"]),
                LoteFabricacao = row["CMP_LOTE_FABRICACAO"].ToString(),
                DataCriacao = Convert.ToDateTime(row["CMP_DATA_CRIACAO"]),
                DescricaoResumida = row["CMP_DESCRICAO_RESUMIDA"].ToString(),
                Produto = new ProdutoModel
                {
                    Codigo = row["PRD_CODIGO"].ToString(),
                    Descricao = row["PRD_DESCRICAO"].ToString()
                }
            };

            return model;
        }
    }
}
