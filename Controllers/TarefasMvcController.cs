using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TodoList.Models;
using TodoList.Data;
using System;
using System.Linq;

namespace projeto_c__main.Controllers
{
    public class TarefasMvcController : Controller
    {
        public IActionResult Index(string pesquisa, string ordenarPor, string direcao)
        {
            BancoDados.CarregarDados(); 
            var tarefas = BancoDados.Tarefas.AsQueryable();
            
            // Aplica filtro de pesquisa se foi fornecido
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                tarefas = tarefas.Where(t => 
                    t.Titulo.Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                    t.Usuario.Contains(pesquisa, StringComparison.OrdinalIgnoreCase));
                
                // Passa o termo de pesquisa para a view
                ViewBag.TermoPesquisa = pesquisa;
            }
        
        //ORDENAÇÃO
        // Verifica se uma ordenação foi solicitada
        if (!string.IsNullOrWhiteSpace(ordenarPor))
        {
            string direcaoAtual;

            // Alterna entre asc e desc quando a mesma coluna for clicada novamente
            if (ViewBag.OrdenarPor != null && ViewBag.OrdenarPor.ToString() == ordenarPor)
            {
                // Usa a direção anterior (vinda do ViewBag) para alternar
                direcaoAtual = ViewBag.Direcao == "asc" ? "desc" : "asc";
            }
            else
            {
                // Primeiro clique na coluna: começa com asc
                direcaoAtual = "asc";
            }

            // Aplica ordenação
            tarefas = direcaoAtual == "desc"
                ? ordenarPor.ToLower() switch
                {
                    "id" => tarefas.OrderByDescending(t => t.Id),
                    "titulo" => tarefas.OrderByDescending(t => t.Titulo),
                    "usuario" => tarefas.OrderByDescending(t => t.Usuario),
                    "status" => tarefas.OrderByDescending(t => t.Status),
                    "criadoem" => tarefas.OrderByDescending(t => t.CriadoEm),
                    "modificadoem" => tarefas.OrderByDescending(t => t.ModificadoEm),
                    "concluidoem" => tarefas.OrderByDescending(t => t.ConcluidoEm),
                    _ => tarefas.OrderByDescending(t => t.CriadoEm)
                }
                : ordenarPor.ToLower() switch
                {
                    "id" => tarefas.OrderBy(t => t.Id),
                    "titulo" => tarefas.OrderBy(t => t.Titulo),
                    "usuario" => tarefas.OrderBy(t => t.Usuario),
                    "status" => tarefas.OrderBy(t => t.Status),
                    "criadoem" => tarefas.OrderBy(t => t.CriadoEm),
                    "modificadoem" => tarefas.OrderBy(t => t.ModificadoEm),
                    "concluidoem" => tarefas.OrderBy(t => t.ConcluidoEm),
                    _ => tarefas.OrderBy(t => t.CriadoEm)
                };

            // Salva as informações para a próxima requisição
            ViewBag.OrdenarPor = ordenarPor;
            ViewBag.Direcao = direcaoAtual;
        }
        else
        {
            // Ordenação padrão
            tarefas = tarefas.OrderByDescending(t => t.CriadoEm);
            ViewBag.OrdenarPor = "CriadoEm";
            ViewBag.Direcao = "desc";
        }

        return View("~/View/Index.cshtml", tarefas.ToList());
    }

        // Exibe o formulário de criação de tarefa
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/View/Create.cshtml");
        }

        // Processa o cadastro de uma nova tarefa
        [HttpPost]
        public IActionResult Create(TodoTask tarefa)
        {
            if (ModelState.IsValid)
            {
                BancoDados.CarregarDados();
                // Gera novo ID e define data de criação
                tarefa.Id = BancoDados.Tarefas.Any() ? BancoDados.Tarefas.Max(t => t.Id) + 1 : 1;
                tarefa.CriadoEm = DateTime.Now;
                BancoDados.Tarefas.Add(tarefa);
                BancoDados.SalvarDados();
                return RedirectToAction("Index");
            }
            return View("~/View/Create.cshtml", tarefa);
        }

        // Exibe o formulário de edição de tarefa
        [HttpGet]
        public IActionResult Edit(int id)
        {
            BancoDados.CarregarDados();
            var tarefa = BancoDados.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null) return NotFound();
            return View("~/View/Edit.cshtml", tarefa);
        }

        // Processa a edição de uma tarefa
        [HttpPost]
        public IActionResult Edit(TodoTask tarefa)
        {
            if (ModelState.IsValid)
            {
                BancoDados.CarregarDados();
                var tarefaDb = BancoDados.Tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
                if (tarefaDb == null) return NotFound();
                
                // Preserva a data de criação original
                var criadoEmOriginal = tarefaDb.CriadoEm;
                var statusAnterior = tarefaDb.Status;
                
                // Atualiza os campos da tarefa
                tarefaDb.Titulo = tarefa.Titulo;
                tarefaDb.Usuario = tarefa.Usuario;
                tarefaDb.Status = tarefa.Status;
                tarefaDb.CriadoEm = criadoEmOriginal; 
                tarefaDb.ModificadoEm = DateTime.Now; 
                
                // Atualiza a data de conclusão apenas se o status mudou para concluído
                if (tarefa.Status && !statusAnterior)
                {
                    tarefaDb.ConcluidoEm = DateTime.Now;
                }
                else if (!tarefa.Status && statusAnterior)
                {
                    // Se mudou de concluído para pendente, remove a data de conclusão
                    tarefaDb.ConcluidoEm = null;
                }
                // Se o status não mudou, mantém a data de conclusão original
                
                BancoDados.SalvarDados();
                return RedirectToAction("Index");
            }
            return View("~/View/Edit.cshtml", tarefa);
        }

        // Exibe a tela de confirmação de exclusão
        [HttpGet]
        public IActionResult Delete(int id)
        {
            BancoDados.CarregarDados();
            var tarefa = BancoDados.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null) return NotFound();
            return View("~/View/Delete.cshtml", tarefa);
        }

        // Processa a exclusão da tarefa
        [HttpPost]
        public IActionResult Delete(TodoTask tarefa)
        {
            BancoDados.CarregarDados();
            var tarefaDb = BancoDados.Tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
            if (tarefaDb == null) return NotFound();
            BancoDados.Tarefas.Remove(tarefaDb);
            BancoDados.SalvarDados();
            return RedirectToAction("Index");
        }
    }
}

