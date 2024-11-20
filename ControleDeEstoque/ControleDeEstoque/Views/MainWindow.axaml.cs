using Avalonia.Controls;
using ControleDeEstoque.ViewModels;

namespace ControleDeEstoque.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }
}