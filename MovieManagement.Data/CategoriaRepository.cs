using System;
using System.Collections.Generic;
using System.Text;
using MovieManagement.Domain;

namespace MovieManagement.Data
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private static readonly List<Categoria> _categorias = new List<Categoria>();
        private static int _proximoId = 1;

        public void Adicionar(Categoria categoria)
        {
            categoria.Id = _proximoId++;
            _categorias.Add(categoria);
        }

        public List<Categoria> ListarTodas() => _categorias;

        public Categoria? ProcurarPorNome(string nome)
        {
            return _categorias.FirstOrDefault(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public bool Remover(int id)
        {
            var cat = _categorias.FirstOrDefault(c => c.Id == id);
            if (cat != null)
            {
                _categorias.Remove(cat);
                return true;
            }
            return false;
        }
    }
}
