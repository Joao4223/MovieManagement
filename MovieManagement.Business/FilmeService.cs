using MovieManagement.Domain;

namespace MovieManagement.Business
{
    public class FilmeService
    {
        private readonly IFilmeRepository _filmeRepository;

        // O serviço de filmes recebe a interface (Inversão de Dependência)
        public FilmeService(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public string AdicionarFilme(Filme filme)
        {
            // Regra de Negócio: Título obrigatório
            if (string.IsNullOrWhiteSpace(filme.Titulo))
                return "Erro: O título do filme é obrigatório.";

            // Regra de Negócio: Não pode existir duplicado
            if (_filmeRepository.ProcurarPorTitulo(filme.Titulo) != null)
                return "Erro: Já existe um filme registado com este título.";

            // Regra de Negócio: Classificação entre 0 e 5
            if (filme.Classificacao < 0 || filme.Classificacao > 5)
                return "Erro: A classificação deve situar-se entre 0 e 5.";

            _filmeRepository.Adicionar(filme);
            return "Filme adicionado com sucesso!";
        }

        public List<Filme> ObterTodosFilmes()
        {
            return _filmeRepository.ListarTodos();
        }

        public Filme? PesquisarFilme(string titulo)
        {
            return _filmeRepository.ProcurarPorTitulo(titulo);
        }

        public bool EliminarFilme(int id)
        {
            return _filmeRepository.Remover(id);
        }

    }
}
