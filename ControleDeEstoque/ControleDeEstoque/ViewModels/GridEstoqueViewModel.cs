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
                new(1, "Teste"),
                new(2, "Teste2"),
                new(3, "Teste3")
            };
        }
    }
}
