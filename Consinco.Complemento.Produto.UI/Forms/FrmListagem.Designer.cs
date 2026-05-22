namespace Consinco.Complemento.Produto.UI.Forms
{
    partial class FrmListagem
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlFiltros = new System.Windows.Forms.GroupBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.lblLote = new System.Windows.Forms.Label();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.lblDe = new System.Windows.Forms.Label();
            this.dtpDe = new System.Windows.Forms.DateTimePicker();
            this.lblAte = new System.Windows.Forms.Label();
            this.dtpAte = new System.Windows.Forms.DateTimePicker();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.pnlAcoes = new System.Windows.Forms.Panel();
            this.btnNovo = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgvComplementos = new System.Windows.Forms.DataGridView();
            this.pnlFiltros.SuspendLayout();
            this.pnlAcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComplementos)).BeginInit();
            this.SuspendLayout();

            // pnlFiltros
            this.pnlFiltros.Text = "Filtros";
            this.pnlFiltros.Location = new System.Drawing.Point(12, 12);
            this.pnlFiltros.Size = new System.Drawing.Size(960, 110);
            this.pnlFiltros.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlFiltros.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblCodigo, this.txtCodigo,
                this.lblDescricao, this.txtDescricao,
                this.lblLote, this.txtLote,
                this.lblDe, this.dtpDe,
                this.lblAte, this.dtpAte,
                this.btnPesquisar, this.btnLimpar
            });

            // lblCodigo
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.Location = new System.Drawing.Point(10, 25);
            this.lblCodigo.Size = new System.Drawing.Size(50, 20);

            // txtCodigo
            this.txtCodigo.Location = new System.Drawing.Point(65, 22);
            this.txtCodigo.Size = new System.Drawing.Size(120, 23);

            // lblDescricao
            this.lblDescricao.Text = "Descrição:";
            this.lblDescricao.Location = new System.Drawing.Point(200, 25);
            this.lblDescricao.Size = new System.Drawing.Size(65, 20);

            // txtDescricao
            this.txtDescricao.Location = new System.Drawing.Point(270, 22);
            this.txtDescricao.Size = new System.Drawing.Size(200, 23);

            // lblLote
            this.lblLote.Text = "Lote:";
            this.lblLote.Location = new System.Drawing.Point(485, 25);
            this.lblLote.Size = new System.Drawing.Size(35, 20);

            // txtLote
            this.txtLote.Location = new System.Drawing.Point(525, 22);
            this.txtLote.Size = new System.Drawing.Size(150, 23);

            // lblDe
            this.lblDe.Text = "Data De:";
            this.lblDe.Location = new System.Drawing.Point(10, 65);
            this.lblDe.Size = new System.Drawing.Size(55, 20);

            // dtpDe
            this.dtpDe.Location = new System.Drawing.Point(70, 62);
            this.dtpDe.Size = new System.Drawing.Size(130, 23);
            this.dtpDe.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDe.ShowCheckBox = true;
            this.dtpDe.Checked = false;

            // lblAte
            this.lblAte.Text = "Data Até:";
            this.lblAte.Location = new System.Drawing.Point(215, 65);
            this.lblAte.Size = new System.Drawing.Size(55, 20);

            // dtpAte
            this.dtpAte.Location = new System.Drawing.Point(275, 62);
            this.dtpAte.Size = new System.Drawing.Size(130, 23);
            this.dtpAte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAte.ShowCheckBox = true;
            this.dtpAte.Checked = false;

            // btnPesquisar
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.Location = new System.Drawing.Point(770, 25);
            this.btnPesquisar.Size = new System.Drawing.Size(90, 30);
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);

            // btnLimpar
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.Location = new System.Drawing.Point(870, 25);
            this.btnLimpar.Size = new System.Drawing.Size(80, 30);
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);

            // pnlAcoes
            this.pnlAcoes.Location = new System.Drawing.Point(12, 130);
            this.pnlAcoes.Size = new System.Drawing.Size(960, 35);
            this.pnlAcoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlAcoes.Controls.AddRange(new System.Windows.Forms.Control[] { this.btnNovo, this.lblTotal });

            // btnNovo
            this.btnNovo.Text = "+ Novo";
            this.btnNovo.Location = new System.Drawing.Point(0, 3);
            this.btnNovo.Size = new System.Drawing.Size(90, 28);
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);

            // lblTotal
            this.lblTotal.Location = new System.Drawing.Point(100, 8);
            this.lblTotal.Size = new System.Drawing.Size(300, 20);
            this.lblTotal.ForeColor = System.Drawing.Color.Gray;

            // dgvComplementos
            this.dgvComplementos.Location = new System.Drawing.Point(12, 170);
            this.dgvComplementos.Size = new System.Drawing.Size(960, 400);
            this.dgvComplementos.Anchor = System.Windows.Forms.AnchorStyles.Top
                                          | System.Windows.Forms.AnchorStyles.Bottom
                                          | System.Windows.Forms.AnchorStyles.Left
                                          | System.Windows.Forms.AnchorStyles.Right;

            // FrmListagem
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 591);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Text = "Consinco — Complemento de Produtos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmListagem_Load);
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.pnlFiltros,
                this.pnlAcoes,
                this.dgvComplementos
            });

            this.pnlFiltros.ResumeLayout(false);
            this.pnlAcoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComplementos)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox pnlFiltros;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label lblLote;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.Label lblDe;
        private System.Windows.Forms.DateTimePicker dtpDe;
        private System.Windows.Forms.Label lblAte;
        private System.Windows.Forms.DateTimePicker dtpAte;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Panel pnlAcoes;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dgvComplementos;
    }
}
