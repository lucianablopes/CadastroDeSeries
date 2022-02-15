
using System;

namespace CadastroDeSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {

            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Finalizando....");
            Console.ReadLine();
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine(@"
            Informe a opção desejada:
            1 - Listar Séries
            2 - Inserir nova série
            3 - Atualizar série
            4 - Excluir série
            5 - Visualizar série
            C - Limpar tela
            X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");
            var lista = repositorio.Lista();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }
            foreach (var serie in lista)
            {
                Console.WriteLine("# ID {0}:  - {1}", serie.RetornaId(), serie.RetornaTitulo());
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o genero entre as opcões acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título da Série: ");
            string entrataTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de Início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição  da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.Proximo(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entrataTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);


            repositorio.Insere(novaSerie);

        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Atualizar Série");
            Console.WriteLine("Digite o Id da série: ");
            int serieId = int.Parse(Console.ReadLine());

            var lista = repositorio.Lista();
            foreach (var item in lista)
            {
                if (item.Id == serieId)
                {

                    foreach (int i in Enum.GetValues(typeof(Genero)))
                    {
                        Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
                    }

                    Console.WriteLine("Digite o genêro entre as opções acima: ");
                    int entradaGenero = int.Parse(Console.ReadLine());

                    Console.WriteLine("Digite o Título da Série: ");
                    string entradaTitulo = Console.ReadLine();

                    Console.WriteLine("Digite o Ano de Início da série: ");
                    int entradaAno = int.Parse(Console.ReadLine());

                    Console.WriteLine("Digite a Descrição  da série: ");
                    string entradaDescricao = Console.ReadLine();

                    Serie atualizaSerie = new Serie(id: serieId,
                                                    genero: (Genero)entradaGenero,
                                                    titulo: entradaTitulo,
                                                    descricao: entradaDescricao,
                                                    ano: entradaAno);

                    repositorio.Atualiza(serieId, atualizaSerie);
                }
                else
                    Console.WriteLine($"Não existe nenhuma série como Id {serieId}");
            }
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Excluir Série");
            Console.WriteLine("Digite o Id da série: ");
            int idexcluir = int.Parse(Console.ReadLine());

            var lista = repositorio.Lista();
            foreach (var item in lista)
            {
                if (item.Id == idexcluir)
                {
                    Console.WriteLine("# ID {0}:  - {1}", item.RetornaId(), item.RetornaTitulo());
                    Console.WriteLine("Você confirma a exclusão da série acima. S/N: ");
                    string resposta = Console.ReadLine().ToUpper();

                    if (resposta == "S")
                    {
                        repositorio.Exclui(item.Id);
                        Console.WriteLine("Série excluída com sucesso!!!");
                    }

                    else
                    {
                        Console.WriteLine("Você optou por não excluir a série.");
                    }
                }

            }
            Console.WriteLine("Não Existe a série!!!!");
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("Visualizar Série:");
            Console.WriteLine("Digite o Id da série: ");
            int idvisualizar = int.Parse(Console.ReadLine());
            var serie = repositorio.RetornaPorId(idvisualizar);
            Console.WriteLine(serie);
        }

    }
}
