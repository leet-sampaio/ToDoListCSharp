using System;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
using TodoList.Models;
using System.Collections.Generic;
using System.Linq;

namespace TarefasApi.Controllers
{
    [ApiController]
    [Route("api/tarefas")]
    public class TarefasController : ControllerBase
    {

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromQuery] string tipoUsuario, [FromQuery] string nomeUsuario)
        {
            BancoDados.CarregarDados();

            var tarefa = BancoDados.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
                return NotFound("Tarefa não encontrada.");

            if (tipoUsuario == "admin" || tarefa.Usuario == nomeUsuario)
            {
                BancoDados.Tarefas.Remove(tarefa);
                BancoDados.SalvarDados();
                return Ok("Tarefa apagada com sucesso.");
            }

            return Forbid("Você não tem permissão para apagar esta tarefa.");
        }

        [HttpPost("{id}/status")]
        public IActionResult AtualizarStatus(int id, [FromQuery] string tipoUsuario, [FromQuery] string nomeUsuario)
        {
            BancoDados.CarregarDados();

            var tarefa = BancoDados.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
                return NotFound("Tarefa não encontrada.");

            if (tipoUsuario == "admin" || tarefa.Usuario == nomeUsuario)
            {
                tarefa.Status = !tarefa.Status;
                BancoDados.SalvarDados();
                return Ok("Status atualizado com sucesso.");
            }

            return Forbid("Você não tem permissão para atualizar esta tarefa.");
        }
        [HttpPost]
        public IActionResult CriarTarefa([FromBody] NovaTarefaDto novaTarefa)
        {
            if (string.IsNullOrWhiteSpace(novaTarefa.Usuario) || string.IsNullOrWhiteSpace(novaTarefa.Titulo))
            {
                return BadRequest("Os campos 'Usuário' e 'Título' são obrigatórios.");
            }

            BancoDados.CarregarDados();

            int novoId = BancoDados.Tarefas.Any()
                ? BancoDados.Tarefas.Max(t => t.Id) + 1
                : 1;

            var tarefa = new TodoTask
            {
                Id = novoId,
                Usuario = novaTarefa.Usuario,
                Titulo = novaTarefa.Titulo,
                Status = false,
                CriadoEm = DateTime.Now,
                ConcluidoEm = null
            };

            BancoDados.Tarefas.Add(tarefa);
            BancoDados.SalvarDados();

            return Ok(tarefa);
        }


    }

}
