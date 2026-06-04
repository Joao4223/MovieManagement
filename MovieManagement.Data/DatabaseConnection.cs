using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Data
{
    public static class DatabaseConnection
    {
        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "movies.db");
        private static readonly string ConnectionString = $"Data Source={DbPath};";

        public static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static void InicializarBaseDeDados()
        {
            using (var conexao = GetConnection())
            {
                string queryTabelas = @"
                    CREATE TABLE IF NOT EXISTS Categorias (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL UNIQUE
                    );

                    CREATE TABLE IF NOT EXISTS Realizadores (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL UNIQUE,
                        Pais TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Filmes (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Titulo TEXT NOT NULL UNIQUE,
                        Ano INTEGER NOT NULL,
                        Lingua TEXT NOT NULL,
                        Classificacao REAL NOT NULL,
                        CategoriaId INTEGER,
                        RealizadorId INTEGER,
                        FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id) ON DELETE SET NULL,
                        FOREIGN KEY (RealizadorId) REFERENCES Realizadores(Id) ON DELETE SET NULL
                    );
                ";

                using (var comando = new SqliteCommand(queryTabelas, conexao))
                {
                    comando.ExecuteNonQuery();
                }
            }
        }

    }
}
