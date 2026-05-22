using Consinco.Complemento.Produto.Domain.Entities;
using Consinco.Complemento.Produto.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Consinco.Complemento.Produto.UI.Forms
{
    public partial class FrmCadastro : Form
    {
        private readonly IComplementoService _complementoService;
        private readonly IProdutoService _produtoService;

        public ComplementoModel Complemento { get; set; }

        public FrmCadastro(IComplementoService complementoService, IProdutoService produtoService)
        {
            InitializeComponent();
            _complementoService = complementoService;
            _produtoService = produtoService;
        }

        private async void FrmCadastro_Load(object sender, EventArgs e)
        {
            await CarregarProdutos();
            PreencherCampos();
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            await Salvar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async Task CarregarProdutos()
        {
            try
            {
                var produtos = await _produtoService.ListarAtivos();

                cmbProduto.DisplayMember = "Descricao";
                cmbProduto.ValueMember = "Id";
                cmbProduto.DataSource = produtos.ToList();
                cmbProduto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar produtos:\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void PreencherCampos()
        {
            if (Complemento == null)
            {
                this.Text = "Novo Complemento";
                dtpDataCriacao.Value = DateTime.Today;
                return;
            }

            this.Text = "Editar Complemento";

            cmbProduto.SelectedValue = Complemento.ProdutoId;
            txtLoteFabricacao.Text = Complemento.LoteFabricacao;
            dtpDataCriacao.Value = Complemento.DataCriacao;
            txtDescricaoResumida.Text = Complemento.DescricaoResumida;

            cmbProduto.Enabled = false;
        }

        private async Task Salvar()
        {
            if (!Validar()) return;

            try
            {
                ToggleLoading(true);

                var model = new ComplementoModel
                {
                    Id = Complemento?.Id ?? 0,
                    ProdutoId = (int)cmbProduto.SelectedValue,
                    LoteFabricacao = txtLoteFabricacao.Text.Trim(),
                    DataCriacao = dtpDataCriacao.Value.Date,
                    DescricaoResumida = txtDescricaoResumida.Text.Trim()
                };

                if (Complemento == null)
                    await _complementoService.Inserir(model);
                else
                    await _complementoService.Atualizar(model);

                MessageBox.Show(
                    "Registro salvo com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao salvar o registro:\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                ToggleLoading(false);
            }
        }

        private bool Validar()
        {
            if (cmbProduto.SelectedValue == null)
            {
                MessageBox.Show("Selecione um produto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProduto.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLoteFabricacao.Text))
            {
                MessageBox.Show("Informe o Lote de Fabricação.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLoteFabricacao.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescricaoResumida.Text))
            {
                MessageBox.Show("Informe a Descrição Resumida.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescricaoResumida.Focus();
                return false;
            }

            return true;
        }

        private void ToggleLoading(bool carregando)
        {
            btnSalvar.Enabled = !carregando;
            btnCancelar.Enabled = !carregando;
            Cursor = carregando ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}

