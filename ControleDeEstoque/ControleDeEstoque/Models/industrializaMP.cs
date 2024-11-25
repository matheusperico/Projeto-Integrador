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
    internal class industrializaMP
    {
        private int _id;
        private Produto _materiaPrima;
        private Produto _produtoAcabado;
        private double _quantidade;
        private DateTime _dataIndustrializacao;
        private double _valorTotal;

        public industrializaMP(double valorTotal, DateTime dataIndustrializacao, double quantidade, Produto produtoAcabado)
        {
            ValorTotal = valorTotal;
            DataIndustrializacao = dataIndustrializacao;
            Quantidade = quantidade;
            ProdutoAcabado = produtoAcabado;
        }

        public double ValorTotal
        {
            get { return _valorTotal; }
            set { _valorTotal = value; }
        }


        public DateTime DataIndustrializacao
        {
            get { return _dataIndustrializacao; }
            set { _dataIndustrializacao = value; }
        }

        public double Quantidade
        {
            get { return _quantidade; }
            set { _quantidade = value; }
        }

        public Produto ProdutoAcabado
        {
            get { return _produtoAcabado; }
            set { _produtoAcabado = value; }
        }

        public Produto MateriaPrima
        {
            get { return _materiaPrima; }
            set { _materiaPrima = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int industrializaProduto()
        {
            string connectionString = @"Data Source=..\..\..\Database\estoque";
            int ultimoIdInserido = 0;
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO industrializacao (produto_acabado, qtde, data_industrializacao) VALUES(@produto_id, @quantidade, @DataIndustrializacao);
                                    SELECT last_insert_rowid();";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@produto_id", ProdutoAcabado.id);
                        command.Parameters.AddWithValue("@quantidade", Quantidade);
                        command.Parameters.AddWithValue("@DataIndustrializacao", DataIndustrializacao);

                        ultimoIdInserido = Convert.ToInt32(command.ExecuteScalar());

                        var novaEntrada = new registraEntrada(ProdutoAcabado.id, Quantidade, ValorTotal, DateTime.Now);
                        novaEntrada.registrarNovaEntrada();
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

            return ultimoIdInserido;
        }

        public void BaixaMateriaPrimaUsada(int id_materiaPrima, int id_industrializacao, double quantidade, double valorMedio)
        {
            string connectionString = @"Data Source=..\..\..\Database\estoque";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO materia_prima (produto, qtde, id_industrializacao) VALUES(@id_materiaPrima, @quantidade, @id_industrializacao)";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_materiaPrima", id_materiaPrima);
                        command.Parameters.AddWithValue("@quantidade", quantidade);
                        command.Parameters.AddWithValue("@id_industrializacao", id_industrializacao);

                        int rowsAffected = command.ExecuteNonQuery();

                        var novaSaida = new registraSaida(id_materiaPrima, quantidade, valorMedio, DateTime.Now);

                        novaSaida.registrarNovaSaida();
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
