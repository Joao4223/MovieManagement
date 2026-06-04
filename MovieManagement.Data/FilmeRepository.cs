using MovieManagement.Domain;
using System.Collections.Generic;
using System.Linq;
namespace MovieManagement.Data
{
    public class FilmeRepository : IFilmeRepository
    {
        // Lista em memória estática para manter os dados vivos enquanto o programa corre
        private static readonly List<Filme> _filmes = new List<Filme>();
        private static int _proximoId = 1;

        public void Adicionar(Filme filme)
        {
            filme.Id = _proximoId++;
            _filmes.Add(filme);
        }

        public List<Filme> ListarTodos()
        {
            return _filmes;
        }

        public Filme? ProcurarPorTitulo(string titulo) // Nota o ? em Filme?
        {
            return _filmes.FirstOrDefault(f => f.Titulo.Equals(titulo, System.StringComparison.OrdinalIgnoreCase));
        }

        public bool Remover(int id)
        {
            var filme = _filmes.FirstOrDefault(f => f.Id == id);
            if (filme != null)
            {
                _filmes.Remove(filme);
                return true;
            }
            return false;
        }
    }
}
