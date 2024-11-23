using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;
using ControleDeEstoque.Models;
using Microsoft.Data.Sqlite;

namespace ControleDeEstoque.ViewModels
{
    internal class GridEstoqueViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Produto> _produtos;

        public ObservableCollection<Produto> Produtos
        {
            get => _produtos;
            set => SetProperty(ref _produtos, value);
        }

        public GridEstoqueViewModel()
        {
            Produtos = new ObservableCollection<Produto>();
            if (Design.IsDesignMode)
            {
                Produtos.Add(new Produto(1, "TesteA"));
                Produtos.Add(new Produto(2, "TesteB"));
                Produtos.Add(new Produto(3, "TesteC"));
            }
            else
            {
                PopularGridEstoque();
            }
        }

        private void PopularGridEstoque()
        {
            string connectionString = @"Data Source=..\..\..\Database\estoque";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id, nome, unidade, fator_conversao, tipo FROM Produtos";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Produtos.Add(new Produto(
                                    reader.GetInt32(0),             // id
                                    reader.GetString(1),            // nome
                                    reader.GetString(2),            // unidade                                   
                                    reader.GetDouble(3),            // fatorConversao
                                    (tipoProduto)reader.GetInt32(4) // tipoProduto
                                ));
                            }
                        }
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
    }
}
