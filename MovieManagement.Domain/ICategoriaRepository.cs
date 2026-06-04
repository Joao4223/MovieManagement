using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain
{
    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        List<Categoria> ListarTodas();
        Categoria? ProcurarPorNome(string nome);
        bool Remover(int id);
    }
}
