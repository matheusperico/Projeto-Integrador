using ControleDeEstoque.ViewModels;
using Avalonia.Controls;

namespace ControleDeEstoque;

public partial class Industrializacao: UserControl
{
    public Industrializacao()
    {
        InitializeComponent();
        DataContext = new IndustrializacaoViewModel();
    }
}