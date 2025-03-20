using System;

class Program
{
    struct Produto
    {
        public string Nome;
        public decimal Preco;
    }
    
    struct ItemPedido
    {
        public Produto Produto;
        public int Quantidade;
    }

    static Produto[] produtos = new Produto[3];
    static ItemPedido[] pedido = new ItemPedido[100];
    static int totalItens = 0;

    static void Main(string[] args)
    {
        InicializarProdutos();
        bool executando = true;

        while (executando)
        {
            ExibirMenu();
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "a":
                    ListarProdutos();
                    break;
                case "b":
                    AdicionarProduto();
                    break;
                case "c":
                    RemoverProduto();
                    break;
                case "d":
                    VisualizarPedido();
                    break;
                case "e":
                    FinalizarPedido();
                    executando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void InicializarProdutos()
    {
        produtos[0] = new Produto { Nome = "X-Burguer", Preco = 25.00m };
        produtos[1] = new Produto { Nome = "Refrigerante", Preco = 8.00m };
        produtos[2] = new Produto { Nome = "Sorvete", Preco = 12.00m };
    }

    static void ExibirMenu()
    {
        Console.WriteLine("=== LANCHONETE VIRTUAL ===");
        Console.WriteLine("a. Listar produtos disponíveis");
        Console.WriteLine("b. Adicionar produto ao pedido");
        Console.WriteLine("c. Remover produto do pedido");
        Console.WriteLine("d. Visualizar pedido atual");
        Console.WriteLine("e. Finalizar pedido e sair");
        Console.Write("Escolha uma opção: ");
    }

    static void ListarProdutos()
    {
        Console.WriteLine("\nPRODUTOS DISPONÍVEIS:");
        for (int i = 0; i < produtos.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {produtos[i].Nome} - R$ {produtos[i].Preco:F2}");
        }
    }

    static void AdicionarProduto()
    {
        ListarProdutos();
        Console.Write("Digite o número do produto: ");
        int codigo = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Digite a quantidade: ");
        int quantidade = int.Parse(Console.ReadLine());

        if (codigo >= 0 && codigo < produtos.Length)
        {
            for (int i = 0; i < totalItens; i++)
            {
                if (pedido[i].Produto.Nome == produtos[codigo].Nome)
                {
                    pedido[i].Quantidade += quantidade;
                    Console.WriteLine("Quantidade atualizada!");
                    return;
                }
            }
            
            pedido[totalItens] = new ItemPedido 
            { 
                Produto = produtos[codigo], 
                Quantidade = quantidade 
            };
            totalItens++;
            Console.WriteLine("Produto adicionado ao pedido!");
        }
    }

    static void RemoverProduto()
    {
        VisualizarPedido();
        Console.Write("Digite o número do item a remover: ");
        int indice = int.Parse(Console.ReadLine()) - 1;

        if (indice >= 0 && indice < totalItens)
        {
            Console.Write("Remover quantos? (0 para todos): ");
            int qtd = int.Parse(Console.ReadLine());

            if (qtd == 0 || qtd >= pedido[indice].Quantidade)
            {
                for (int i = indice; i < totalItens - 1; i++)
                {
                    pedido[i] = pedido[i + 1];
                }
                totalItens--;
            }
            else
            {
                pedido[indice].Quantidade -= qtd;
            }
            Console.WriteLine("Item atualizado/removido!");
        }
    }

    static void VisualizarPedido()
    {
        if (totalItens == 0)
        {
            Console.WriteLine("\nPedido vazio!");
            return;
        }

        Console.WriteLine("\nSEU PEDIDO:");
        for (int i = 0; i < totalItens; i++)
        {
            decimal subtotal = pedido[i].Produto.Preco * pedido[i].Quantidade;
            Console.WriteLine($"{i + 1}. {pedido[i].Produto.Nome} - {pedido[i].Quantidade}x - R$ {subtotal:F2}");
        }
    }

    static void FinalizarPedido()
    {
        if (totalItens == 0)
        {
            Console.WriteLine("Nenhum item no pedido!");
            return;
        }

        decimal valorBruto = 0;
        int qtdTotalItens = 0;
        
        for (int i = 0; i < totalItens; i++)
        {
            valorBruto += pedido[i].Produto.Preco * pedido[i].Quantidade;
            qtdTotalItens += pedido[i].Quantidade;
        }

        decimal desconto = 0;
        if (valorBruto > 100)
        {
            desconto = valorBruto * 0.10m;
        }

        decimal valorFinal = valorBruto - desconto;

        Console.WriteLine("\n=== RESUMO DO PEDIDO ===");
        Console.WriteLine($"Total de itens: {qtdTotalItens}");
        Console.WriteLine($"Valor bruto: R$ {valorBruto:F2}");
        if (desconto > 0)
            Console.WriteLine($"Desconto (10%): R$ {desconto:F2}");
        if (qtdTotalItens > 5)
            Console.WriteLine("BRINDE: Frete grátis!");
        Console.WriteLine($"Valor final: R$ {valorFinal:F2}");
        Console.WriteLine("Obrigado pela preferência!");
    }
}