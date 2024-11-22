using System;

namespace ControleDeEstoque.Models
{
    internal class registraSaida
    {
        private int _id;
        private Produto _produto;
        private double _quantidade;
        private double _valor;
        private DateTime _dataSaida;

        public int id { get => _id; set => _id = value; }
        public Produto Produto { get => _produto; set => _produto = value; }
        public double Quantidade { get => _quantidade; set => _quantidade = value; }
        public double Valor { get => _valor; set => _valor = value; }
        public DateTime DataSaida { get => _dataSaida; set => _dataSaida = value; }

        public void registrarSaida()
        {
            //
        }

        public void estornarSaida()
        {
            //
        }
    }
}
