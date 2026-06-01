using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestaoMercadoApp
{
    public partial class FormPesquisa : Form
    {
        public FormPesquisa()
        {
            InitializeComponent();
        }

        // =========================================================================
        // OPÇÃO 1: PESQUISAR CLICANDO COM O RATO NO BOTÃO "PROCURAR"
        // =========================================================================
        private void btnBuscarForm_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    // Query corrigida para não quebrar acentos no Windows
                    string query = @"SELECT p.id AS 'ID', 
                                            p.nome AS 'NomeProduto', 
                                            p.preco AS 'PrecoProduto', 
                                            p.stock AS 'Stock', 
                                            c.nome AS 'Categoria'
                                     FROM produtos p 
                                     LEFT JOIN categorias c ON p.categoria_id = c.id 
                                     WHERE p.nome LIKE @pesquisa
                                     ORDER BY p.id ASC";

                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@pesquisa", "%" + txtPesquisaForm.Text.Trim() + "%");

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    gridPesquisaForm.DataSource = dt;

                    // Ajusta os nomes das colunas de forma bonita e limpa no ecrã
                    if (gridPesquisaForm.Columns.Count >= 5)
                    {
                        gridPesquisaForm.Columns[1].HeaderText = "Nome do Artigo";
                        gridPesquisaForm.Columns[2].HeaderText = "Preço (€)";
                    }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Nenhum produto encontrado com esse nome.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao realizar a busca: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        // PESQUISAR CARREGANDO NA TECLA "ENTER" DO TECLADO
       
        private void txtPesquisaForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Se o utilizador carregar na tecla Enter...
            if (e.KeyCode == Keys.Enter)
            {
                // Dispara o clique do botão procurar automaticamente!
                btnBuscarForm_Click(sender, e);

                // Bloqueia o som de erro ("beep") padrão do Windowss
                e.SuppressKeyPress = true;
            }
        }
    }
}