using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkpoint1
{
    public class Produto
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
    
    public class ItemPedido
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }

    class Program
    {
        static List<Produto> produtos = new List<Produto>
        {
            new Produto { Nome = "X-Burguer", Preco = 15.00m },
            new Produto { Nome = "Refrigerante", Preco = 5.00m },
            new Produto { Nome = "Sorvete", Preco = 7.50m }
        };
        static List<ItemPedido> pedido = new List<ItemPedido>();

        static void Main(string[] args)
        {
            bool continuar = true;
            while (continuar)
            {
                ExibirMenu();
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine().ToLower();

                switch (opcao)
                {
                    case "a":
                        ListarProdutos();
                        break;
                    case "b":
                        AdicionarAoPedido();
                        break;
                    case "c":
                        RemoverDoPedido();
                        break;
                    case "d":
                        VisualizarPedido();
                        break;
                    case "e":
                        continuar = FinalizarPedido();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
        
        static void ExibirMenu()
        {
            Console.WriteLine("\n=== Menu da Lanchonete Virtual ===");
            Console.WriteLine("a. Listar produtos disponíveis");
            Console.WriteLine("b. Adicionar produto ao pedido");
            Console.WriteLine("c. Remover produto do pedido");
            Console.WriteLine("d. Visualizar pedido atual");
            Console.WriteLine("e. Finalizar pedido e sair");
        }
        
        static void ListarProdutos()
        {
            Console.WriteLine("\nProdutos disponíveis:");
            for (int i = 0; i < produtos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {produtos[i].Nome} - R$ {produtos[i].Preco}");
            }
        }
        
        static void AdicionarAoPedido()
        {
            ListarProdutos();
            Console.Write("\nDigite o número do produto: ");
            if (int.TryParse(Console.ReadLine(), out int numProduto) && numProduto > 0 && numProduto <= produtos.Count)
            {
                Console.Write("Digite a quantidade: ");
                if (int.TryParse(Console.ReadLine(), out int quantidade) && quantidade > 0)
                {
                    Produto produtoSelecionado = produtos[numProduto - 1];
                    ItemPedido itemExistente = pedido.Find(item => item.Produto == produtoSelecionado);
                    if (itemExistente != null)
                    {
                        itemExistente.Quantidade += quantidade;
                    }
                    else
                    {
                        pedido.Add(new ItemPedido { Produto = produtoSelecionado, Quantidade = quantidade });
                    }
                    Console.WriteLine("Produto adicionado ao pedido com sucesso!");
                }
                else
                {
                    Console.WriteLine("Quantidade inválida.");
                }
            }
            else
            {
                Console.WriteLine("Produto inválido.");
            }
        }
        
        static void RemoverDoPedido()
        {
            if (pedido.Count == 0)
            {
                Console.WriteLine("\nO pedido está vazio.");
                return;
            }

            Console.WriteLine("\nItens no pedido:");
            for (int i = 0; i < pedido.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pedido[i].Produto.Nome} - Quantidade: {pedido[i].Quantidade}");
            }
            Console.Write("Digite o número do item a ser removido: ");
            if (int.TryParse(Console.ReadLine(), out int numItem) && numItem > 0 && numItem <= pedido.Count)
            {
                pedido.RemoveAt(numItem - 1);
                Console.WriteLine("Item removido do pedido com sucesso!");
            }
            else
            {
                Console.WriteLine("Item inválido.");
            }
        }
        
        static void VisualizarPedido()
        {
            if (pedido.Count == 0)
            {
                Console.WriteLine("\nO pedido está vazio.");
                return;
            }

            Console.WriteLine("\nPedido atual:");
            decimal total = 0;
            for (int i = 0; i < pedido.Count; i++)
            {
                ItemPedido item = pedido[i];
                decimal subtotal = item.Produto.Preco * item.Quantidade;
                Console.WriteLine($"{i + 1}. {item.Produto.Nome} - Quantidade: {item.Quantidade} - Subtotal: R$ {subtotal}");
                total += subtotal;
            }
            Console.WriteLine($"Total: R$ {total}");
        }
        
        static bool FinalizarPedido()
        {
            if (pedido.Count == 0)
            {
                Console.WriteLine("\nO pedido está vazio. Não é possível finalizar.");
                return true;
            }

            decimal totalBruto = pedido.Sum(item => item.Produto.Preco * item.Quantidade);
            decimal desconto = totalBruto > 100 ? totalBruto * 0.1m : 0;
            decimal totalFinal = totalBruto - desconto;

            Console.WriteLine("\n=== Resumo do Pedido ===");
            Console.WriteLine($"Total de itens: {pedido.Sum(item => item.Quantidade)}");
            Console.WriteLine($"Valor bruto: R$ {totalBruto}");
            if (desconto > 0)
            {
                Console.WriteLine($"Desconto aplicado (10%): R$ {desconto}");
            }
            Console.WriteLine($"Valor final a pagar: R$ {totalFinal}");
            Console.WriteLine("Obrigado por comprar na Lanchonete Virtual!");
            return false;
        }
    }
}