using Microsoft.Data.Sqlite;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Security.Cryptography;

namespace ControleDeEstoque.Models
{
    public class Produto
    {
        private int _id;
        private string _nome;
        private string _unidade;
        private double _fatorConversao;
        private tipoProduto _tipo;

        public int id
        {
            get => _id;
            set => _id = value;
        }
        public string nome
        {
            get => _nome;
            set => _nome = value;
        }
        public string unidade
        {
            get => _unidade;
            set => _unidade = value;
        }
        public double fatorConversao
        {
            get => _fatorConversao;
            set => _fatorConversao = value;
        }
        public tipoProduto tipo
        {
            get => _tipo;
            set => _tipo = value;
        }
        public Produto(int id1, string nome1)
        {
            this.id = id1;
            this.nome = nome1;
        }

        public Produto(string nome, string unidade, double fatorConversao, tipoProduto tipo)
        {
            this.nome = nome;
            this.unidade = unidade;
            this.fatorConversao = fatorConversao;
            this.tipo = tipo;
        }

        public Produto(int id, string nome, string unidade, double fatorConversao, tipoProduto tipo)
        {
            this.id = id;
            this.nome = nome;
            this.unidade = unidade;
            this.fatorConversao = fatorConversao;
            this.tipo = tipo;
        }

        public async void novoCadastro()
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
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", $"Erro ao conectar ou consultar o banco de dados: {ex.Message}", ButtonEnum.Ok);

                var result = await box.ShowAsync();
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Sistema", $"Erro geral: {ex.Message}", ButtonEnum.Ok);

                var result = await box.ShowAsync();
            }
        }

        public override string ToString()
        {
            return $"{id} - {nome}";
        }
    }
    }
