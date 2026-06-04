using MovieManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Business
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public string AdicionarCategoria(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
                return "Erro: O nome da categoria é obrigatório.";

            if (_categoriaRepository.ProcurarPorNome(categoria.Nome) != null)
                return "Erro: Já existe uma categoria com este nome.";

            _categoriaRepository.Adicionar(categoria);
            return "Categoria adicionada com sucesso!";
        }

        public List<Categoria> ObterTodasCategorias() => _categoriaRepository.ListarTodas();

        public bool EliminarCategoria(int id) => _categoriaRepository.Remover(id);
    }
}
