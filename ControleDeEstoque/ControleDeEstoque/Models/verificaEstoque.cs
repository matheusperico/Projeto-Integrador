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

                    string query = @"SELECT SUM(e.valor) / SUM(e.qtde) AS valorMedio FROM entrada e WHERE e.produto = @id_produto";

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

        public double CalculaEstoque(int id_produto)
        {
            double qtdeEstoque = 0;

            string connectionString = @"Data Source=..\..\..\Database\estoque";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT

                                        estoque.produto,
	                                    (estoque.qtdeEntrada - estoque.qtdeSaida) AS estoqueAtual
                                    FROM
                                        (
                                        SELECT

                                            e.produto,
                                            SUM(e.qtde) AS qtdeEntrada,
                                            0 AS qtdeSaida

                                        FROM

                                            entrada e

                                        WHERE

                                            e.produto = @id_produto

                                        GROUP BY

                                            e.produto
                                    UNION ALL

                                        SELECT

                                            s.produto,
                                            0 AS qtdeEntrada,
                                            SUM(s.qtde) AS qtdeSaida

                                        FROM

                                            saidas s

                                        WHERE

                                            s.produto = @id_produto

                                        GROUP BY

                                            s.produto) estoque
                                    GROUP BY

                                        estoque.produto";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_produto", id_produto);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    qtdeEstoque = reader.GetDouble(1);
                                }
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
            return qtdeEstoque;
        }
    }
}
