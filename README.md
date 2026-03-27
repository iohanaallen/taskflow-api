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

git clone https://github.com/iohanaallen/taskflow-api.git

cd taskflow-api



2\. Restaurar dependências

dotnet restore



3\. Aplicar migrations

dotnet ef database update



4\. Executar a aplicação

dotnet run



5\. Acessar o Swagger

http://localhost:5186/swagger



Configuração JWT



No arquivo appsettings.json, a API utiliza a seção:

"JwtSettings": {

 "SecretKey": "taskflowapi-super-secret-key-2026-minimo-32-caracteres",

 "Issuer": "TaskFlowApi",

 "Audience": "TaskFlowApiClient"

}



Fluxo de autenticação

Registrar usuário em POST /api/Auth/register

Fazer login em POST /api/Auth/login

Copiar o token retornado

Clicar em Authorize no Swagger

Informar o token no campo de autenticação



Endpoints principais:

Auth



POST /api/Auth/register

POST /api/Auth/login

GET /api/Auth/profile



Tasks



POST /api/Tasks

GET /api/Tasks

GET /api/Tasks/{id}

PUT /api/Tasks/{id}

DELETE /api/Tasks/{id}

GET /api/Tasks/status/{status}

GET /api/Tasks/priority/{priority}

GET /api/Tasks/overdue

PATCH /api/Tasks/{id}/complete

PATCH /api/Tasks/{id}/reopen





Exemplo de criação de tarefa:

{

 "titulo": "Finalizar README do projeto",

 "descricao": "Escrever documentação completa do TaskFlowApi",

 "prioridade": "Alta",

 "dataLimite": "2026-03-31T18:00:00"

}





Regras de negócio implementadas

Cada usuário só pode visualizar e gerenciar as próprias tarefas

O título da tarefa é obrigatório

Prioridade aceita apenas: Baixa, Media e Alta

Status aceita apenas: Pendente, EmAndamento e Concluida

Apenas tarefas concluídas podem ser reabertas

Tarefas atrasadas podem ser filtradas por endpoint específico



Melhorias futuras

Paginação e ordenação

Filtro por data

Roles de usuário

PostgreSQL

Docker

Deploy em nuvem

Testes unitários com xUnit





Autor



Desenvolvido por Iohana Allen

LinkedIn: https://www.linkedin.com/in/iohana-allen



GitHub: https://github.com/iohanaallen



