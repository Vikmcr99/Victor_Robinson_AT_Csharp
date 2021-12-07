using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public interface IRepositorio
    {
        void CadastrarAniversariante(Aniversariante aniversariante);
        void Excluir(string nome);
        void MostraAniversariantes();
        IEnumerable<Aniversariante> BuscaAniversariantes();
        IEnumerable<Aniversariante> BuscaPeloNome(string nome);
        IEnumerable<Aniversariante> BuscapelaData(DateTime nascimento);

    }
}
