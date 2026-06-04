using System;
using System.Collections.Generic;
using System.Text;
using MovieManagement.Domain;

namespace MovieManagement.Data
{
    public class RealizadorRepository : IRealizadorRepository
    {
        private static readonly List<Realizador> _realizadores = new List<Realizador>();
        private static int _proximoId = 1;

        public void Adicionar(Realizador realizador)
        {
            realizador.Id = _proximoId++;
            _realizadores.Add(realizador);
        }

        public List<Realizador> ListarTodos() => _realizadores;

        public Realizador? ProcurarPorNome(string nome)
        {
            return _realizadores.FirstOrDefault(r => r.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public bool Remover(int id)
        {
            var real = _realizadores.FirstOrDefault(r => r.Id == id);
            if (real != null)
            {
                _realizadores.Remove(real);
                return true;
            }
            return false;
        }
    }
}
