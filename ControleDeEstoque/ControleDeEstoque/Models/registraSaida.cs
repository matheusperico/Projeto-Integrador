using Microsoft.Data.Sqlite;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;

namespace ControleDeEstoque.Models
{
    internal class registraSaida
    {
        private int _id;
        private Produto _produto;
        private double _quantidade;
        private double _valor;
        private DateTime _dataSaida;

        public registraSaida(int id, double quantidade, double valor, DateTime dataSaida)
        {
            this.id = id;
            Quantidade = quantidade;
            Valor = valor;
            DataSaida = dataSaida;
        }

        public int id { get => _id; set => _id = value; }
        public Produto Produto { get => _produto; set => _produto = value; }
        public double Quantidade { get => _quantidade; set => _quantidade = value; }
        public double Valor { get => _valor; set => _valor = value; }
        public DateTime DataSaida { get => _dataSaida; set => _dataSaida = value; }

        public async void registrarNovaSaida(Boolean mostraMsg = true)
        {
            string connectionString = @"Data Source=..\..\..\Database\estoque";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO saidas (produto, qtde, valor, data_saida) VALUES(@produto_id, @quantidade, @valor, @data_saida)";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@produto_id", id);
                        command.Parameters.AddWithValue("@quantidade", Quantidade);
                        command.Parameters.AddWithValue("@valor", Valor);
                        command.Parameters.AddWithValue("@data_saida", DataSaida);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (mostraMsg)
                        {
                            var box = MessageBoxManager.GetMessageBoxStandard("Sistema", "Registro salvo com sucesso!", ButtonEnum.Ok);

                            var result = await box.ShowAsync();
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", $"Erro ao conectar ou consultar o banco de dados: {ex.Message}", ButtonEnum.Ok);

                var result = await box.ShowAsync();
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", $"Erro geral: {ex.Message}", ButtonEnum.Ok);

                var result = await box.ShowAsync();
            }
        }
    }
}
