using MovieManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Business
{
    public class RealizadorService
    {
        private readonly IRealizadorRepository _realizadorRepository;

        public RealizadorService(IRealizadorRepository realizadorRepository)
        {
            _realizadorRepository = realizadorRepository;
        }

        public string AdicionarRealizador(Realizador realizador)
        {
            if (string.IsNullOrWhiteSpace(realizador.Nome))
                return "Erro: O nome do realizador é obrigatório.";

            if (string.IsNullOrWhiteSpace(realizador.Pais))
                return "Erro: O país de origem é obrigatório.";

            if (_realizadorRepository.ProcurarPorNome(realizador.Nome) != null)
                return "Erro: Já existe um realizador com este nome.";

            _realizadorRepository.Adicionar(realizador);
            return "Realizador adicionado com sucesso!";
        }

        public List<Realizador> ObterTodosRealizadores() => _realizadorRepository.ListarTodos();

        public bool EliminarRealizador(int id) => _realizadorRepository.Remover(id);
    }
}
