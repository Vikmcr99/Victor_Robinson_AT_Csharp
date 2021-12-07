using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace ClassLibrary
{
    public  class Repositorio : IRepositorio
    {

        public List<Aniversariante> listaAniversariantes = new List<Aniversariante>();
        private string BuscaArquivo()
        {
            string diretorio = @"C:\AniversariantesDiretorio\";
            System.IO.Directory.CreateDirectory(diretorio);

            string nomeArquivo = "AniversariantesArquivo.txt";
            string caminhoArquivo = System.IO.Path.Combine(diretorio, nomeArquivo);

            FileStream arquivo;
            if (!File.Exists(caminhoArquivo))
            {
                arquivo = File.Create(caminhoArquivo);
                arquivo.Close();
            }




            return caminhoArquivo;
        }
        public IEnumerable<Aniversariante> BuscaAniversariantes()
        {

            string arquivoSaida = File.ReadAllText(BuscaArquivo());

            string[] aniversariantes = arquivoSaida.Split(';');


            for (int i = 0; i < aniversariantes.Length - 1; i++)
            {
                string[] dadosAniversariante = aniversariantes[i].Split(',');

                string nome = dadosAniversariante[0];
                string sobrenome = dadosAniversariante[1];
                DateTime nascimento = DateTime.Parse(dadosAniversariante[2]);

                Aniversariante aniversariante = new Aniversariante(nome, sobrenome, nascimento);

                listaAniversariantes.Add(aniversariante);
            }

            return listaAniversariantes;
        }

        public IEnumerable<Aniversariante> BuscaPeloNome(string nome)
        {

            return (from x in BuscaAniversariantes()
                    where x.Nome.Contains(nome) || x.Sobrenome.Contains(nome)
                    orderby x.Nome
                    select x);
        }
        public IEnumerable<Aniversariante> BuscapelaData(DateTime nascimento)
        {

            return (from x in BuscaAniversariantes()
                    where x.Nascimento == nascimento
                    orderby x.Nascimento
                    select x);
        }

        public void Excluir(string nome)
        {

            //List<string> listaAniver = File.ReadAllLines(BuscaArquivo()).ToList();
            //listaAniver.RemoveAll(x => x == nome);
            //File.WriteAllLines(BuscaArquivo(), listaAniver.ToArray());


            //var mantemNoArquivo = File.ReadLines(BuscaArquivo()).Where(x => x != nome);

            //using (var sw = new StreamWriter(BuscaArquivo()))
            //{
            //    sw.WriteLine(mantemNoArquivo);

            //}

            var temporario = Path.GetTempFileName();
            File.WriteAllLines(temporario, File.ReadLines(BuscaArquivo()).Where(x => x != nome).ToList());
            
            File.Delete(BuscaArquivo());
            if (!File.Exists(BuscaArquivo()))
            {
                File.Move(temporario, BuscaArquivo());
            }
           

        }

        public void CadastrarAniversariante(Aniversariante aniversariante)
        {
            string arquivo = BuscaArquivo();

            using (StreamWriter sw = File.AppendText(arquivo))
            {

                sw.WriteLine($"{ aniversariante.Nome}, { aniversariante.Sobrenome}, { aniversariante.Nascimento.ToString()};");

            }


        }

        public void MostraAniversariantes()
        {
            string arquivo = BuscaArquivo();
            using (var leArquivo = new StreamReader(arquivo))
            {
                Console.WriteLine(leArquivo.ReadToEnd());
            }

        }

    }
}

