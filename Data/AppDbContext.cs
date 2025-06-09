using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TodoList.Models;

namespace TodoList.Data
{
    // Classe estática responsável por salvar e carregar as tarefas do arquivo JSON
    public static class BancoDados
    {
        // Caminho do arquivo onde as tarefas são salvas
        private static readonly string caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "dados.json");


        // Lista de tarefas em memória
        public static List<TodoTask> Tarefas { get; private set; } = new List<TodoTask>();

        // Carrega as tarefas do arquivo JSON para a memória
        public static void CarregarDados()
        {
            if (File.Exists(caminhoArquivo))
            {
                string json = File.ReadAllText(caminhoArquivo);
                Tarefas = JsonSerializer.Deserialize<List<TodoTask>>(json) ?? new List<TodoTask>();
            }
            else
            {
                Tarefas = new List<TodoTask>();
            }
        }

        // Salva as tarefas da memória para o arquivo JSON
        public static void SalvarDados()
        {
            string pasta = Path.GetDirectoryName(caminhoArquivo);
            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            string json = JsonSerializer.Serialize(Tarefas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(caminhoArquivo, json);
        }
    }
}
