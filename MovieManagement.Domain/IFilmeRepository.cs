using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain
{
    public interface IFilmeRepository
    {
        void Adicionar(Filme filme);
        List<Filme> ListarTodos();
        Filme? ProcurarPorTitulo(string titulo);
        bool Remover(int id);
    }
}
