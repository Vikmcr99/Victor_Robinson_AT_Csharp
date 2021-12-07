using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Aniversariante
    {
        private string _nome;
        private string _sobrenome;
        private DateTime _nascimento;

        public Aniversariante(string nome, string sobrenome, DateTime nascimento)
        {
            _nome = nome;
            _sobrenome = sobrenome;
            _nascimento = nascimento;
        }

        #region GET SET

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Sobrenome
        {
            get { return _sobrenome; }
            set { _sobrenome = value; }
        }

        public DateTime Nascimento
        {
            get { return _nascimento; }
            set { _nascimento = value; }
        }
        #endregion
    }
}
