using ControleDeEstoque.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using ControleDeEstoque.Models;
using System;

namespace ControleDeEstoque;

public partial class GridEstoque : UserControl
{    public GridEstoque()
    {
        InitializeComponent(); 
        DataContext = new GridEstoqueViewModel();  
    }
}