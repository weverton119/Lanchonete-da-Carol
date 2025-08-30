

using System;
using System.Collections.Generic;
using System.Globalization;

namespace PedidoLanchonete
{
    class Produto
    {
        public string Nome { get; set; }
        public double Preco { get; set; }

        public Produto(string nome, double preco)
        {
            Nome = nome;
            Preco = preco;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR", false);

            List<Produto> cardapio = new List<Produto>();
            List<(Produto, int)> pedidos = new List<(Produto, int)>();

            Console.WriteLine("==============================================");
            Console.WriteLine("     Sistema de Cadastro e Pedido Lanchonete  ");
            Console.WriteLine("==============================================");

         
            // 1. Cadastro de Produtos
           
            Console.WriteLine("\n--- Cadastro de Produtos ---");

            string continuarCadastro;
            do
            {
                Console.Write("Digite o nome do produto: ");
                string nome = Console.ReadLine();

                double preco;
                Console.Write("Digite o preço do produto: ");
                while (!double.TryParse(Console.ReadLine(), out preco) || preco <= 0)
                {
                    Console.Write("Preço inválido! Digite novamente: ");
                }

                cardapio.Add(new Produto(nome, preco));
                Console.WriteLine($"=> Produto '{nome}' cadastrado com sucesso!");

                Console.Write("Deseja cadastrar outro produto? (s/n): ");
                continuarCadastro = Console.ReadLine().ToLower();

            } while (continuarCadastro == "s");

            // 2. Pedido do Cliente
        
            bool finalizarPedido = false;
            do
            {
                Console.WriteLine("\n--- CARDÁPIO ---");
                for (int i = 0; i < cardapio.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {cardapio[i].Nome} - {cardapio[i].Preco:C}");
                }
                Console.WriteLine($"{cardapio.Count + 1}. Finalizar Pedido");

                Console.Write("\nDigite o número do produto desejado: ");
                string input = Console.ReadLine();
                int escolha;

                if (int.TryParse(input, out escolha))
                {
                    if (escolha >= 1 && escolha <= cardapio.Count)
                    {
                        Produto produtoEscolhido = cardapio[escolha - 1];

                        Console.Write($"Digite a quantidade de {produtoEscolhido.Nome}: ");
                        int quantidade;
                        while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0)
                        {
                            Console.Write("Quantidade inválida! Digite novamente: ");
                        }

                        pedidos.Add((produtoEscolhido, quantidade));
                        Console.WriteLine($"=> {quantidade}x {produtoEscolhido.Nome}(s) adicionado(s) ao pedido.");
                    }
                    else if (escolha == cardapio.Count + 1)
                    {
                        finalizarPedido = true;
                        Console.WriteLine("Finalizando o pedido...");
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida! Escolha um número do cardápio.");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida! Digite apenas números.");
                }

            } while (!finalizarPedido);

           
            // 3. Resumo do Pedido
          
            Console.WriteLine("\n==============================================");
            Console.WriteLine("                RESUMO DO PEDIDO              ");
            Console.WriteLine("==============================================");

            double valorTotal = 0;
            foreach (var item in pedidos)
            {
                double subtotal = item.Item1.Preco * item.Item2;
                Console.WriteLine($"{item.Item2}x {item.Item1.Nome} - Subtotal: {subtotal:C}");
                valorTotal += subtotal;
            }

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"TOTAL DA COMPRA: {valorTotal:C}");
            Console.WriteLine("==============================================");
            Console.WriteLine("\nObrigado pela preferência e volte sempre!");
        }
    }
}