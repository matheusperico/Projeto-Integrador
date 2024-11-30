using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ControleDeEstoque.ViewModels;


namespace ControleDeEstoque;

public partial class CadastroProduto : UserControl
{
    public CadastroProduto()
    {
        InitializeComponent();
        DataContext = new CadastroProdutoViewModel();
    }

    private void Binding(object? sender, Avalonia.Controls.TextChangedEventArgs e)
    {
    }
}