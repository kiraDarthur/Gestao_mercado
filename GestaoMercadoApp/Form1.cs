using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestaoMercadoApp
{
    public partial class Form1 : Form
    {
        private string caminhoImagemSelecionada = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
            CarregarCategorias();
            LimparCampos();
        }

        // FUNÇÃO QUE CARREGA E AJUSTA A FOTO NO ECRÃ
        private void ExibirImagem(string caminho)
        {
            try
            {
                if (!string.IsNullOrEmpty(caminho) && File.Exists(caminho))
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Garante que a foto cabe no quadrado!
                    pictureBox1.Image = Image.FromFile(caminho);
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch
            {
                pictureBox1.Image = null;
            }
        }

        private void AtualizarGrid()
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"SELECT p.id, p.nome, p.preco, p.stock, c.nome AS 'categoria', p.imagem_path
                                     FROM produtos p 
                                     LEFT JOIN categorias c ON p.categoria_id = c.id 
                                     ORDER BY p.id ASC";

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(query, conexao);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar tabela: " + ex.Message);
            }
        }

        private void CarregarCategorias()
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "SELECT id, nome FROM categorias ORDER BY nome ASC";
                    MySqlDataAdapter adaptador = new MySqlDataAdapter(query, conexao);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    comboCategoria.DataSource = dt;
                    comboCategoria.DisplayMember = "nome";
                    comboCategoria.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message);
            }
        }

        // O CLIQUE DA TABELA QUE ATIVA A FOTO E PREENCHE OS CAMPOS
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow linha = dataGridView1.Rows[e.RowIndex];

                    texID.Text = linha.Cells["id"].Value?.ToString() ?? "";
                    textNome.Text = linha.Cells["nome"].Value?.ToString() ?? "";
                    textPreco.Text = linha.Cells["preco"].Value?.ToString() ?? "";
                    textStock.Text = linha.Cells["stock"].Value?.ToString() ?? "";
                    comboCategoria.Text = linha.Cells["categoria"].Value?.ToString() ?? "";

                    // Pega o caminho exato da imagem armazenada na base de dados
                    caminhoImagemSelecionada = linha.Cells["imagem_path"].Value?.ToString() ?? "";
                    ExibirImagem(caminhoImagemSelecionada);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao selecionar item: " + ex.Message);
                }
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textNome.Text) || string.IsNullOrEmpty(textPreco.Text))
            {
                MessageBox.Show("Preencha o Nome e o Preço!");
                return;
            }

            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "INSERT INTO produtos (nome, preco, stock, categoria_id, imagem_path) VALUES (@nome, @preco, @stock, @catId, @img)";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);

                    cmd.Parameters.AddWithValue("@nome", textNome.Text.Trim());
                    cmd.Parameters.AddWithValue("@preco", decimal.Parse(textPreco.Text.Replace(',', '.')));
                    cmd.Parameters.AddWithValue("@stock", string.IsNullOrEmpty(textStock.Text) ? 0 : int.Parse(textStock.Text));
                    cmd.Parameters.AddWithValue("@catId", comboCategoria.SelectedValue);
                    cmd.Parameters.AddWithValue("@img", caminhoImagemSelecionada);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produto adicionado!");

                    AtualizarGrid();
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(texID.Text))
            {
                MessageBox.Show("Selecione um produto para editar!");
                return;
            }

            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "UPDATE produtos SET nome=@nome, preco=@preco, stock=@stock, categoria_id=@catId, imagem_path=@img WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);

                    cmd.Parameters.AddWithValue("@id", texID.Text);
                    cmd.Parameters.AddWithValue("@nome", textNome.Text.Trim());
                    cmd.Parameters.AddWithValue("@preco", decimal.Parse(textPreco.Text.Replace(',', '.')));
                    cmd.Parameters.AddWithValue("@stock", int.Parse(textStock.Text));
                    cmd.Parameters.AddWithValue("@catId", comboCategoria.SelectedValue);
                    cmd.Parameters.AddWithValue("@img", caminhoImagemSelecionada);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produto atualizado!");

                    AtualizarGrid();
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(texID.Text))
            {
                MessageBox.Show("Selecione um produto para eliminar!");
                return;
            }

            if (MessageBox.Show("Deseja eliminar este produto?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conexao = Conexao.ObterConexao())
                    {
                        string query = "DELETE FROM produtos WHERE id=@id";
                        MySqlCommand cmd = new MySqlCommand(query, conexao);
                        cmd.Parameters.AddWithValue("@id", texID.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Produto eliminado!");

                        AtualizarGrid();
                        LimparCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao eliminar: " + ex.Message);
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            FormPesquisa telaPesquisa = new FormPesquisa();
            telaPesquisa.ShowDialog();
        }

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            FormHistorico telaHistorico = new FormHistorico();
            telaHistorico.ShowDialog();
        }

        private void btnCarregarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imagens (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    caminhoImagemSelecionada = ofd.FileName;
                    ExibirImagem(caminhoImagemSelecionada); // Mostra logo no ecrã ao escolher
                    MessageBox.Show("Foto associada com sucesso!");
                }
            }
        }

        private void LimparCampos()
        {
            texID.Clear();
            textNome.Clear();
            textPreco.Clear();
            textStock.Clear();
            caminhoImagemSelecionada = "";
            pictureBox1.Image = null;
            if (comboCategoria.Items.Count > 0) comboCategoria.SelectedIndex = 0;
        }
    }
}