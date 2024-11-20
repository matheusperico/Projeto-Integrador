using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ControleDeEstoque.ViewModels;

namespace ControleDeEstoque;

public partial class Entrada : UserControl
{
    public Entrada()
    {
        InitializeComponent();
        DataContext = new EntradaViewModel();
    }
}