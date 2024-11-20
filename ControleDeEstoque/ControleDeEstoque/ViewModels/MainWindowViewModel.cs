using ControleDeEstoque.Views;
using System.ComponentModel;
using Avalonia.Controls;

namespace ControleDeEstoque.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _btnEntradaTxt;
        private string _btnSaidaTxt;
        private string _btnIndustrializaTxt;
        private string _btnInicioTxt;

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

        public MainWindowViewModel()
        {
            btnEntradaTxt = "Entrada";
            btnSaidaTxt = "Saída";
            btnIndustrializaTxt = "Industrialização";
            btnInicioTxt = "Inicio";
        }
    }

}
