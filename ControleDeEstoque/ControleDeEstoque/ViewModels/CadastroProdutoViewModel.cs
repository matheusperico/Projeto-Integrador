using ControleDeEstoque.Models;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Avalonia.Controls;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ControleDeEstoque.ViewModels
{
    internal class CadastroProdutoViewModel:ViewModelBase, INotifyPropertyChanged
    {
        private string _nome;
        public string nome { get => _nome; set => SetProperty(ref _nome, value); }
        private double _unidade;
        public double unidade { get => _unidade; set => SetProperty(ref _unidade, value); }
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

        private async void SalvarProduto()
        {
            string connectionString = @"Data Source=..\..\..\Database\estoque";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO produtos (nome, unidade, tipo, fator_conversao) VALUES (@nome, @unidade, @tipo, @fator_conversao)";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@unidade", unidade);
                        command.Parameters.AddWithValue("@tipo", (int)tipo);
                        command.Parameters.AddWithValue("@fator_conversao", fatorConversao);

                        int rowsAffected = command.ExecuteNonQuery();
                        var box = MessageBoxManager.GetMessageBoxStandard("Sistema", "Registro salvo com sucesso!", ButtonEnum.Ok); 
                        
                        var result = await box.ShowAsync();
                    }                          
                 }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Erro ao conectar ou consultar o banco de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
            }
        } 
        
        private void LimparCampos()
        {
            nome = string.Empty;
            unidade = 0;
            tipo = default;
            fatorConversao = 0;
        }
    }
}
