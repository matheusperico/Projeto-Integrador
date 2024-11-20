using ControleDeEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoque.ViewModels
{
    internal class CadastroProdutoViewModel
    {
        public List<tipoProduto> TipoProdutos { get; }

        public CadastroProdutoViewModel()
        {
            TipoProdutos = new List<tipoProduto>((tipoProduto[])Enum.GetValues(typeof(tipoProduto)));
        }
    }
}
