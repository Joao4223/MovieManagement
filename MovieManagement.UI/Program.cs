using MovieManagement.Business;
using MovieManagement.Data;
using MovieManagement.Domain;

namespace MovieManagement.UI
{
    internal class Program
    {
        private static FilmeService? _filmeService;
        static void Main(string[] args)
        {
            // Instanciar o repositório em memória e passar para o serviço
            IFilmeRepository repository = new FilmeRepository();
            _filmeService = new FilmeService(repository);

            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== MovieManagement - Menu ===");
                Console.WriteLine("1. Adicionar Filme");
                Console.WriteLine("2. Listar Filmes");
                Console.WriteLine("3. Procurar Filme por Título");
                Console.WriteLine("4. Remover Filme");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                switch (opcao)
                {
                    case 1: MenuAdicionar(); break;
                    case 2: MenuListar(); break;
                    case 3: MenuProcurar(); break;
                    case 4: MenuRemover(); break;
                }
                if (opcao != 0) { Console.WriteLine("\nPressione qualquer tecla para continuar..."); Console.ReadKey(); }

            } while (opcao != 0);
        }

        static void MenuAdicionar()
        {
            Console.Clear();
            Console.WriteLine("--- Adicionar Novo Filme ---");
            Filme novo = new Filme();

            Console.Write("Título: "); novo.Titulo = Console.ReadLine();
            Console.Write("Ano: "); int.TryParse(Console.ReadLine(), out int ano); novo.Ano = ano;
            Console.Write("Língua: "); novo.Lingua = Console.ReadLine();
            Console.Write("Classificação (0-5): "); double.TryParse(Console.ReadLine(), out double classif); novo.Classificacao = classif;

            string resultado = _filmeService.AdicionarFilme(novo);
            Console.WriteLine(resultado);
        }

        static void MenuListar()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Filmes ---");
            var lista = _filmeService.ObterTodosFilmes();
            if (lista.Count == 0) Console.WriteLine("Nenhum filme registado.");
            foreach (var f in lista)
                Console.WriteLine($"[{f.Id}] {f.Titulo} ({f.Ano}) - {f.Lingua} | Nota: {f.Classificacao}/5");
        }

        static void MenuProcurar()
        {
            Console.Clear();
            Console.WriteLine("--- Procurar Filme ---");
            Console.Write("Digite o título: ");
            string termo = Console.ReadLine();
            var f = _filmeService.PesquisarFilme(termo);
            if (f != null)
                Console.WriteLine($"Encontrado: [{f.Id}] {f.Titulo} ({f.Ano}) - Nota: {f.Classificacao}");
            else
                Console.WriteLine("Filme não encontrado.");
        }

        static void MenuRemover()
        {
            Console.Clear();
            Console.WriteLine("--- Remover Filme ---");
            Console.Write("Digite o ID do filme a eliminar: ");
            int.TryParse(Console.ReadLine(), out int id);
            if (_filmeService.EliminarFilme(id))
                Console.WriteLine("Filme removido com sucesso!");
            else
                Console.WriteLine("Filme não encontrado.");
        }
    }
}
