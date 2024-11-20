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
                new Produto { Nome = "Produto 1", Preco = 10.00 },
                new Produto { Nome = "Produto 2", Preco = 20.00 },
                new Produto { Nome = "Produto 3", Preco = 30.00 }
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
