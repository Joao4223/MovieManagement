using MovieManagement.Domain;

namespace MovieManagement.Business
{
    public class FilmeService
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IRealizadorRepository _realizadorRepository;


        
        public FilmeService(
            IFilmeRepository filmeRepository,
            ICategoriaRepository categoriaRepository,
            IRealizadorRepository realizadorRepository)
        {
            _filmeRepository = filmeRepository;
            _categoriaRepository = categoriaRepository;
            _realizadorRepository = realizadorRepository;
        }

        public string AdicionarFilme(Filme filme, string nomeCategoria, string nomeRealizador)
        {
            // Validações básicas da Parte 1
            if (string.IsNullOrWhiteSpace(filme.Titulo))
                return "Erro: O título do filme é obrigatório.";

            if (_filmeRepository.ProcurarPorTitulo(filme.Titulo) != null)
                return "Erro: Já existe um filme registado com este título.";

            if (filme.Classificacao < 0 || filme.Classificacao > 5)
                return "Erro: A classificação deve situar-se entre 0 e 5.";

            // Validações de Associação (Parte 3)
            var categoriaExistente = _categoriaRepository.ProcurarPorNome(nomeCategoria);
            if (categoriaExistente == null)
                return $"Erro: A categoria '{nomeCategoria}' não existe. Crie-a primeiro.";

            var realizadorExistente = _realizadorRepository.ProcurarPorNome(nomeRealizador);
            if (realizadorExistente == null)
                return $"Erro: O realizador '{nomeRealizador}' não existe. Crie-o primeiro.";

            // Fazer a associação
            filme.Categoria = categoriaExistente;
            filme.Realizador = realizadorExistente;

            _filmeRepository.Adicionar(filme);
            return "Filme adicionado e associado com sucesso!";
        }

        public List<Filme> ObterTodosFilmes() => _filmeRepository.ListarTodos();

        public Filme? PesquisarFilme(string titulo) => _filmeRepository.ProcurarPorTitulo(titulo);

        public bool EliminarFilme(int id) => _filmeRepository.Remover(id);

    }
}
