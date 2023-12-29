using System;
using System.IO;

class Program
{
    static string[] produtos; // vetor para armazenar os nomes dos produtos
    static int[] estoque; // vetor para armazenar a quantidade em estoque dos produtos
    static int[,] vendas; // matriz para armazenar as quantidades vendidas

    static void Main()
    {
        bool sair = false;
        while (!sair)
        {
            Console.WriteLine("1 – Importar arquivo de produtos");
            Console.WriteLine("2 – Registrar venda");
            Console.WriteLine("3 – Relatório de vendas");
            Console.WriteLine("4 – Relatório de estoque");
            Console.WriteLine("5 – Criar arquivo de vendas");
            Console.WriteLine("6 - Sair");
            Console.WriteLine();

            Console.Write("Digite a opção desejada: ");
            int opcao = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (opcao)
            {
                case 1:
                    ImportarArquivoProdutos();
                    break;
                case 2:
                    RegistrarVenda();
                    break;
                case 3:
                    RelatorioVendas();
                    break;
                case 4:
                    RelatorioEstoque();
                    break;
                case 5:
                    CriarArquivoVendas();
                    break;
                case 6:
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void ImportarArquivoProdutos()
    {
        StreamReader sr = new StreamReader("trabalhofinal.txt");
        String linha;
        String[] var = new String[2];
        int i=0;
        while((linha = sr.ReadLine()) != null){
            linha.Trim();
            var = linha.Split(',');
            Console.WriteLine(linha);
            produtos[i] = var[0];
            estoque[i] = int.Parse(var[1]);
            i++;
        }
        sr.Close();
        Console.WriteLine("Arquivo de produtos importado com sucesso.");
    }

    static void RegistrarVenda()
    {
        if (produtos == null || produtos.Length == 0)
        {
            Console.WriteLine("Não há produtos importados. Importe um arquivo de produtos primeiro.");
            return;
        }

        Console.Write("Digite o número do produto (1 a {0}): ", produtos.Length);
        int numProduto = int.Parse(Console.ReadLine()) - 1;

        if (numProduto < 0 || numProduto >= produtos.Length)
        {
            Console.WriteLine("Número de produto inválido.");
            return;
        }

        Console.Write("Digite o dia do mês: ");
        int dia = int.Parse(Console.ReadLine());

        if (dia < 1 || dia > 30)
        {
            Console.WriteLine("Dia inválido.");
            return;
        }

        Console.Write("Digite a quantidade vendida: ");
        int quantidade = int.Parse(Console.ReadLine());

        if (quantidade < 0)
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }

        if (quantidade > estoque[numProduto])
        {
            Console.WriteLine("Quantidade em estoque insuficiente.");
            return;
        }

        if (vendas == null)
        {
            vendas = new int[30, produtos.Length];
        }

        vendas[dia - 1, numProduto] += quantidade;
        estoque[numProduto] -= quantidade;

        Console.WriteLine("Venda registrada com sucesso.");
    }

    static void RelatorioVendas()
    {
        if (vendas == null)
        {
            Console.WriteLine("Não há vendas registradas.");
            return;
        }

        Console.WriteLine("Relatório de vendas:");
        Console.Write("           ");
        for (int i = 0; i < produtos.Length; i++)
        {
            Console.Write("{0,10}", produtos[i]);
        }
        Console.WriteLine();

        for (int i = 0; i < 30; i++)
        {
            Console.Write("Dia {0,2}: ", i + 1);
            for (int j = 0; j < produtos.Length; j++)
            {
                Console.Write("{0,10}", vendas[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void RelatorioEstoque()
    {
        if (produtos == null || produtos.Length == 0)
        {
            Console.WriteLine("Não há produtos importados. Importe um arquivo de produtos primeiro.");
            return;
        }

        Console.WriteLine("Relatório de estoque:");
        for (int i = 0; i < produtos.Length; i++)
        {
            Console.WriteLine("{0,-10} {1}", produtos[i], estoque[i]);
        }
    }

    static void CriarArquivoVendas()
    {

        using (StreamWriter writer = new StreamWriter("relatorio_vendas.txt"))
        {
            for (int i = 0; i < produtos.Length; i++)
            {
                int totalVendas = 0;
                for (int j = 0; j < 30; j++)
                {
                    totalVendas += vendas[j, i];
                }

                writer.WriteLine("{0} {1}", produtos[i], totalVendas);
            }
        }

        Console.WriteLine("Arquivo de vendas criado com sucesso.");
    }
}
