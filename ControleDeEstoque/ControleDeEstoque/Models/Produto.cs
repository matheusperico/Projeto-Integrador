using System.Security.Cryptography;

namespace ControleDeEstoque.Models
{
    public class Produto
    {
        private int _id;
        private string _nome;
        private string _unidade;
        private double _fatorConversao;
        private tipoProduto _tipo;

        public int id
        {
            get => _id;
            set => _id = value;
        }
        public string nome
        {
            get => _nome;
            set => _nome = value;
        }
        public string unidade
        {
            get => _unidade;
            set => _unidade = value;
        }
        public double fatorConversao
        {
            get => _fatorConversao;
            set => _fatorConversao = value;
        }
        public tipoProduto tipo
        {
            get => _tipo;
            set => _tipo = value;
        }
        public Produto(int id1, string nome1)
        {
            this.id = id1;
            this.nome = nome1;
        }

        public Produto(int id, string nome, string unidade, double fatorConversao, tipoProduto tipo) : this(id, nome)
        {
            this.unidade = unidade;
            this.fatorConversao = fatorConversao;
            this.tipo = tipo;
        }

        public void novoCadastro()
        {
            //
        }
        public void inativarProduto()
        {
            //
        }

        public override string ToString()
        {
            return $"{id} - {nome}";
        }
    }
    }
