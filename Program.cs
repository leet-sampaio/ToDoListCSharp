// Programa principal do ASP.NET Core
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a controllers MVC (Views)
builder.Services.AddControllersWithViews();

// Adiciona suporte a sessão (para login)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Ativa o uso de sessão
app.UseSession();

// Define a rota padrão para o MVC (abre TarefasMvc/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TarefasMvc}/{action=Index}/{id?}");

app.Run();




// using System;
// using TodoList.Controllers;
// using TodoList.Data;

// namespace TodoList
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             BancoDados.CarregarDados();

//             var userAccess = new UserAccessController();
//             string tipoUsuario = null;
//             while (tipoUsuario == null)
//             {
//                 tipoUsuario = userAccess.SelecionarTipoUsuario();
//                 if (tipoUsuario == null)
//                 {
//                     Console.WriteLine("Opção inválida. Tente novamente.");
//                 }
//             }

//             Console.Write("Digite seu nome de usuário: ");
//             string nomeUsuario = Console.ReadLine();

//             var menu = new MenuController();
//             var getTarefas = new GetTarefasController();
//             var postTarefa = new PostTarefaController();
//             var deleteTarefa = new DeleteTarefaController();
//             var updateTarefa = new UpdateTaskController();

//             bool continuar = true;

//             while (continuar)
//             {
//                 menu.MostrarMenu(tipoUsuario);
//                 string opcao = Console.ReadLine();

//                 switch (opcao)
//                 {
//                     case "1":  // Listar tarefas
//                         var tarefas = getTarefas.ListarTarefas(tipoUsuario, nomeUsuario);
//                         Console.Clear();
//                         Console.WriteLine("== Tarefas ==");
//                         foreach (var t in tarefas)
//                         {
//                             Console.WriteLine($"ID: {t.Id} | Título: {t.Titulo} | Status: {(t.Status ? "Concluído" : "Pendente")} | Criado em: {t.CriadoEm} | Concluído em: {(t.ConcluidoEm.HasValue ? t.ConcluidoEm.ToString() : "não concluído")} | Usuário: {t.Usuario}");
//                         }
//                         Console.WriteLine("\nPressione ENTER para voltar...");
//                         Console.ReadLine();
//                         break;

//                     case "2":  // Criar tarefa
//                         Console.Write("Digite o título da tarefa: ");
//                         string titulo = Console.ReadLine();
//                         postTarefa.CriarTarefa(titulo, nomeUsuario);
//                         Console.WriteLine("Tarefa criada com sucesso!");
//                         Console.WriteLine("Pressione ENTER para voltar...");
//                         Console.ReadLine();
//                         break;

//                     case "3":  // Apagar tarefa
//                         Console.Write("Digite o ID da tarefa que deseja apagar: ");
//                         if (int.TryParse(Console.ReadLine(), out int idApagar))
//                         {
//                             bool apagou = deleteTarefa.ApagarTarefa(idApagar, tipoUsuario, nomeUsuario);
//                             if (apagou)
//                                 Console.WriteLine("Tarefa apagada com sucesso!");
//                             else
//                                 Console.WriteLine("Não foi possível apagar a tarefa (não encontrada ou sem permissão).");
//                         }
//                         else
//                         {
//                             Console.WriteLine("ID inválido.");
//                         }
//                         Console.WriteLine("Pressione ENTER para voltar...");
//                         Console.ReadLine();
//                         break;

//                     case "4":  // Atualizar status da tarefa
//                         Console.Write("Digite o ID da tarefa que deseja atualizar o status: ");
//                         if (int.TryParse(Console.ReadLine(), out int idAtualizar))
//                         {
//                             bool atualizou = updateTarefa.AtualizarStatus(idAtualizar, tipoUsuario, nomeUsuario);
//                             if (atualizou)
//                                 Console.WriteLine("Status da tarefa atualizado com sucesso!");
//                             else
//                                 Console.WriteLine("Não foi possível atualizar o status da tarefa (não encontrada ou sem permissão).");
//                         }
//                         else
//                         {
//                             Console.WriteLine("ID inválido.");
//                         }
//                         Console.WriteLine("Pressione ENTER para voltar...");
//                         Console.ReadLine();
//                         break;

//                     case "0":
//                         continuar = false;
//                         break;

//                     default:
//                         Console.WriteLine("Opção inválida. Tente novamente.");
//                         Console.WriteLine("Pressione ENTER para voltar...");
//                         Console.ReadLine();
//                         break;
//                 }
//             }

//             Console.WriteLine("Obrigado por usar o TodoApp!");
//         }
//     }
// }
