using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Data.Sqlite;
using ControleDeEstoque.Models;
using System;

namespace ControleDeEstoque.ViewModels
{
    internal class EntradaViewModel: ViewModelBase, INotifyPropertyChanged
    {
		private string _produtoSelecionado;
		private string _listaProdutos;

        public ObservableCollection<Produto> ListaProdutos { get; set; }

        public string ProdutoSelecionado
		{
			get { return _produtoSelecionado; }
			set { _produtoSelecionado = value; }
		}

		public EntradaViewModel()
		{
			ListaProdutos = new ObservableCollection<Produto>();

#if !DESIGNTIME

            ListaProdutos.Add(new Produto(1, "TesteA"));
            ListaProdutos.Add(new Produto(2, "TesteB"));
            ListaProdutos.Add(new Produto(3, "TesteC"));
#else
                  CarregarListaProdutos();
#endif
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
                Console.WriteLine($"Erro ao conectar ou consultar o banco de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
            }
        }
	}
}
