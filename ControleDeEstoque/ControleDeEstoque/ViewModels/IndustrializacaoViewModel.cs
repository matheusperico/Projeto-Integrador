using CommunityToolkit.Mvvm.Input;
using ControleDeEstoque.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ControleDeEstoque.ViewModels
{
    internal class IndustrializacaoViewModel: ViewModelBase, INotifyPropertyChanged
    {
        public ObservableCollection<Produto> ProdutosDisponiveis { get; }
        public ObservableCollection<Produto> ProdutosSelecionados { get; }
        public ObservableCollection<Produto> ProdutosAcabados { get; }
        public Produto ProdutoAcabadoSelecionado { get; }

        private Produto _produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set => SetProperty(ref _produtoSelecionado, value);
               
            
        }

        public ICommand AdicionarProdutoCommand { get; }
        public ICommand RemoverProdutoCommand { get; }

        public IndustrializacaoViewModel()
        {
            ProdutosDisponiveis = new ObservableCollection<Produto>
            {
                new(1, "Teste", "Kg", 0.60, tipoProduto.MateriaPrima),
                new(2, "Teste2","Lt", 1, tipoProduto.MateriaPrima),
                new(3, "Teste3","Kg", 0.30, tipoProduto.MateriaPrima)
            };

            ProdutosAcabados = new ObservableCollection<Produto>
            {
                new(1, "TesteAcabado", "Kg", 0.60, tipoProduto.ProdutoAcabado),
                new(2, "TesteAcabado2","Lt", 1, tipoProduto.ProdutoAcabado),
                new(3, "TesteAcabado3","Kg", 0.30, tipoProduto.ProdutoAcabado)
            };

            ProdutosSelecionados = new ObservableCollection<Produto>();

            AdicionarProdutoCommand = new RelayCommand(AdicionarProduto);
            RemoverProdutoCommand = new RelayCommand<Produto>(RemoverProduto);
        }

        private void AdicionarProduto()
        {
            if (ProdutoSelecionado != null && !ProdutosSelecionados.Contains(ProdutoSelecionado))
            {
                ProdutosSelecionados.Add(ProdutoSelecionado);
            }
        }

        private void RemoverProduto(Produto produto)
        {
            if (ProdutosSelecionados.Contains(produto))
            {
                ProdutosSelecionados.Remove(produto);
            }
        }
    }
}
