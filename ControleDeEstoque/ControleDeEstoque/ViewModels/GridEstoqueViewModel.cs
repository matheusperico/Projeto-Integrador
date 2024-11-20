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
                new Produto { nome = "Produto 1", fatorConversao = 10 },
                new Produto { nome = "Produto 2", fatorConversao = 20 },
                new Produto { nome = "Produto 3", fatorConversao = 30 }
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
