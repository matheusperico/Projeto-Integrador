using Avalonia.Controls;
using ControleDeEstoque.ViewModels;

namespace ControleDeEstoque;

public partial class Saida : UserControl
{
    public Saida()
    {
        InitializeComponent();
        DataContext = new SaidaViewModel();
    }
}