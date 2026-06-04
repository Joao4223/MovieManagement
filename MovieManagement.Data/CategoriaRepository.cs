using Microsoft.Data.Sqlite;
using MovieManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Data
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public void Adicionar(Categoria categoria)
        {
            using (var conexao = DatabaseConnection.GetConnection())
            {
                // Grava a nova categoria e descobre o ID automático criado pelo SQLite
                string query = "INSERT INTO Categorias (Nome) VALUES (@Nome); SELECT last_insert_rowid();";
                using (var comando = new SqliteCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", categoria.Nome);
                    categoria.Id = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public List<Categoria> ListarTodas()
        {
            var lista = new List<Categoria>();
            using (var conexao = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Id, Nome FROM Categorias";
                using (var comando = new SqliteCommand(query, conexao))
                using (var leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        lista.Add(new Categoria
                        {
                            Id = leitor.GetInt32(0),
                            Nome = leitor.GetString(1)
                        });
                    }
                }
            }
            return lista;
        }

        public Categoria? ProcurarPorNome(string nome)
        {
            using (var conexao = DatabaseConnection.GetConnection())
            {
                // O "COLLATE NOCASE" serve para ignorar maiúsculas/minúsculas na pesquisa
                string query = "SELECT Id, Nome FROM Categorias WHERE Nome = @Nome COLLATE NOCASE";
                using (var comando = new SqliteCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", nome);
                    using (var leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            return new Categoria
                            {
                                Id = leitor.GetInt32(0),
                                Nome = leitor.GetString(1)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool Remover(int id)
        {
            using (var conexao = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Categorias WHERE Id = @Id";
                using (var comando = new SqliteCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@Id", id);
                    int linhasAfetadas = comando.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
        }
    }
}
