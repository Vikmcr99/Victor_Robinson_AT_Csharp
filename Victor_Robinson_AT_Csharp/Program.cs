using System;
using ClassLibrary;

namespace Victor_Robinson_AT_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Repositorio r = new Repositorio();
            Funcoes.AniversarioDia(r);
            Console.WriteLine("");
            Funcoes.Menu(r);
            
            
        }
    }
}
