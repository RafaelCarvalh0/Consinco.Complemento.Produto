using System;

namespace Consinco.Complemento.Produto.Domain.Entities
{
    public class ComplementoFiltroRequest
    {
        public string ProdutoCodigo { get; set; }
        public string ProdutoDescricao { get; set; }
        public string LoteFabricacao { get; set; }
        public DateTime? DataCriacaoDe { get; set; }
        public DateTime? DataCriacaoAte { get; set; }
    }
}
