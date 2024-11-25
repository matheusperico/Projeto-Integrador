using Microsoft.Data.Sqlite;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoque.Models
{
    internal class verificaEstoque
    {
        public double CalculaValorMedio(int id_produto)
        {
            double valorMedio = 0;

            string connectionString = @"Data Source=..\..\..\Database\estoque";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT AVG(e.valor) as valorMedio from entrada e where e.produto = @id_produto";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_produto", id_produto);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                valorMedio = reader.GetDouble(0);
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
            return valorMedio;
        }
    }
}
