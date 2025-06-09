# Csharp

## Revisão C# - Gerenciador de Tarefas

Este projeto é uma aplicação ASP.NET Core MVC para gerenciamento de tarefas (ToDo List), utilizando um arquivo JSON como banco de dados.

### Tecnologias Utilizadas
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- Armazenamento de dados em arquivo JSON
- Bootstrap 5 para o front-end
- Razor Views

### Funcionalidades
- Cadastro e login de usuários
- CRUD de tarefas (criar, listar, editar, excluir, concluir)
- Interface responsiva para dispositivos móveis
- Persistência dos dados em `Data/dados.json`

### Estrutura do Projeto
- `Controllers/` - Lógica de controle das rotas e ações
- `Models/` - Modelos de dados (ex: TodoTask)
- `View/` - Páginas Razor (HTML + C#)
- `Data/dados.json` - Arquivo que armazena as tarefas e usuários

### Como Executar
1. Instale o .NET 8 SDK
2. Execute `dotnet run` na pasta do projeto
3. Acesse `http://localhost:5000` no navegador

---

> **Observação:** Este projeto utiliza um arquivo JSON como banco de dados, facilitando testes e uso local sem necessidade de um SGBD tradicional.
