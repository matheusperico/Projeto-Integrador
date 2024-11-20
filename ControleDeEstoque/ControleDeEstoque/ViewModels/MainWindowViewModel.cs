using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;

namespace ControleDeEstoque.ViewModels
{
    public partial class MainWindowViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private string _btnEntradaTxt = "Entrada";
        private string _btnSaidaTxt = "Saída";
        private string _btnIndustrializaTxt = "Industrialização";
        private string _btnInicioTxt = "Inicio";

        private UserControl _currentView;
        public string btnEntradaTxt
        {
            get => _btnEntradaTxt;
            set
            {
                _btnEntradaTxt = value;
                OnPropertyChanged(nameof(btnEntradaTxt));
            }
        }

        public string btnSaidaTxt
        {
            get => _btnSaidaTxt;
            set
            {
                _btnSaidaTxt = value;
                OnPropertyChanged(nameof(btnSaidaTxt));
            }
        }

        public string btnIndustrializaTxt
        {
            get => _btnIndustrializaTxt;
            set
            {
                _btnIndustrializaTxt = value;
                OnPropertyChanged(nameof(_btnIndustrializaTxt));
            }
        }

        public string btnInicioTxt
        {
            get => _btnInicioTxt;
            set
            {
                _btnInicioTxt = value;
                OnPropertyChanged(nameof(_btnInicioTxt));
            }
        }

        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand ViewInicio { get; }
        public ICommand ViewEntradas { get; }
        public ICommand ViewSaidas { get; }
        public ICommand ViewIndustrializacao { get; }

        public MainWindowViewModel()
        {
            ViewInicio = new RelayCommand(() => SetView(new GridEstoque()));  // View para Início
            ViewEntradas = new RelayCommand(() => SetView(new GridEstoque()));  // View para Entradas
            ViewSaidas = new RelayCommand(() => SetView(new GridEstoque()));  // View para Saídas
            ViewIndustrializacao = new RelayCommand(() => SetView(new GridEstoque())); // View para Industrialização

            CurrentView = new GridEstoque();
        }

        public void SetView(UserControl view)
        {
            CurrentView = view;
        }
    }

}
