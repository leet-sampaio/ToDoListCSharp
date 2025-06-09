using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TodoList.Models;
using TodoList.Data;
using System;
using System.Linq;

namespace projeto_c__main.Controllers
{
    // Controller responsável pelas telas web (MVC)
    public class TarefasMvcController : Controller
    {
        // Exibe a tela de login (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/View/Login.cshtml");
        }

        // Processa o login (POST)
        [HttpPost]
        public IActionResult Login(string NomeUsuario, string TipoUsuario)
        {
            // Valida se os campos foram preenchidos
            if (string.IsNullOrWhiteSpace(NomeUsuario) || string.IsNullOrWhiteSpace(TipoUsuario))
            {
                ViewBag.Erro = "Preencha todos os campos.";
                return View("~/View/Login.cshtml");
            }
            // Salva o nome e tipo do usuário na sessão
            HttpContext.Session.SetString("NomeUsuario", NomeUsuario);
            HttpContext.Session.SetString("TipoUsuario", TipoUsuario);
            return RedirectToAction("Index");
        }

        // Garante que o usuário esteja logado antes de acessar outras telas
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            var action = context.ActionDescriptor.RouteValues["action"];
            if (action != "Login")
            {
                var nomeUsuario = HttpContext.Session.GetString("NomeUsuario");
                var tipoUsuario = HttpContext.Session.GetString("TipoUsuario");
                if (string.IsNullOrEmpty(nomeUsuario) || string.IsNullOrEmpty(tipoUsuario))
                {
                    context.Result = RedirectToAction("Login");
                }
            }
            base.OnActionExecuting(context);
        }

        // Lista todas as tarefas
        public IActionResult Index()
        {
            BancoDados.CarregarDados(); // Carrega tarefas do arquivo
            var tarefas = BancoDados.Tarefas.ToList();
            return View("~/View/Index.cshtml", tarefas);
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
                // Atualiza os campos da tarefa
                tarefaDb.Titulo = tarefa.Titulo;
                tarefaDb.Usuario = tarefa.Usuario;
                tarefaDb.Status = tarefa.Status;
                tarefaDb.ConcluidoEm = tarefa.Status ? DateTime.Now : null;
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
