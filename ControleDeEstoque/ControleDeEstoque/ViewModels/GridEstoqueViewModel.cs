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

                    string query = @"SELECT
	                                    p.id,
	                                    p.nome,
	                                    p.unidade,
	                                    p.fator_conversao,
	                                    p.tipo,
	                                    IFNULL((estoque.qtdeEntrada - estoque.qtdeSaida), 0) AS estoqueAtual,
	                                    ROUND(IFNULL(estoque.valor_medio, 0), 2) AS valor_medio 
                                    FROM 
	                                    produtos p
	                                    LEFT JOIN(
	                                    SELECT produto, SUM(qtdeEntrada) AS qtdeEntrada,  SUM(qtdeSaida) AS qtdeSaida, valor_medio FROM (
	                                    SELECT
		                                    e.produto,
		                                    SUM(e.qtde) AS qtdeEntrada,
		                                    0 AS qtdeSaida,
		                                    (SUM(e.valor) / SUM(e.qtde)) as valor_medio
	                                    FROM
		                                    entrada e
	                                    GROUP BY
		                                    e.produto
                                    UNION ALL
	                                    SELECT
		                                    s.produto,
		                                    0 AS qtdeEntrada,
		                                    SUM(s.qtde) AS qtdeSaida,
		                                    0 AS valor_medio
	                                    FROM
		                                    saidas s
	                                    GROUP BY
		                                    s.produto) GROUP BY produto) estoque ON estoque.produto = p.id
                                    GROUP BY
	                                    p.id";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Produtos.Add(new Produto(
                                    reader.GetInt32(0),              // id
                                    reader.GetString(1),             // nome
                                    reader.GetString(2),             // unidade                                   
                                    reader.GetDouble(3),             // fatorConversao
                                    (tipoProduto)reader.GetInt32(4), // tipoProduto
                                    reader.GetDouble(5),             // estoque
                                    reader.GetDouble(6)              // valor medio   
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
