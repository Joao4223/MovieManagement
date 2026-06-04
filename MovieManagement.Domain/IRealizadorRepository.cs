using System;
using System.Collections.Generic;
using System.Text;


namespace MovieManagement.Domain
{
    public interface IRealizadorRepository
    {
        void Adicionar(Realizador realizador);
        List<Realizador> ListarTodos();
        Realizador? ProcurarPorNome(string nome);
        bool Remover(int id);
    }
}
