using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco
{
    internal class ArtistaDAL
    {
        //Método Listar
        public IEnumerable<Artista> Listar()
        {
            //criar a lista
            var lista = new List<Artista>();
            using var conn = new Connection().ObterConecao();
            conn.Open();

            string sql = "SELECT * FROM Artistas";
            SqlCommand command = new SqlCommand(sql, conn);
            using SqlDataReader dataReader = command.ExecuteReader();

            //Verificar o que vai ler na tabela
            while (dataReader.Read())
            {
                string nomeArtista = Convert.ToString(dataReader["Nome"]);
                string bioArtista = Convert.ToString(dataReader["Bio"]);
                // ID é diferente
                int idArtista = Convert.ToInt32(dataReader["Id"]);
                //Criar o artista
                Artista artista = new Artista(nomeArtista, bioArtista) { Id = idArtista };
                //Adicionar o artista na lista
                lista.Add(artista);
            }
            // fora do while - retornar a lista
            return lista;

        }
        //Método Adicionar
        public void Adicionar(Artista artista)
        {
            //conexão
            using var conn = new Connection().ObterConecao();
            conn.Open();
            //string sql 
            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
            //command
            SqlCommand command = new SqlCommand(sql, conn);
            //Adicionar os parametros
            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);
            //variavel de retorno
            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }
        //Método Atualizar
        public void Atualizar(Artista artista) 
        {
            //conexão
            using var conn = new Connection().ObterConecao();
            conn.Open();
            //string sql 
            string sql = $"UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";
            //command
            SqlCommand command = new SqlCommand(sql, conn);
            //Adicionar os parametros
            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@bio", artista.Bio);
            command.Parameters.AddWithValue("@id", artista.Id);
            //variavel de retorno
            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");

        }
        public void Deletar(Artista artista)
        {
            //conexão
            using var conn = new Connection().ObterConecao();
            conn.Open();
            //string sql 
            string sql = $"DELETE FROM Artistas WHERE Id = @id";
            //command
            SqlCommand command = new SqlCommand(sql, conn);
            //parametros
            command.Parameters.AddWithValue("@id", artista.Id);
            //retorno
            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");

        }
    }
}
