using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkpoint1
{
    // Definição da classe Produto, que representa um item no cardápio
    public class Produto
    {
        public string Nome { get; set; } // Nome do produto
        public decimal Preco { get; set; } // Preço do produto
    }

    // Definição da classe ItemPedido, que representa um item adicionado ao pedido
    public class ItemPedido
    {
        public Produto Produto { get; set; } // Produto selecionado
        public int Quantidade { get; set; } // Quantidade desse produto no pedido
    }

    class Program
    {
        // Lista estática de produtos disponíveis no cardápio
        static List<Produto> produtos = new List<Produto>
        {
            new Produto { Nome = "X-Burguer", Preco = 15.00m },
            new Produto { Nome = "Refrigerante", Preco = 5.00m },
            new Produto { Nome = "Sorvete", Preco = 7.50m }
        };

        // Lista estática para armazenar os itens do pedido atual
        static List<ItemPedido> pedido = new List<ItemPedido>();

        // Método principal que inicia o programa
        static void Main(string[] args)
        {
            bool continuar = true; // Variável para controlar o loop do menu
            while (continuar)
            {
                ExibirMenu(); // Exibe o menu de opções
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine().ToLower(); // Lê a opção do usuário e converte para minúsculas

                // Usa um switch para direcionar a ação com base na opção escolhida
                switch (opcao)
                {
                    case "a":
                        ListarProdutos(); // Chama a função para listar produtos
                        break;
                    case "b":
                        AdicionarAoPedido(); // Chama a função para adicionar ao pedido
                        break;
                    case "c":
                        RemoverDoPedido(); // Chama a função para remover do pedido
                        break;
                    case "d":
                        VisualizarPedido(); // Chama a função para visualizar o pedido
                        break;
                    case "e":
                        continuar = FinalizarPedido(); // Chama a função para finalizar o pedido e decide se continua o loop
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        // Função para exibir o menu de opções
        static void ExibirMenu()
        {
            Console.WriteLine("\n=== Menu da Lanchonete Virtual ===");
            Console.WriteLine("a. Listar produtos disponíveis");
            Console.WriteLine("b. Adicionar produto ao pedido");
            Console.WriteLine("c. Remover produto do pedido");
            Console.WriteLine("d. Visualizar pedido atual");
            Console.WriteLine("e. Finalizar pedido e sair");
        }

        // Função para listar os produtos disponíveis
        static void ListarProdutos()
        {
            Console.WriteLine("\nProdutos disponíveis:");
            // Usa um loop for para iterar sobre a lista de produtos
            for (int i = 0; i < produtos.Count; i++)
            {
                // Exibe o número do produto (começando em 1), nome e preço
                Console.WriteLine($"{i + 1}. {produtos[i].Nome} - R$ {produtos[i].Preco}");
            }
        }

        // Função para adicionar um produto ao pedido
        static void AdicionarAoPedido()
        {
            ListarProdutos(); // Primeiro, lista os produtos para o usuário escolher
            Console.Write("\nDigite o número do produto: ");
            // Tenta converter a entrada para um número inteiro
            if (int.TryParse(Console.ReadLine(), out int numProduto) && numProduto > 0 && numProduto <= produtos.Count)
            {
                Console.Write("Digite a quantidade: ");
                // Tenta converter a entrada para um número inteiro
                if (int.TryParse(Console.ReadLine(), out int quantidade) && quantidade > 0)
                {
                    // Seleciona o produto com base no número fornecido (ajustado para índice 0)
                    Produto produtoSelecionado = produtos[numProduto - 1];
                    // Verifica se o produto já está no pedido
                    ItemPedido itemExistente = pedido.Find(item => item.Produto == produtoSelecionado);
                    if (itemExistente != null)
                    {
                        // Se já existe, incrementa a quantidade
                        itemExistente.Quantidade += quantidade;
                    }
                    else
                    {
                        // Se não existe, adiciona um novo item ao pedido
                        pedido.Add(new ItemPedido { - Quantidade: { Produto = produtoSelecionado, Quantidade = quantidade });
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

        // Função para remover um produto do pedido
        static void RemoverDoPedido()
        {
            // Verifica se o pedido está vazio
            if (pedido.Count == 0)
            {
                Console.WriteLine("\nO pedido está vazio.");
                return;
            }

            // Exibe os itens do pedido
            Console.WriteLine("\nItens no pedido:");
            for (int i = 0; i < pedido.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pedido[i].Produto.Nome} - Quantidade: {pedido[i].Quantidade}");
            }

            Console.Write("Digite o número do item a ser modificado: ");
            // Tenta converter a entrada para um número inteiro
            if (int.TryParse(Console.ReadLine(), out int numItem) && numItem > 0 && numItem <= pedido.Count)
            {
                // Seleciona o item com base no número fornecido (ajustado para índice 0)
                ItemPedido itemSelecionado = pedido[numItem - 1];
                Console.WriteLine($"\nItem selecionado: {itemSelecionado.Produto.Nome} - Quantidade: {itemSelecionado.Quantidade}");

                // Pergunta ao usuário se quer decrementar ou remover completamente
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1. Decrementar quantidade");
                Console.WriteLine("2. Remover completamente");
                Console.Write("Opção: ");
                string opcaoRemocao = Console.ReadLine();

                switch (opcaoRemocao)
                {
                    case "1": // Decrementar quantidade
                        Console.Write("Digite a quantidade a ser removida: ");
                        if (int.TryParse(Console.ReadLine(), out int qtdRemover) && qtdRemover > 0)
                        {
                            // Se a quantidade a remover for maior ou igual à atual, remove o item
                            if (qtdRemover >= itemSelecionado.Quantidade)
                            {
                                pedido.RemoveAt(numItem - 1);
                                Console.WriteLine("Item removido completamente do pedido.");
                            }
                            else
                            {
                                // Caso contrário, subtrai a quantidade
                                itemSelecionado.Quantidade -= qtdRemover;
                                Console.WriteLine($"Quantidade decrementada. Nova quantidade: {itemSelecionado.Quantidade}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Quantidade inválida.");
                        }
                        break;

                    case "2": // Remover completamente
                        pedido.RemoveAt(numItem - 1);
                        Console.WriteLine("Item removido completamente do pedido.");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Item inválido.");
            }
        }

        // Função para visualizar o pedido atual
        static void VisualizarPedido()
        {
            // Verifica se o pedido está vazio
            if (pedido.Count == 0)
            {
                Console.WriteLine("\nO pedido está vazio.");
                return;
            }

            Console.WriteLine("\nPedido atual:");
            decimal total = 0; // Inicializa o total
            for (int i = 0; i < pedido.Count; i++)
            {
                ItemPedido item = pedido[i];
                // Calcula o subtotal do item (preço * quantidade)
                decimal subtotal = item.Produto.Preco * item.Quantidade;
                Console.WriteLine($"{i + 1}. {item.Produto.Nome} - Quantidade: {item.Quantidade} - Subtotal: R$ {subtotal}");
                total += subtotal; // Adiciona ao total
            }
            Console.WriteLine($"Total: R$ {total}"); // Exibe o total
        }

        // Função para finalizar o pedido
        static bool FinalizarPedido()
        {
            // Verifica se o pedido está vazio
            if (pedido.Count == 0)
            {
                Console.WriteLine("\nO pedido está vazio. Não é possível finalizar.");
                return true; // Continua o loop
            }

            // Calcula o total bruto usando LINQ
            decimal totalBruto = pedido.Sum(item => item.Produto.Preco * item.Quantidade);
            // Aplica desconto de 10% se o total bruto for maior que R$ 100
            decimal desconto = totalBruto > 100 ? totalBruto * 0.1m : 0;
            decimal totalFinal = totalBruto - desconto; // Calcula o total final

            // Exibe o resumo do pedido
            Console.WriteLine("\n=== Resumo do Pedido ===");
            Console.WriteLine($"Total de itens: {pedido.Sum(item => item.Quantidade)}");
            Console.WriteLine($"Valor bruto: R$ {totalBruto}");
            if (desconto > 0)
            {
                Console.WriteLine($"Desconto aplicado (10%): R$ {desconto}");
            }
            Console.WriteLine($"Valor final a pagar: R$ {totalFinal}");
            Console.WriteLine("Obrigado por comprar na Lanchonete Virtual!");
            return false; // Encerra o loop
        }
    }
}