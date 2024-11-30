using ControleDeEstoque.Models;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.ComponentModel;
using Avalonia.Controls;
using System.Text.RegularExpressions;

namespace ControleDeEstoque.ViewModels
{
    internal class CadastroProdutoViewModel:ViewModelBase, INotifyPropertyChanged
    {
        private string _nome;
        public string nome { get => _nome; set => SetProperty(ref _nome, value); }
        private string _unidade;
        public string unidade { get => _unidade; set => SetProperty(ref _unidade, value); }
        private tipoProduto _tipo;
        public tipoProduto tipo { get => _tipo; set => SetProperty(ref _tipo, value); }
        private double _fatorConversao;
        public double fatorConversao { get => _fatorConversao; set => SetProperty(ref _fatorConversao, value); }
        public List<tipoProduto> TipoProdutos { get; }
        public ICommand Salvar { get; }
        public ICommand Cancelar { get; }
        public CadastroProdutoViewModel()
        {
            TipoProdutos = new List<tipoProduto>((tipoProduto[])Enum.GetValues(typeof(tipoProduto)));

            Salvar = new RelayCommand(SalvarProduto); 

            Cancelar = new RelayCommand(LimparCampos);
        }

        private void SalvarProduto()
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", "O nome do produto é obrigatório.");
                var result = box.ShowAsync();
                return;
            }

            if (string.IsNullOrWhiteSpace(unidade) || !Regex.IsMatch(unidade, @"^[a-zA-Z]+$"))
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", "A unidade deve conter apenas letras.");
                var result = box.ShowAsync();
                return;
            }

            if (!double.TryParse(fatorConversao.ToString(), out double fator))
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", "O fator de conversão deve ser um número válido.");
                var result = box.ShowAsync();
                return;
            }

            if (tipo == default)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", "O tipo do produto deve ser selecionado.");
                var result = box.ShowAsync();
                return;
            }

            var novoProduto = new Produto(nome, unidade, fatorConversao, tipo);

            novoProduto.novoCadastro();
        } 
        
        private void LimparCampos()
        {
            nome = string.Empty;
            unidade = string.Empty;
            tipo = default;
            fatorConversao = 0;
        }
    }
}
