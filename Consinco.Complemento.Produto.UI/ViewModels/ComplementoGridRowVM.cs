using Consinco.Complemento.Produto.Domain.Entities;

namespace Consinco.Complemento.Produto.UI.ViewModels
{
    public class ComplementoGridRowVM
    {
        public int Id { get; set; }
        public string ProdutoCodigo { get; set; }
        public string ProdutoDescricao { get; set; }
        public string LoteFabricacao { get; set; }
        public string DataCriacaoFormatada { get; set; }
        public string DescricaoResumida { get; set; }
        public ComplementoModel Model { get; set; }
    }
}
