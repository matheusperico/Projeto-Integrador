using System.Security.Cryptography;

namespace ControleDeEstoque.Models
{
    public class Produto
    {
        private int _id;
        private string _nome;
        private string _unidade;
        private float _fatorConversao;
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
        public float fatorConversao
        {
            get => _fatorConversao;
            set => _fatorConversao = value;
        }
        public tipoProduto tipo
        {
            get => _tipo;
            set => _tipo = value;
        }

        public void novoCadastro()
        {
            //
        }
        public void inativarProduto()
        {
            //
        }
    }
    }
