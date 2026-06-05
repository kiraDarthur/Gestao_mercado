namespace GestaoMercadoApp
{
    partial class FormPesquisa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtPesquisaForm = new System.Windows.Forms.TextBox();
            this.btnBuscarForm = new System.Windows.Forms.Button();
            this.gridPesquisaForm = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridPesquisaForm)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Digite o Nome do Artigo:";
            // 
            // txtPesquisaForm
            // 
            this.txtPesquisaForm.Location = new System.Drawing.Point(291, 57);
            this.txtPesquisaForm.Name = "txtPesquisaForm";
            this.txtPesquisaForm.Size = new System.Drawing.Size(255, 20);
            this.txtPesquisaForm.TabIndex = 1;
            this.txtPesquisaForm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquisaForm_KeyDown);
            // 
            // btnBuscarForm
            // 
            this.btnBuscarForm.Location = new System.Drawing.Point(694, 57);
            this.btnBuscarForm.Name = "btnBuscarForm";
            this.btnBuscarForm.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarForm.TabIndex = 2;
            this.btnBuscarForm.Text = "Procurar";
            this.btnBuscarForm.UseVisualStyleBackColor = true;
            this.btnBuscarForm.Click += new System.EventHandler(this.btnBuscarForm_Click);
            // 
            // gridPesquisaForm
            // 
            this.gridPesquisaForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPesquisaForm.Location = new System.Drawing.Point(47, 159);
            this.gridPesquisaForm.Name = "gridPesquisaForm";
            this.gridPesquisaForm.Size = new System.Drawing.Size(686, 265);
            this.gridPesquisaForm.TabIndex = 3;
            // 
            // FormPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridPesquisaForm);
            this.Controls.Add(this.btnBuscarForm);
            this.Controls.Add(this.txtPesquisaForm);
            this.Controls.Add(this.label1);
            this.Name = "FormPesquisa";
            ((System.ComponentModel.ISupportInitialize)(this.gridPesquisaForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPesquisaForm;
        private System.Windows.Forms.Button btnBuscarForm;
        private System.Windows.Forms.DataGridView gridPesquisaForm;
    }
}