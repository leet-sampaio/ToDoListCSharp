# 📋 Sistema de Gerenciamento de Tarefas (ToDo List)

Um sistema completo de gerenciamento de tarefas desenvolvido em ASP.NET Core MVC com armazenamento em JSON, oferecendo uma interface web responsiva e uma API RESTful.

## 🎯 Propósito do Projeto

Este projeto serve como um sistema de gerenciamento de tarefas (ToDo List) que permite aos usuários:
- **Criar** tarefas com título, usuário e timestamp automático
- **Listar** todas as tarefas com pesquisa e ordenação
- **Editar** tarefas existentes com controle de modificação
- **Excluir** tarefas com confirmação de segurança
- Pesquisar tarefas por título ou usuário
- Ordenar tarefas por diferentes critérios (ID, título, usuário, status, datas)
- Marcar tarefas como concluídas
- Visualizar histórico de modificações com timestamps

### 🔧 **Operações CRUD Implementadas**
O sistema implementa as **4 operações fundamentais de CRUD**:
- **CREATE** - Criação de novas tarefas via formulário web e API
- **READ** - Leitura e listagem de tarefas com filtros e ordenação
- **UPDATE** - Atualização de tarefas existentes com controle de versão
- **DELETE** - Exclusão de tarefas via interface web e API RESTful

## 🏗️ Arquitetura e Tecnologias

### **Linguagens de Programação Utilizadas**
- **C#** - Linguagem principal para desenvolvimento backend (.NET 8)
- **HTML5** - Estruturação de páginas web e formulários
- **CSS3** - Estilização e design responsivo da interface
- **JavaScript** - Interatividade do cliente e confirmações
- **JSON** - Formato de armazenamento e troca de dados
- **Razor Syntax** - Template engine para views dinâmicas (C# + HTML)

### **Backend (.NET 8)**
- **ASP.NET Core MVC** - Framework principal para desenvolvimento web
- **C#** - Linguagem de programação
- **Entity Framework Core** - ORM para manipulação de dados
- **System.Text.Json** - Serialização/deserialização JSON
- **Armazenamento em JSON** - Persistência de dados em arquivo

### **Frontend**
- **Razor Views** - Engine de templates do ASP.NET Core
- **HTML5 + CSS3** - Estrutura e estilização
- **Bootstrap 5** - Framework CSS para responsividade
- **JavaScript** - Interatividade do cliente

## 📁 Estrutura de Pastas (Padrão MVC)

```
ToDoListCSharp/
├── Controllers/              # Controladores (Lógica de negócio)
│   ├── TarefasMvcController.cs    # Controller MVC para interface web
│   └── TarefasController.cs       # Controller API RESTful
├── Models/                   # Modelos de dados
│   └── TodoTask.cs               # Modelo da tarefa e DTOs
├── View/                     # Views (Interface do usuário)
│   ├── Index.cshtml              # Lista de tarefas
│   ├── Create.cshtml             # Formulário de criação
│   ├── Edit.cshtml               # Formulário de edição
│   └── Delete.cshtml             # Confirmação de exclusão
├── Data/                     # Camada de dados
│   ├── AppDbContext.cs           # Contexto de banco de dados
│   └── dados.json                # Arquivo de armazenamento
├── Properties/               # Configurações do projeto
│   └── launchSettings.json       # Configurações de execução
├── Program.cs                # Ponto de entrada da aplicação
├── Final.csproj             # Arquivo de projeto
├── appsettings.json         # Configurações da aplicação
└── README.md                # Documentação
```

## 🔧 Backend Detalhado

### **Controllers**

#### `TarefasMvcController.cs`
- **Responsabilidade**: Interface web MVC
- **Operações CRUD implementadas**:
  - **CREATE**: `Create()` - Formulário e criação de tarefas
  - **READ**: `Index()` - Lista tarefas com pesquisa e ordenação
  - **UPDATE**: `Edit()` - Formulário e edição de tarefas
  - **DELETE**: `Delete()` - Confirmação e exclusão de tarefas
- **Funcionalidades extras**:
  - Pesquisa por título e usuário
  - Ordenação dinâmica por qualquer coluna
  - Controle de timestamps automático

#### `TarefasController.cs`
- **Responsabilidade**: API RESTful
- **Operações CRUD via API**:
  - **DELETE**: `DELETE api/tarefas/{id}` - Exclusão via API com controle de permissão
- **Características**:
  - Controle de autorização por usuário
  - Respostas em formato JSON
  - Validação de dados de entrada

### **Models**

#### `TodoTask.cs`
```csharp
public class TodoTask
{
    public int Id { get; set; }                    // Identificador único
    public string Titulo { get; set; }             // Título da tarefa
    public string Usuario { get; set; }            // Usuário responsável
    public bool Status { get; set; }               // Status (concluída/pendente)
    public DateTime CriadoEm { get; set; }         // Data de criação
    public DateTime? ConcluidoEm { get; set; }     // Data de conclusão
    public DateTime? ModificadoEm { get; set; }    // Data de modificação
}
```

### **Data Layer**

#### `AppDbContext.cs` (BancoDados)
- **Classe estática** para gerenciamento de dados
- **Linguagem**: C# com System.Text.Json
- **Operações CRUD de baixo nível**:
  - **CREATE/UPDATE**: `SalvarDados()` - Persiste dados no arquivo JSON
  - **READ**: `CarregarDados()` - Carrega tarefas do arquivo JSON
  - **Utilities**: `ObterProximoId()` - Gera ID sequencial automático
- **Características**:
  - Serialização/deserialização automática
  - Gerenciamento de IDs únicos
  - Controle de concorrência básico

## 🎨 Frontend Detalhado

### **Tecnologias de Interface**
- **Razor Pages** - Templates dinâmicos C# + HTML
- **Bootstrap 5** - Grid system, componentes e responsividade
- **CSS customizado** - Estilização personalizada com cores pastéis
- **JavaScript** - Confirmações de exclusão e interatividade

### **Características da Interface**
- ✅ **Responsiva** - Adaptável para desktop, tablet e mobile
- ✅ **Pesquisa em tempo real** - Filtro por título e usuário
- ✅ **Ordenação interativa** - Clique nos cabeçalhos para ordenar
- ✅ **Status visuais** - Badges coloridos para status das tarefas
- ✅ **Confirmações** - Avisos antes de exclusões
- ✅ **Timestamps** - Exibição de datas de criação, modificação e conclusão

## 💾 Armazenamento de Dados (JSON)

### **Estrutura do arquivo `dados.json`**
```json
[
  {
    "Id": 1,
    "Titulo": "Implementar autenticação",
    "Usuario": "João Silva",
    "Status": false,
    "CriadoEm": "2024-12-19T10:30:00",
    "ConcluidoEm": null,
    "ModificadoEm": "2024-12-19T11:45:00"
  },
  {
    "Id": 2,
    "Titulo": "Criar documentação",
    "Usuario": "Maria Santos",
    "Status": true,
    "CriadoEm": "2024-12-19T09:15:00",
    "ConcluidoEm": "2024-12-19T14:20:00",
    "ModificadoEm": null
  }
]
```

### **Vantagens do Armazenamento JSON**
- ✅ **Simplicidade** - Não requer configuração de banco de dados
- ✅ **Portabilidade** - Fácil backup e migração
- ✅ **Legibilidade** - Formato human-readable
- ✅ **Performance** - Rápido para pequenos volumes
- ⚠️ **Limitações** - Não ideal para grandes volumes ou acesso concorrente

## 🌐 API RESTful

### **Endpoints Disponíveis**

#### **Interface Web (MVC)**
- `GET /TarefasMvc` - Lista todas as tarefas
- `GET /TarefasMvc?pesquisa={termo}` - Pesquisa tarefas
- `GET /TarefasMvc?ordenarPor={campo}&direcao={asc|desc}` - Ordenação
- `GET /TarefasMvc/Create` - Formulário de criação
- `POST /TarefasMvc/Create` - Criar nova tarefa
- `GET /TarefasMvc/Edit/{id}` - Formulário de edição
- `POST /TarefasMvc/Edit/{id}` - Atualizar tarefa
- `GET /TarefasMvc/Delete/{id}` - Confirmação de exclusão
- `POST /TarefasMvc/Delete/{id}` - Excluir tarefa

#### **API RESTful**
- `DELETE /api/tarefas/{id}?tipoUsuario={tipo}&nomeUsuario={nome}` - Excluir tarefa via API

### **Exemplo de Uso da API**

```bash
# Excluir tarefa via API
curl -X DELETE "http://localhost:5000/api/tarefas/1?tipoUsuario=admin&nomeUsuario=João"
```

### **Respostas da API**
```json
// Sucesso
{
  "message": "Tarefa apagada com sucesso."
}

// Erro - Tarefa não encontrada
{
  "error": "Tarefa não encontrada."
}

// Erro - Sem permissão
{
  "error": "Você não tem permissão para apagar esta tarefa."
}
```

## 🚀 Como Inicializar o Projeto

### **Pré-requisitos**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado
- Editor de código (Visual Studio, VS Code, etc.)
- Navegador web moderno

### **Passo a Passo**

#### **1. Clone o Repositório**
```bash
git clone https://github.com/leet-sampaio/ToDoListCSharp.git
cd ToDoListCSharp
```

#### **2. Restaurar Dependências**
```bash
dotnet restore
```

#### **3. Executar a Aplicação**
```bash
dotnet run
```

#### **4. Acessar a Aplicação**
- **Interface Web**: `http://localhost:5000` ou `https://localhost:5001`
- **API**: `http://localhost:5000/api/tarefas`

### **URLs de Desenvolvimento**
- **Home**: `http://localhost:5000/TarefasMvc`
- **Nova Tarefa**: `http://localhost:5000/TarefasMvc/Create`
- **API Swagger**: `http://localhost:5000/swagger` (se habilitado)

### **Comandos Úteis**

```bash
# Executar em modo de desenvolvimento
dotnet run --environment Development

# Compilar para produção
dotnet publish -c Release

# Executar testes (se houver)
dotnet test

# Limpar arquivos de build
dotnet clean

# Verificar versão do .NET
dotnet --version
```

## 📊 Funcionalidades Implementadas

### **Operações CRUD Completas**
- ✅ **CREATE (Criar)** - Formulários web e API para criação de tarefas
- ✅ **READ (Ler)** - Listagem com pesquisa, filtros e ordenação
- ✅ **UPDATE (Atualizar)** - Edição de tarefas com controle de modificação
- ✅ **DELETE (Excluir)** - Remoção via interface web e API com confirmação

### **Funcionalidades Avançadas**
- ✅ **Pesquisa Avançada** - Por título e usuário
- ✅ **Ordenação Dinâmica** - Por qualquer coluna
- ✅ **Interface Responsiva** - Mobile-first design
- ✅ **Timestamps Automáticos** - Controle de datas
- ✅ **Status Management** - Controle de conclusão
- ✅ **API RESTful** - Endpoints para integração
- ✅ **Persistência JSON** - Armazenamento local
- ✅ **Validações** - Controle de entrada de dados

### **Linguagens e Tecnologias em Uso**
- ✅ **C# (.NET 8)** - Backend, lógica de negócio e APIs
- ✅ **HTML5** - Estrutura das páginas web
- ✅ **CSS3** - Estilização e design responsivo
- ✅ **JavaScript** - Interatividade e confirmações
- ✅ **JSON** - Armazenamento e transferência de dados
- ✅ **Razor Syntax** - Templates dinâmicos (C# + HTML)

## 🔄 Fluxo de Dados e CRUD

### **Arquitetura de Dados**
```
┌─────────────┐    ┌──────────────┐    ┌─────────────┐    ┌──────────────┐
│   Browser   │───▶│ Controller   │───▶│   Model     │───▶│   JSON File  │
│   (View)    │◀───│    (MVC)     │◀───│ (TodoTask)  │◀───│ (dados.json) │
└─────────────┘    └──────────────┘    └─────────────┘    └──────────────┘
     HTML/CSS/JS        C# CRUD           C# Classes         JSON Storage
```

### **Fluxo das Operações CRUD**

#### **CREATE (Criar Tarefa)**
1. **Frontend**: Formulário HTML com campos (Título, Usuário)
2. **Controller**: `TarefasMvcController.Create()` recebe dados
3. **Model**: Cria objeto `TodoTask` com timestamps
4. **Data**: `BancoDados.SalvarDados()` persiste em JSON

#### **READ (Listar/Pesquisar)**
1. **Frontend**: Página de listagem com filtros em HTML/JS
2. **Controller**: `TarefasMvcController.Index()` processa filtros
3. **Data**: `BancoDados.CarregarDados()` lê do arquivo JSON
4. **View**: Renderiza lista em Razor/HTML com dados

#### **UPDATE (Editar Tarefa)**
1. **Frontend**: Formulário de edição pré-preenchido
2. **Controller**: `TarefasMvcController.Edit()` atualiza objeto
3. **Model**: Atualiza `ModificadoEm` automaticamente
4. **Data**: Persiste alterações no arquivo JSON

#### **DELETE (Excluir Tarefa)**
1. **Frontend**: Confirmação JavaScript antes da exclusão
2. **Controller**: `TarefasMvcController.Delete()` ou API
3. **Data**: Remove tarefa da lista e salva JSON
4. **Response**: Retorna status de sucesso/erro

## 📝 Próximas Melhorias

- [ ] Autenticação e autorização de usuários
- [ ] Categorias e tags para tarefas
- [ ] Notificações de prazo
- [ ] Dashboard com estatísticas
- [ ] Export/Import de dados
- [ ] API completa (GET, POST, PUT)
- [ ] Migração para banco de dados
- [ ] Interface Dark Mode
- [ ] PWA (Progressive Web App)

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 👨‍💻 Autor

**Leet Sampaio**
- GitHub: [@leet-sampaio](https://github.com/leet-sampaio)
- LinkedIn: [Letícia](https://www.linkedin.com/in/let%C3%ADciasampaiofagundes/)

