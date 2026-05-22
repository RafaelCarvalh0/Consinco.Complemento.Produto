using System;

namespace Consinco.Complemento.Produto.Domain.Entities
{
    public class ComplementoModel
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string LoteFabricacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string DescricaoResumida { get; set; }
        public ProdutoModel Produto { get; set; }
    }
}
