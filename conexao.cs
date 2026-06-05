using System;
using MySql.Data.MySqlClient; // Ativa as ferramentas do MySQL

namespace GestaoMercadoApp
{
    public class Conexao
    {
        // Linha de conexão que aponta diretamente para a tua base de dados do XAMPP
        private static string connectionString = "server=localhost;database=gestao_mercado;uid=root;pwd=;";

        // Método que o teu formulário vai usar para abrir a ligação
        public static MySqlConnection ObterConexao()
        {
            MySqlConnection conexao = new MySqlConnection(connectionString);
            try
            {
                conexao.Open();
                return conexao;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ligar à Base de Dados: " + ex.Message);
            }
        }
    }
}