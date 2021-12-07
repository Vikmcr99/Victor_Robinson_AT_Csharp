using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ClassLibrary
{
    public class Funcoes
    {
        public static void Menu(Repositorio r)
        {
            Console.WriteLine("---------- Gerenciador de Aniversários ----------");
            Console.WriteLine("");
            Console.WriteLine("1 - Adicionar Pessoa\n2 - Pesquisar Pessoa\n3 - Editar Pessoa\n4 - Ver as Pessoas Cadastradas\n5 - Detetar Pessoa\n6 - Sair");

            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    AdicionarPessoa(r);
                    break;
                case "2":
                    OpcoesPesquisar(r);
                    break;
                case "3":
                    EditarPessoa(r);
                    break;
                case "4":
                    MostrarPessoas(r);
                    break;
                case "5":
                    DeletarPessoa(r);
                    break;
                case "6":
                    Console.WriteLine("Fechando Console........");
                    break;
                default:
                    Console.WriteLine("Opção invalida! tente novamente");
                    Console.Clear();
                    Menu(r);
                    break;
            }
        }


        public static void OpcoesPesquisar(Repositorio r)
        {
            Console.Clear();

            Console.WriteLine("---------- Pesquisar Pessoa ----------");
            Console.WriteLine("");
            Console.WriteLine("Selecione a forma de pesquisar abaixo:  ");
            Console.WriteLine("1 - Pesquisar pelo Nome\n2 - Pesquisar pela Data de Nascimento(DD/MM/YY) ");

            string opcao = Console.ReadLine();


            switch (opcao)
            {
                case "1":
                    PesquisaPeloNome(r);
                    break;
                case "2":
                    PesquisaPelaData(r);
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    OpcoesPesquisar(r);
                    break;
            }
        }


        public static void AdicionarPessoa(Repositorio r)
        {
            var a = new Repositorio();
            Console.Clear();
            Console.WriteLine("Insira o nome da pessoa que deseja adicionar: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Agora o Sobrenome: ");
            string sobrenome = Console.ReadLine();

            Console.WriteLine("Data de Nascimento(DD/MM/YY): ");
            DateTime nascimento = DateTime.Parse(Console.ReadLine());


            if (nascimento.Month >= DateTime.Now.Month)
            {
                DateTime proxNiver = new DateTime(DateTime.Now.Year, nascimento.Month, nascimento.Day);
                TimeSpan aniversario = proxNiver.Subtract(DateTime.Today);
                Console.WriteLine("Faltam " + aniversario.Days + " dias para o aniversário de " + nome);
            }
            else
            {
                DateTime proxNiver = new DateTime(DateTime.Now.Year, nascimento.Month, nascimento.Day);
                TimeSpan aniversario = proxNiver.Subtract(DateTime.Today);
                Console.WriteLine("Faltam " + (365 + aniversario.Days) + " dias para o aniversário de " + nome);
            }


            Aniversariante a1 = new Aniversariante(nome, sobrenome, nascimento);

            a.CadastrarAniversariante(a1);

            Console.WriteLine("Pessoa cadastrada com sucesso!");

            Menu(r);
        }

        public static void DeletarPessoa(Repositorio r)
        {
            var a = new Repositorio();

            Console.Clear();
            //MostrarPessoas()
            Console.WriteLine(" ");
            Console.WriteLine("Insira o nome da pessoa que deseja DELETAR: ");
            string nome = Console.ReadLine();

            Console.WriteLine($"Tem certeza que deseja DELETAR {nome}? [s/n]");

            string escolha = Console.ReadLine();

            if (escolha == "s")
            {
                a.Excluir(nome);
                Console.WriteLine("Pessoa deletada com sucesso!");
            }
            else
            {
                Console.WriteLine("Voltando ao Menu...");
            }

            Menu(r);
        }

        public static void MostrarPessoas(Repositorio r)
        {
            var a = new Repositorio();

            Console.Clear();

            var p = a.listaAniversariantes;

            if (p != null)
            {
                a.MostraAniversariantes();
            }
            else
                Console.WriteLine("Ops, parece que nimguem foi cadastrado ainda!");

            Menu(r);
        }


        public static void EditarPessoa(Repositorio r)
        {
            Console.Clear();
            var a = new Repositorio();

            a.MostraAniversariantes();


            Console.WriteLine("Insira o nome da pessoa que deseja editar: ");

            string nome = Console.ReadLine();

            a.Excluir(nome);

            Console.WriteLine("------------------- Editar Pessoa ---------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Novo Nome: ");
            string nomeNovo = Console.ReadLine();
            Console.WriteLine("Novo Sobrenome: ");
            string sobrenomeNovo = Console.ReadLine();
            Console.WriteLine("Nova data de aniverário(DD/MM/YY): ");
            DateTime dataNova = DateTime.Parse(Console.ReadLine());

            Aniversariante a1 = new Aniversariante(nomeNovo, sobrenomeNovo, dataNova);

            a.CadastrarAniversariante(a1);
            Menu(r);


        }

        public static void PesquisaPeloNome(Repositorio r)
        {

            Console.Clear();
            Console.WriteLine("Insira o nome ou o sobrenome da pessoa que deseja pesquisar");

            string[] nome = Console.ReadLine().Split(',');
            string nomeAniver = nome[0];

            var aniversariantesEncontrados = r.BuscaPeloNome(nomeAniver);

            foreach (var aniversariante in aniversariantesEncontrados)
            {
                Console.WriteLine($"{aniversariante.Nome} {aniversariante.Sobrenome} nascido em: {aniversariante.Nascimento} ");
            }

            if (aniversariantesEncontrados == null)
            {
                Console.WriteLine("Ops, parece que não encontrei nimguem!");
            }

        }
        public static void PesquisaPelaData(Repositorio r)
        {
      
            Console.Clear();
            Console.WriteLine("Insira a data de nascimento que deseja pesquisar(DD/MM/YY): ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            var aniversariantesEncontrados = r.BuscapelaData(data);

            int c = aniversariantesEncontrados.Count();

            if (c > 0)
            {
                Console.WriteLine("Aniversariantes encontrados no sistema:");

                foreach (var aniversariante in aniversariantesEncontrados)
                {
                    Console.WriteLine($"{aniversariante.Nome} {aniversariante.Sobrenome} nascido em: {aniversariante.Nascimento} ");
                }
            }
            else
            {
                Console.WriteLine("Não foi encontrado nenhuma pessoa referente a esta data!");
                Console.WriteLine("");
            }
        }

        public static void AniversarioDia(Repositorio r)
        {
            Console.WriteLine("------------------- Aniversariante do dia ---------------------");

            foreach (var aniversariante in r.BuscaAniversariantes())
            {
                if (DateTime.Now.Month == aniversariante.Nascimento.Month && DateTime.Now.Day == aniversariante.Nascimento.Day)
                {
                    Console.WriteLine($"{aniversariante.Nome} {aniversariante.Sobrenome} nascido em: {aniversariante.Nascimento}");
                    Console.WriteLine(" ");
                }

            }

        }
    }
}





