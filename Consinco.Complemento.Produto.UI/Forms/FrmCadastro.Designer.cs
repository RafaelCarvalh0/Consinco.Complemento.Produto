namespace Consinco.Complemento.Produto.UI.Forms
{
    partial class FrmCadastro
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
            this.grpDados = new System.Windows.Forms.GroupBox();
            this.lblProduto = new System.Windows.Forms.Label();
            this.cmbProduto = new System.Windows.Forms.ComboBox();
            this.lblLoteFabricacao = new System.Windows.Forms.Label();
            this.txtLoteFabricacao = new System.Windows.Forms.TextBox();
            this.lblDataCriacao = new System.Windows.Forms.Label();
            this.dtpDataCriacao = new System.Windows.Forms.DateTimePicker();
            this.lblDescricaoResumida = new System.Windows.Forms.Label();
            this.txtDescricaoResumida = new System.Windows.Forms.TextBox();
            this.pnlBotoes = new System.Windows.Forms.Panel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grpDados.SuspendLayout();
            this.pnlBotoes.SuspendLayout();
            this.SuspendLayout();

            // grpDados
            this.grpDados.Text = "Dados do Complemento";
            this.grpDados.Location = new System.Drawing.Point(12, 12);
            this.grpDados.Size = new System.Drawing.Size(460, 230);
            this.grpDados.Anchor = System.Windows.Forms.AnchorStyles.Top
                                   | System.Windows.Forms.AnchorStyles.Left
                                   | System.Windows.Forms.AnchorStyles.Right;
            this.grpDados.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblProduto, this.cmbProduto,
                this.lblLoteFabricacao, this.txtLoteFabricacao,
                this.lblDataCriacao, this.dtpDataCriacao,
                this.lblDescricaoResumida, this.txtDescricaoResumida
            });

            // lblProduto
            this.lblProduto.Text = "Produto: *";
            this.lblProduto.Location = new System.Drawing.Point(10, 30);
            this.lblProduto.Size = new System.Drawing.Size(120, 20);

            // cmbProduto
            this.cmbProduto.Location = new System.Drawing.Point(140, 27);
            this.cmbProduto.Size = new System.Drawing.Size(305, 23);
            this.cmbProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // lblLoteFabricacao
            this.lblLoteFabricacao.Text = "Lote Fabricação: *";
            this.lblLoteFabricacao.Location = new System.Drawing.Point(10, 70);
            this.lblLoteFabricacao.Size = new System.Drawing.Size(120, 20);

            // txtLoteFabricacao
            this.txtLoteFabricacao.Location = new System.Drawing.Point(140, 67);
            this.txtLoteFabricacao.Size = new System.Drawing.Size(200, 23);
            this.txtLoteFabricacao.MaxLength = 50;

            // lblDataCriacao
            this.lblDataCriacao.Text = "Data Criação: *";
            this.lblDataCriacao.Location = new System.Drawing.Point(10, 110);
            this.lblDataCriacao.Size = new System.Drawing.Size(120, 20);

            // dtpDataCriacao
            this.dtpDataCriacao.Location = new System.Drawing.Point(140, 107);
            this.dtpDataCriacao.Size = new System.Drawing.Size(150, 23);
            this.dtpDataCriacao.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // lblDescricaoResumida
            this.lblDescricaoResumida.Text = "Descrição Resumida: *";
            this.lblDescricaoResumida.Location = new System.Drawing.Point(10, 150);
            this.lblDescricaoResumida.Size = new System.Drawing.Size(130, 20);

            // txtDescricaoResumida
            this.txtDescricaoResumida.Location = new System.Drawing.Point(140, 147);
            this.txtDescricaoResumida.Size = new System.Drawing.Size(305, 23);
            this.txtDescricaoResumida.MaxLength = 500;
            this.txtDescricaoResumida.Multiline = true;
            this.txtDescricaoResumida.Height = 55;
            this.txtDescricaoResumida.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // pnlBotoes
            this.pnlBotoes.Location = new System.Drawing.Point(12, 255);
            this.pnlBotoes.Size = new System.Drawing.Size(460, 40);
            this.pnlBotoes.Anchor = System.Windows.Forms.AnchorStyles.Bottom
                                    | System.Windows.Forms.AnchorStyles.Right;
            this.pnlBotoes.Controls.AddRange(new System.Windows.Forms.Control[] { this.btnSalvar, this.btnCancelar });

            // btnSalvar
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Location = new System.Drawing.Point(270, 5);
            this.btnSalvar.Size = new System.Drawing.Size(90, 30);
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            // btnCancelar
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new System.Drawing.Point(368, 5);
            this.btnCancelar.Size = new System.Drawing.Size(90, 30);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // FrmCadastro
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Complemento";
            this.Load += new System.EventHandler(this.FrmCadastro_Load);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.grpDados, this.pnlBotoes });

            this.grpDados.ResumeLayout(false);
            this.pnlBotoes.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpDados;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.ComboBox cmbProduto;
        private System.Windows.Forms.Label lblLoteFabricacao;
        private System.Windows.Forms.TextBox txtLoteFabricacao;
        private System.Windows.Forms.Label lblDataCriacao;
        private System.Windows.Forms.DateTimePicker dtpDataCriacao;
        private System.Windows.Forms.Label lblDescricaoResumida;
        private System.Windows.Forms.TextBox txtDescricaoResumida;
        private System.Windows.Forms.Panel pnlBotoes;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
