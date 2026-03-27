\# TaskFlow API



API REST para gerenciamento de tarefas desenvolvida com \*\*C#\*\*, \*\*ASP.NET Core\*\*, \*\*Entity Framework Core\*\* e \*\*SQLite\*\*.



O projeto permite autenticação de usuários com \*\*JWT\*\*, criação e gerenciamento de tarefas por usuário, controle de status, prioridade, prazo e filtros úteis para produtividade.



\---



\## Objetivo



O \*\*TaskFlow API\*\* foi desenvolvido como projeto de portfólio para demonstrar conhecimentos em:



\- desenvolvimento back-end com ASP.NET Core

\- autenticação e autorização com JWT

\- persistência com Entity Framework Core e SQLite

\- CRUD com regras de negócio

\- filtros por usuário, status e prioridade

\- documentação e testes com Swagger



\---



\## Funcionalidades



\- Cadastro de usuários

\- Login com autenticação JWT

\- Rota protegida de perfil

\- CRUD de tarefas

\- Associação de tarefas por usuário autenticado

\- Filtro por status

\- Filtro por prioridade

\- Consulta de tarefas atrasadas

\- Endpoint para concluir tarefa

\- Endpoint para reabrir tarefa

\- Validações básicas de status, prioridade e título

\- Documentação com Swagger



\---



\## Tecnologias utilizadas



\- \*\*C#\*\*

\- \*\*ASP.NET Core Web API\*\*

\- \*\*Entity Framework Core\*\*

\- \*\*SQLite\*\*

\- \*\*JWT Authentication\*\*

\- \*\*Swagger / Swashbuckle\*\*

\- \*\*Git / GitHub\*\*



\---



\## Estrutura do projeto



```bash

TaskFlowApi/

│

├── Controllers/

│   ├── AuthController.cs

│   └── TasksController.cs

│

├── Data/

│   └── AppDbContext.cs

│

├── DTOs/

│   ├── Auth/

│   │   ├── RegisterRequest.cs

│   │   └── LoginRequest.cs

│   └── TaskItem/

│       ├── CreateTaskItemDto.cs

│       ├── UpdateTaskItemDto.cs

│       └── TaskItemResponseDto.cs

│

├── Models/

│   ├── User.cs

│   └── TaskItem.cs

│

├── Services/

│   └── TokenService.cs

│

├── Migrations/

├── Program.cs

├── appsettings.json

└── TaskFlowApi.csproj



Como executar o projeto

1\. Clonar o repositório

&#x20;git clone https://github.com/iohanaallen/taskflow-api.git

cd taskflow-api



2\. Restaurar dependências

&#x20;dotnet restore



3\. Aplicar migrations

&#x20;dotnet ef database update



4\. Executar a aplicação

&#x20;dotnet run



5\. Acessar o Swagger

&#x20;http://localhost:5186/swagger



Configuração JWT



No arquivo appsettings.json, a API utiliza a seção:

&#x20;"JwtSettings": {

&#x20; "SecretKey": "taskflowapi-super-secret-key-2026-minimo-32-caracteres",

&#x20; "Issuer": "TaskFlowApi",

&#x20; "Audience": "TaskFlowApiClient"

}



Fluxo de autenticação

&#x20;Registrar usuário em POST /api/Auth/register

&#x20;Fazer login em POST /api/Auth/login

&#x20;Copiar o token retornado

&#x20;Clicar em Authorize no Swagger

&#x20;Informar o token no campo de autenticação



Endpoints principais:

Auth



&#x20;POST /api/Auth/register

&#x20;POST /api/Auth/login

&#x20;GET /api/Auth/profile



Tasks



&#x20;POST /api/Tasks

&#x20;GET /api/Tasks

&#x20;GET /api/Tasks/{id}

&#x20;PUT /api/Tasks/{id}

&#x20;DELETE /api/Tasks/{id}

&#x20;GET /api/Tasks/status/{status}

&#x20;GET /api/Tasks/priority/{priority}

&#x20;GET /api/Tasks/overdue

&#x20;PATCH /api/Tasks/{id}/complete

&#x20;PATCH /api/Tasks/{id}/reopen





Exemplo de criação de tarefa:

&#x20;{

&#x20; "titulo": "Finalizar README do projeto",

&#x20; "descricao": "Escrever documentação completa do TaskFlowApi",

&#x20; "prioridade": "Alta",

&#x20; "dataLimite": "2026-03-31T18:00:00"

}





Regras de negócio implementadas

&#x20;Cada usuário só pode visualizar e gerenciar as próprias tarefas

&#x20;O título da tarefa é obrigatório

&#x20;Prioridade aceita apenas: Baixa, Media e Alta

&#x20;Status aceita apenas: Pendente, EmAndamento e Concluida

&#x20;Apenas tarefas concluídas podem ser reabertas

&#x20;Tarefas atrasadas podem ser filtradas por endpoint específico



Melhorias futuras

&#x20;Paginação e ordenação

&#x20;Filtro por data

&#x20;Roles de usuário

&#x20;PostgreSQL

&#x20;Docker

&#x20;Deploy em nuvem

&#x20;Testes unitários com xUnit





Autor



Desenvolvido por Iohana Allen

LinkedIn: https://www.linkedin.com/in/iohana-allen



GitHub: https://github.com/iohanaallen



