using Consinco.Complemento.Produto.Domain.Entities;
using Consinco.Complemento.Produto.Domain.Interfaces.Services;
using Consinco.Complemento.Produto.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Consinco.Complemento.Produto.UI.Forms
{
    public partial class FrmListagem : Form
    {
        private readonly IComplementoService _complementoService;

        public FrmListagem(IComplementoService complementoService)
        {
            InitializeComponent();
            _complementoService = complementoService;
        }

        private void FrmListagem_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
        }

        private async void btnPesquisar_Click(object sender, EventArgs e)
        {
            await Pesquisar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            AbrirCadastro(null);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparFiltros();
        }

        private void ConfigurarGrid()
        {
            dgvComplementos.AutoGenerateColumns = false;
            dgvComplementos.AllowUserToAddRows = false;
            dgvComplementos.ReadOnly = true;
            dgvComplementos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvComplementos.RowHeadersVisible = false;
            dgvComplementos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvComplementos.Columns.Clear();

            dgvComplementos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            dgvComplementos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCodigo",
                HeaderText = "Código",
                DataPropertyName = "ProdutoCodigo",
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            dgvComplementos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDescricao",
                HeaderText = "Descrição do Produto",
                DataPropertyName = "ProdutoDescricao"
            });

            dgvComplementos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLote",
                HeaderText = "Lote de Fabricação",
                DataPropertyName = "LoteFabricacao",
                Width = 130,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            dgvComplementos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colData",
                HeaderText = "Data Criação",
                DataPropertyName = "DataCriacaoFormatada",
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            dgvComplementos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDescricaoResumida",
                HeaderText = "Descrição Resumida",
                DataPropertyName = "DescricaoResumida"
            });

            var colEditar = new DataGridViewButtonColumn
            {
                Name = "colEditar",
                HeaderText = "",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                Width = 70,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvComplementos.Columns.Add(colEditar);

            var colExcluir = new DataGridViewButtonColumn
            {
                Name = "colExcluir",
                HeaderText = "",
                Text = "Excluir",
                UseColumnTextForButtonValue = true,
                Width = 70,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvComplementos.Columns.Add(colExcluir);

            dgvComplementos.CellClick += dgvComplementos_CellClick;
        }

        private async Task Pesquisar()
        {
            if (!FiltroValido())
            {
                MessageBox.Show(
                    "Preencha ao menos um filtro para realizar a pesquisa.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                ToggleLoading(true);

                var filtro = new ComplementoFiltroRequest
                {
                    ProdutoCodigo = txtCodigo.Text.Trim(),
                    ProdutoDescricao = txtDescricao.Text.Trim(),
                    LoteFabricacao = txtLote.Text.Trim(),
                    DataCriacaoDe = dtpDe.Checked ? dtpDe.Value.Date : (DateTime?)null,
                    DataCriacaoAte = dtpAte.Checked ? dtpAte.Value.Date : (DateTime?)null
                };

                var resultado = await _complementoService.Consultar(filtro);
                var lista = resultado.ToList();

                CarregarGrid(lista);

                lblTotal.Text = $"{lista.Count} registro(s) encontrado(s).";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao realizar a pesquisa:\n{ex.Message}",
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

        private void CarregarGrid(List<ComplementoModel> lista)
        {
            List<ComplementoGridRowVM> listaVM = lista.Select(c => new ComplementoGridRowVM
            {
                Id = c.Id,
                ProdutoCodigo = c.Produto?.Codigo,
                ProdutoDescricao = c.Produto?.Descricao,
                LoteFabricacao = c.LoteFabricacao,
                DataCriacaoFormatada = c.DataCriacao.ToString("dd/MM/yyyy"),
                DescricaoResumida = c.DescricaoResumida,
                Model = c
            }).ToList();

            dgvComplementos.DataSource = listaVM;
        }

        private async void dgvComplementos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (!(dgvComplementos.Rows[e.RowIndex].DataBoundItem is ComplementoGridRowVM row)) return;

            if (e.ColumnIndex == dgvComplementos.Columns["colEditar"].Index)
            {
                AbrirCadastro(row.Model);
            }
            else if (e.ColumnIndex == dgvComplementos.Columns["colExcluir"].Index)
            {
                await Excluir(row.Model);
            }
        }

        private void AbrirCadastro(ComplementoModel model)
        {
            var frm = Program.ServiceProvider.GetRequiredService<FrmCadastro>();
            frm.Complemento = model;

            if (frm.ShowDialog() == DialogResult.OK)
                _ = Pesquisar();
        }

        private async Task Excluir(ComplementoModel model)
        {
            var confirmacao = MessageBox.Show(
                $"Deseja excluir o complemento do produto '{model.Produto?.Descricao}'?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacao != DialogResult.Yes) return;

            try
            {
                await _complementoService.Excluir(model.Id);

                MessageBox.Show("Registro excluído com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await Pesquisar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao excluir o registro:\n{ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool FiltroValido()
        {
            return !string.IsNullOrWhiteSpace(txtCodigo.Text)
                || !string.IsNullOrWhiteSpace(txtDescricao.Text)
                || !string.IsNullOrWhiteSpace(txtLote.Text)
                || dtpDe.Checked
                || dtpAte.Checked;
        }

        private void LimparFiltros()
        {
            txtCodigo.Clear();
            txtDescricao.Clear();
            txtLote.Clear();
            dtpDe.Checked = false;
            dtpAte.Checked = false;
            dgvComplementos.DataSource = null;
            lblTotal.Text = string.Empty;
        }

        private void ToggleLoading(bool carregando)
        {
            btnPesquisar.Enabled = !carregando;
            btnNovo.Enabled = !carregando;
            Cursor = carregando ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}
