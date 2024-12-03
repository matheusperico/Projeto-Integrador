using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using ControleDeEstoque.Models;
using Microsoft.Data.Sqlite;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ControleDeEstoque.ViewModels
{
    internal class IndustrializacaoViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private double _quantidade;

        public double quantidade
        {
            get { return _quantidade; }
            set { SetProperty( ref _quantidade, value); }
        }

        private double _markup;

        public double markup
        {
            get { return _markup; }
            set { SetProperty(ref _markup, value); }
        }

        public ObservableCollection<Produto> ProdutosDisponiveis { get; set; }
        public ObservableCollection<Produto> ProdutosSelecionados { get; }
        public ObservableCollection<Produto> ProdutosAcabados { get; set; }
        public Produto ProdutoAcabadoSelecionado { get; set; }

        private Produto _produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set => SetProperty(ref _produtoSelecionado, value);
               
            
        }

        public ICommand AdicionarProdutoCommand { get; }
        public ICommand RemoverProdutoCommand { get; }
        public ICommand Cancelar {  get; }
        public ICommand Salvar {  get; }

        public IndustrializacaoViewModel()
        {
            ProdutosDisponiveis = new ObservableCollection<Produto>();
            ProdutosAcabados = new ObservableCollection<Produto>();

            if (Design.IsDesignMode)
            {
                ProdutosDisponiveis.Add(new Produto(1, "TesteA"));
                ProdutosDisponiveis.Add(new Produto(2, "TesteB"));
                ProdutosDisponiveis.Add(new Produto(3, "TesteC"));

                ProdutosAcabados.Add(new Produto(1, "TesteAcabadoA"));
                ProdutosAcabados.Add(new Produto(2, "TesteAcabadoB"));
                ProdutosAcabados.Add(new Produto(3, "TesteAcabadoC"));                
            }
            else
            {
                CarregarListaProdutosDisponiveis();
                CarregarListaProdutosAcabados();
            }

            

            ProdutosSelecionados = new ObservableCollection<Produto>();

            AdicionarProdutoCommand = new RelayCommand(AdicionarProduto);
            RemoverProdutoCommand = new RelayCommand<Produto>(RemoverProduto);

            Cancelar = new RelayCommand(LimparCampos);

            Salvar = new RelayCommand(SalvaIndustrializacao);
        }

        private void SalvaIndustrializacao()
        {
            if (quantidade <= 0)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Erro", "A quantidade deve ser maior que zero.", ButtonEnum.Ok);
                var result = box.ShowAsync();
                return;
            }

            if (markup < 0)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Erro", "O markup não pode ser negativo.", ButtonEnum.Ok);
                var result = box.ShowAsync();
                return;
            }

            if (ProdutoAcabadoSelecionado == null)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Erro", "Selecione um produto acabado.", ButtonEnum.Ok);
                var result = box.ShowAsync();
                return;
            }

            if (ProdutosSelecionados.Count == 0)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Erro", "Adicione ao menos um produto à lista.", ButtonEnum.Ok);
                var result = box.ShowAsync();
                return;
            }


            double valorTotal = 0;
            var verificador = new verificaEstoque();

            foreach(Produto produto in ProdutosSelecionados) 
            {
                double quantidadeMateriaPrimaUsada = quantidade * produto.fatorConversao;

                if(quantidadeMateriaPrimaUsada > verificador.CalculaEstoque(produto.id))
                {
                    var box = MessageBoxManager.GetMessageBoxStandard("Industrialização", $"Não há quantidade suficiente de {produto.nome} em estoque para a produção.", ButtonEnum.Ok);

                    var result = box.ShowAsync();

                    return;
                } 
                
                double valorMedio = verificador.CalculaValorMedio(produto.id) * quantidadeMateriaPrimaUsada;
                valorTotal += valorMedio;
            }

            valorTotal += valorTotal * (markup / 100);

            var novaIndustrializacao = new industrializaMP(valorTotal, DateTime.Now, quantidade, ProdutoAcabadoSelecionado);

            int id_industrializacao = novaIndustrializacao.industrializaProduto();

            foreach(Produto produto in ProdutosSelecionados)
            {
                double quantidadeMateriaPrimaUsada = quantidade * produto.fatorConversao;
                double valorMedio = verificador.CalculaValorMedio(produto.id);

                novaIndustrializacao.BaixaMateriaPrimaUsada(produto.id, id_industrializacao, quantidadeMateriaPrimaUsada, valorMedio);
            }

            var msg = MessageBoxManager.GetMessageBoxStandard("Sistema", $"Industrialização realizada com sucesso!", ButtonEnum.Ok);

            var resultado = msg.ShowAsync();
        }

        private void AdicionarProduto()
        {
            if (ProdutoSelecionado != null && !ProdutosSelecionados.Contains(ProdutoSelecionado))
            {
                ProdutosSelecionados.Add(ProdutoSelecionado);
            }
        }

        private void LimparCampos()
        {
            quantidade = 0;
            markup = 0;
            ProdutosAcabados.Clear();
            ProdutosSelecionados.Clear();
            ProdutosDisponiveis.Clear();

            CarregarListaProdutosDisponiveis();
            CarregarListaProdutosAcabados();
        }

        private void RemoverProduto(Produto produto)
        {
            if (ProdutosSelecionados.Contains(produto))
            {
                ProdutosSelecionados.Remove(produto);
            }
        }

        private void CarregarListaProdutosDisponiveis()
        {
            ProdutosDisponiveis.Clear();

            string connectionString = @"Data Source=..\..\..\Database\estoque";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id, nome, fator_conversao, unidade, tipo FROM Produtos WHERE tipo = 1";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProdutosDisponiveis.Add(new Produto(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetDouble(2), tipoProduto.MateriaPrima));                                
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

        private void CarregarListaProdutosAcabados()
        {
            ProdutosAcabados.Clear();

            string connectionString = @"Data Source=..\..\..\Database\estoque";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id, nome FROM Produtos WHERE tipo = 2";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProdutosAcabados.Add(new Produto(reader.GetInt32(0), reader.GetString(1)));                                
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
