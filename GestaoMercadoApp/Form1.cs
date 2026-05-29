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
            // O ID continua bloqueado para escrita manual, pois o MySQL gera-o automaticamente
            texID.ReadOnly = true;
            texID.BackColor = SystemColors.InactiveCaption;

            CarregarCategorias();
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"SELECT p.id, p.nome, p.preco, p.stock, c.nome AS categoria, p.imagem_path 
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
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void CarregarCategorias()
        {
            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "SELECT id, nome FROM categorias ORDER BY nome ASC";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBoxCategoria.DisplayMember = "nome";
                    comboBoxCategoria.ValueMember = "id";
                    comboBoxCategoria.DataSource = dt;

                    comboBoxCategoria.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message);
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // 1. Validar campos vazios obrigatórios
            if (string.IsNullOrWhiteSpace(textNome.Text) || string.IsNullOrWhiteSpace(textPreco.Text) || comboBoxCategoria.SelectedValue == null)
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios (Nome, Preço e Categoria)!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // [VALIDAÇÃO DE NÚMEROS NO NOME REMOVIDA] - Agora podes escrever "Coca-Cola 2L", "Arroz 1kg", etc.

            // 2. Validação do Preço
            string precoTexto = textPreco.Text.Replace(',', '.');
            if (!double.TryParse(precoTexto, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double preco) || preco < 0)
            {
                MessageBox.Show("O Preço introduzido é inválido! Não utilize letras e use valores positivos.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Validação do Stock
            if (!int.TryParse(textStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("O Stock Inicial deve ser um número inteiro positivo!", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"INSERT INTO produtos (nome, preco, stock, imagem_path, categoria_id) 
                                   VALUES (@nome, @preco, @stock, @imagem, @catId)";

                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@nome", textNome.Text.Trim());
                    cmd.Parameters.AddWithValue("@preco", preco);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.Parameters.AddWithValue("@imagem", caminhoImagemSelecionada);
                    cmd.Parameters.AddWithValue("@catId", comboBoxCategoria.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produto adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AtualizarGrid();
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir na base de dados: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(texID.Text))
            {
                MessageBox.Show("Selecione um produto na tabela para editar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textNome.Text) || string.IsNullOrWhiteSpace(textPreco.Text) || comboBoxCategoria.SelectedValue == null)
            {
                MessageBox.Show("Por favor, preencha o Nome, o Preço e a Categoria!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // [VALIDAÇÃO DE NÚMEROS NO NOME REMOVIDA] - Agora podes editar para nomes com números.

            string precoTexto = textPreco.Text.Replace(',', '.');
            if (!double.TryParse(precoTexto, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double preco) || preco < 0)
            {
                MessageBox.Show("O Preço introduzido é inválido!", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("O Stock deve ser um número inteiro positivo!", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"UPDATE produtos SET nome=@nome, preco=@preco, stock=@stock, 
                                     imagem_path=@imagem, categoria_id=@catId WHERE id=@id";

                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@id", texID.Text);
                    cmd.Parameters.AddWithValue("@nome", textNome.Text.Trim());
                    cmd.Parameters.AddWithValue("@preco", preco);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.Parameters.AddWithValue("@imagem", caminhoImagemSelecionada);
                    cmd.Parameters.AddWithValue("@catId", comboBoxCategoria.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produto updated com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                MessageBox.Show("Selecione um produto na tabela para eliminar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja eliminar este artigo definitivamente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.No) return;

            try
            {
                using (MySqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "DELETE FROM produtos WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@id", texID.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produto eliminado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AtualizarGrid();
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao eliminar: " + ex.Message);
            }
        }

        private void btnCarregarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imagens (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                ofd.Title = "Selecione a foto do produto";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    caminhoImagemSelecionada = ofd.FileName;

                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }

                    pictureBox1.Image = Image.FromFile(caminhoImagemSelecionada);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                texID.Text = row.Cells["id"].Value.ToString();
                textNome.Text = row.Cells["nome"].Value.ToString();
                textPreco.Text = row.Cells["preco"].Value.ToString();
                textStock.Text = row.Cells["stock"].Value.ToString();

                if (row.Cells["categoria"].Value != null)
                {
                    comboBoxCategoria.Text = row.Cells["categoria"].Value.ToString();
                }

                if (row.Cells["imagem_path"].Value != null)
                {
                    string imgPath = row.Cells["imagem_path"].Value.ToString();

                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }

                    if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                    {
                        pictureBox1.Image = Image.FromFile(imgPath);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        caminhoImagemSelecionada = imgPath;
                    }
                    else
                    {
                        caminhoImagemSelecionada = "";
                    }
                }
            }
        }

        private void LimparCampos()
        {
            texID.Clear();
            textNome.Clear();
            textPreco.Clear();
            textStock.Clear();
            comboBoxCategoria.SelectedIndex = -1;

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            caminhoImagemSelecionada = "";
        }
    }
}