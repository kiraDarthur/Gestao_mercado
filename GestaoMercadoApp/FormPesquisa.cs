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

        private void btnBuscarForm_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"SELECT p.id AS 'id', p.nome AS 'nome', p.preco AS 'preco', 
                                            p.stock AS 'stock', c.nome AS 'categoria'
                                     FROM produtos p 
                                     LEFT JOIN categorias c ON p.categoria_id = c.id 
                                     WHERE p.nome LIKE @pesquisa 
                                     ORDER BY p.id ASC";

                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@pesquisa", "%" + txtPesquisaForm.Text.Trim() + "%");

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    if (gridPesquisaForm != null)
                    {
                        gridPesquisaForm.DataSource = dt;
                    }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Nenhum artigo encontrado com esse nome.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao realizar a busca: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPesquisaForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscarForm_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}