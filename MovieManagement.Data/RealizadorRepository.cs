using Microsoft.Data.Sqlite;
using MovieManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Data
{
    public class RealizadorRepository : IRealizadorRepository
    {
        public void Adicionar(Realizador realizador)
        {
            using (var conexao = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Realizadores (Nome, Pais) VALUES (@Nome, @Pais); SELECT last_insert_rowid();";
                using (var comando = new SqliteCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", realizador.Nome);
                    comando.Parameters.AddWithValue("@Pais", realizador.Pais);
                    realizador.Id = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public List<Realizador> ListarTodos()
        {
            var lista = new List<Realizador>();
            using (var conexao = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Id, Nome, Pais FROM Realizadores";
                using (var comando = new SqliteCommand(query, conexao))
                using (var leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        lista.Add(new Realizador
                        {
                            Id = leitor.GetInt32(0),
                            Nome = leitor.GetString(1),
                            Pais = leitor.GetString(2)
                        });
                    }
                }
            }
            return lista;
        }

        public Realizador? ProcurarPorNome(string nome)
        {
            using (var conexao = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Id, Nome, Pais FROM Realizadores WHERE Nome = @Nome COLLATE NOCASE";
                using (var comando = new SqliteCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", nome);
                    using (var leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            return new Realizador
                            {
                                Id = leitor.GetInt32(0),
                                Nome = leitor.GetString(1),
                                Pais = leitor.GetString(2)
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
                string query = "DELETE FROM Realizadores WHERE Id = @Id";
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
