using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestaoMercadoApp
{
    public partial class FormHistorico : Form
    {
        public FormHistorico()
        {
            InitializeComponent();
        }

        private void FormHistorico_Load_1(object sender, EventArgs e)
        {
            CarregarDadosHistorico();
        }

        private void FormHistorico_Load(object sender, EventArgs e)
        {
            CarregarDadosHistorico();
        }

        private void CarregarDadosHistorico()
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    // Usamos o * para o MySQL trazer todas as colunas existentes, evitando erros de nomes!
                    string query = "SELECT * FROM vendas ORDER BY id DESC";

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(query, conexao);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    gridHistorico.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar histórico: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}