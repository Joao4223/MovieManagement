using System;
using MovieManagement.Business;
using MovieManagement.Data;
using MovieManagement.Domain;

namespace MovieManagement.UI
{
    internal class Program
    {
        private static FilmeService _filmeService = null!;
        private static CategoriaService _categoriaService = null!;
        private static RealizadorService _realizadorService =null!;
        static void Main(string[] args)
        {
            // Instanciar Repositórios e Serviços (Injeção manual)
            IFilmeRepository filmeRepo = new FilmeRepository();
            ICategoriaRepository catRepo = new CategoriaRepository();
            IRealizadorRepository realRepo = new RealizadorRepository();

            _filmeService = new FilmeService(filmeRepo);
            _categoriaService = new CategoriaService(catRepo);
            _realizadorService = new RealizadorService(realRepo);

            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== MovieManagement - Menu Principal ===");
                Console.WriteLine("1. Gestão de Filmes");
                Console.WriteLine("2. Gestão de Categorias");
                Console.WriteLine("3. Gestão de Realizadores");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                switch (opcao)
                {
                    case 1: MenuFilmes(); break;
                    case 2: MenuCategorias(); break;
                    case 3: MenuRealizadores(); break;
                }
            } while (opcao != 0);
        }

       
        static void MenuFilmes()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("--- Gestão de Filmes ---");
                Console.WriteLine("1. Adicionar Filme");
                Console.WriteLine("2. Listar Filmes");
                Console.WriteLine("3. Procurar Filme por Título");
                Console.WriteLine("4. Remover Filme");
                Console.WriteLine("0. Voltar");
                Console.Write("Opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Filme novo = new Filme();
                        Console.Write("Título: "); novo.Titulo = Console.ReadLine() ?? "";
                        Console.Write("Ano: "); int.TryParse(Console.ReadLine(), out int ano); novo.Ano = ano;
                        Console.Write("Língua: "); novo.Lingua = Console.ReadLine() ?? "";
                        Console.Write("Classificação (0-5): "); double.TryParse(Console.ReadLine(), out double classif); novo.Classificacao = classif;
                        Console.WriteLine(_filmeService.AdicionarFilme(novo));
                        break;

                    case 2:
                        Console.Clear();
                        var lista = _filmeService.ObterTodosFilmes();
                        if (lista.Count == 0) Console.WriteLine("Nenhum filme registado.");
                        foreach (var f in lista) Console.WriteLine($"[{f.Id}] {f.Titulo} ({f.Ano}) - Nota: {f.Classificacao}/5");
                        break;

                    case 3:
                        Console.Clear();
                        Console.Write("Digite o título: ");
                        // Mudamos o nome de 'f' para 'filmeEncontrado' para eliminar o erro
                        var filmeEncontrado = _filmeService.PesquisarFilme(Console.ReadLine() ?? "");
                        if (filmeEncontrado != null)
                            Console.WriteLine($"Encontrado: [{filmeEncontrado.Id}] {filmeEncontrado.Titulo} ({filmeEncontrado.Ano})");
                        else
                            Console.WriteLine("Filme não encontrado.");
                        break;

                    case 4:
                        Console.Clear();
                        Console.Write("ID a eliminar: ");
                        int.TryParse(Console.ReadLine(), out int id);
                        Console.WriteLine(_filmeService.EliminarFilme(id) ? "Removido!" : "Não encontrado.");
                        break;
                }
                if (opcao != 0) { Console.WriteLine("\nPressione qualquer tecla..."); Console.ReadKey(); }
            } while (opcao != 0);
        }
       
        static void MenuCategorias()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("--- Gestão de Categorias ---");
                Console.WriteLine("1. Adicionar Categoria");
                Console.WriteLine("2. Listar Categorias");
                Console.WriteLine("3. Remover Categoria");
                Console.WriteLine("0. Voltar");
                Console.Write("Opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Categoria nova = new Categoria();
                        Console.Write("Nome da Categoria: "); nova.Nome = Console.ReadLine() ?? "";
                        Console.WriteLine(_categoriaService.AdicionarCategoria(nova));
                        break;
                    case 2:
                        Console.Clear();
                        var lista = _categoriaService.ObterTodasCategorias();
                        if (lista.Count == 0) Console.WriteLine("Nenhuma categoria registada.");
                        foreach (var c in lista) Console.WriteLine($"[{c.Id}] {c.Nome}");
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("ID a eliminar: ");
                        int.TryParse(Console.ReadLine(), out int id);
                        Console.WriteLine(_categoriaService.EliminarCategoria(id) ? "Removida!" : "Não encontrada.");
                        break;
                }
                if (opcao != 0) { Console.WriteLine("\nPressione qualquer tecla..."); Console.ReadKey(); }
            } while (opcao != 0);
        }
        

        
        static void MenuRealizadores()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("--- Gestão de Realizadores ---");
                Console.WriteLine("1. Adicionar Realizador");
                Console.WriteLine("2. Listar Realizadores");
                Console.WriteLine("3. Remover Realizador");
                Console.WriteLine("0. Voltar");
                Console.Write("Opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Realizador novo = new Realizador();
                        Console.Write("Nome do Realizador: "); novo.Nome = Console.ReadLine() ?? "";
                        Console.Write("País de Origem: "); novo.Pais = Console.ReadLine() ?? "";
                        Console.WriteLine(_realizadorService.AdicionarRealizador(novo));
                        break;
                    case 2:
                        Console.Clear();
                        var lista = _realizadorService.ObterTodosRealizadores();
                        if (lista.Count == 0) Console.WriteLine("Nenhum realizador registado.");
                        foreach (var r in lista) Console.WriteLine($"[{r.Id}] {r.Nome} ({r.Pais})");
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("ID a eliminar: ");
                        int.TryParse(Console.ReadLine(), out int id);
                        Console.WriteLine(_realizadorService.EliminarRealizador(id) ? "Removido!" : "Não encontrado.");
                        break;
                }
                if (opcao != 0) { Console.WriteLine("\nPressione qualquer tecla..."); Console.ReadKey(); }
            } while (opcao != 0);
        }
        

    }







}

