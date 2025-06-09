using System;

// Classe que representa uma tarefa do sistema
namespace TodoList.Models
{
    public class TodoTask
    {
        // Identificador único da tarefa
        public int Id { get; set; }
        // Título/descritivo da tarefa
        public string Titulo { get; set; } = string.Empty;
        // Nome do usuário responsável pela tarefa
        public string Usuario { get; set; }
        // Status: true = concluída, false = pendente
        public bool Status { get; set; } = false;
        // Data de criação da tarefa
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        // Data de conclusão (pode ser nula)
        public DateTime? ConcluidoEm { get; set; } = null;
    }
}