using Avalonia.Controls;
using ControleDeEstoque.Models;
using Microsoft.Data.Sqlite;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ControleDeEstoque.ViewModels
{
    internal class SaidaViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private Produto _produtoSelecionado;
        private string _listaProdutos;

        public ObservableCollection<Produto> ListaProdutos { get; set; }

        private double _quantidade;

        public double quantidade
        {
            get { return _quantidade; }
            set { SetProperty(ref _quantidade, value); }
        }

        private double _valor;

        public double valor
        {
            get { return _valor; }
            set { SetProperty(ref _valor, value); }
        }


        public Produto ProdutoSelecionado
        {
            get { return _produtoSelecionado; }
            set { SetProperty(ref _produtoSelecionado, value); }
        }
        public ICommand Cancelar { get; }
        public ICommand Salvar { get; }

        public SaidaViewModel()
        {
            ListaProdutos = new ObservableCollection<Produto>();

            if (Design.IsDesignMode)
            {
                ListaProdutos.Add(new Produto(1, "TesteA"));
                ListaProdutos.Add(new Produto(2, "TesteB"));
                ListaProdutos.Add(new Produto(3, "TesteC"));
            }
            else
            {
                CarregarListaProdutos();
            }

            Cancelar = new RelayCommand(LimparCampos);

            Salvar = new RelayCommand(SalvarSaida);
        }

        private void LimparCampos()
        {
            quantidade = 0;
            valor = 0;
            ProdutoSelecionado = null;
        }

        private void SalvarSaida()
        {
            if (ProdutoSelecionado == null)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", "Nenhum produto foi selecionado.", ButtonEnum.Ok);

                var result = box.ShowAsync();

                return;
            }

            if (quantidade <= 0)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", "A quantidade deve ser maior que 0.", ButtonEnum.Ok);

                var result = box.ShowAsync();
                return;
            }

            if (valor <= 0)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", "O valor deve ser maior que 0.", ButtonEnum.Ok);

                var result = box.ShowAsync();
                return;
            }

            var verificador = new verificaEstoque();

            if (quantidade > verificador.CalculaEstoque(ProdutoSelecionado.id))
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", $"Não há quantidade suficiente de {ProdutoSelecionado.nome} em estoque para realizar a baixa.", ButtonEnum.Ok);

                var result = box.ShowAsync();

                return;
            }

            var novaSaida = new registraSaida(ProdutoSelecionado.id, quantidade, valor, DateTime.Now);

            novaSaida.registrarNovaSaida();
        }
        private void CarregarListaProdutos()
        {
            ListaProdutos.Clear();

            string connectionString = @"Data Source=..\..\..\Database\estoque";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id, nome FROM Produtos";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var Produtos = new Produto(reader.GetInt32(0), reader.GetString(1));

                                ListaProdutos.Add(Produtos);
                            }
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", $"Erro ao conectar ou consultar o banco de dados: {ex.Message}", ButtonEnum.Ok);

                var result = box.ShowAsync();
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", $"Erro geral: {ex.Message}", ButtonEnum.Ok);

                var result = box.ShowAsync();
            }
        }
    }
}
