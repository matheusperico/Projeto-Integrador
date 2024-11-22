using System.Collections.ObjectModel;
using System.ComponentModel;
using ControleDeEstoque.Models;

namespace ControleDeEstoque.ViewModels
{
    internal class GridEstoqueViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Produto> _produtos;

        public ObservableCollection<Produto> Produtos
        {
            get => _produtos;
            set => SetProperty(ref _produtos, value);
        }

        public GridEstoqueViewModel()
        {
            Produtos = new ObservableCollection<Produto>
            {
                new(1, "Teste", "Kg", 0.60, tipoProduto.MateriaPrima),
                new(2, "Teste2","Lt", 1, tipoProduto.ProdutoAcabado),
                new(3, "Teste3","Kg", 0.30, tipoProduto.MateriaPrima)
            };
        }
    }
}
