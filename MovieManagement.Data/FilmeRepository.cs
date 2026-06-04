using Microsoft.Data.Sqlite;
using MovieManagement.Domain;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagement.Data
{
    public class FilmeRepository : IFilmeRepository
    {
        public void Adicionar(Filme filme)
        {
            using (var conexao = DatabaseConnection.GetConnection())
            {
                string query = @"INSERT INTO Filmes (Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId) 
                                VALUES (@Titulo, @Ano, @Lingua, @Classificacao, @CategoriaId, @RealizadorId);
                                SELECT last_insert_rowid();";

                using (var comando = new SqliteCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@Titulo", filme.Titulo);
                    comando.Parameters.AddWithValue("@Ano", filme.Ano);
                    comando.Parameters.AddWithValue("@Lingua", filme.Lingua);
                    comando.Parameters.AddWithValue("@Classificacao", filme.Classificacao);
                    comando.Parameters.AddWithValue("@CategoriaId", (object?)filme.Categoria?.Id ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@RealizadorId", (object?)filme.Realizador?.Id ?? DBNull.Value);

                    filme.Id = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public List<Filme> ListarTodos()
        {
            var lista = new List<Filme>();
            using (var conexao = DatabaseConnection.GetConnection())
            {
                string query = @"
                    SELECT f.Id, f.Titulo, f.Ano, f.Lingua, f.Classificacao, 
                           c.Id, c.Nome, 
                           r.Id, r.Nome, r.Pais
                    FROM Filmes f
                    LEFT JOIN Categorias c ON f.CategoriaId = c.Id
                    LEFT JOIN Realizadores r ON f.RealizadorId = r.Id";

                using (var comando = new SqliteCommand(query, conexao))
                using (var leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        var filme = new Filme
                        {
                            Id = leitor.GetInt32(0),
                            Titulo = leitor.GetString(1),
                            Ano = leitor.GetInt32(2),
                            Lingua = leitor.GetString(3),
                            Classificacao = leitor.GetDouble(4)
                        };

                        if (!leitor.IsDBNull(5))
                        {
                            filme.Categoria = new Categoria { Id = leitor.GetInt32(5), Nome = leitor.GetString(6) };
                        }

                        if (!leitor.IsDBNull(7))
                        {
                            filme.Realizador = new Realizador { Id = leitor.GetInt32(7), Nome = leitor.GetString(8), Pais = leitor.GetString(9) };
                        }

                        lista.Add(filme);
                    }
                }
            }
            return lista;
        }

        public Filme? ProcurarPorTitulo(string titulo)
        {
            using (var conexao = DatabaseConnection.GetConnection())
            {
                string query = @"
                    SELECT f.Id, f.Titulo, f.Ano, f.Lingua, f.Classificacao, 
                           c.Id, c.Nome, 
                           r.Id, r.Nome, r.Pais
                    FROM Filmes f
                    LEFT JOIN Categorias c ON f.CategoriaId = c.Id
                    LEFT JOIN Realizadores r ON f.RealizadorId = r.Id
                    WHERE f.Titulo = @Titulo COLLATE NOCASE";

                using (var comando = new SqliteCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@Titulo", titulo);
                    using (var leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            var filme = new Filme
                            {
                                Id = leitor.GetInt32(0),
                                Titulo = leitor.GetString(1),
                                Ano = leitor.GetInt32(2),
                                Lingua = leitor.GetString(3),
                                Classificacao = leitor.GetDouble(4)
                            };

                            if (!leitor.IsDBNull(5))
                            {
                                filme.Categoria = new Categoria { Id = leitor.GetInt32(5), Nome = leitor.GetString(6) };
                            }

                            if (!leitor.IsDBNull(7))
                            {
                                filme.Realizador = new Realizador { Id = leitor.GetInt32(7), Nome = leitor.GetString(8), Pais = leitor.GetString(9) };
                            }

                            return filme;
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
                string query = "DELETE FROM Filmes WHERE Id = @Id";
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
