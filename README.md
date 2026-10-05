# 🎧 DeskFlow API — Gestão de Chamados e Helpdesk de TI

Web API RESTful em .NET 10 + EF Core + SQL Server para centralizar chamados de suporte técnico.

## 🛠 Tecnologias
- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server
- Swagger / OpenAPI

## 🏛 Arquitetura em Camadas
- **Controllers** — rotas e status codes
- **Services** — regras de negócio (transições de status)
- **Repositories** — acesso a dados (EF Core)
- **Middlewares** — tratamento global de erros
- **Models** — Entities + Enums + Dtos
- **Data** — AppDbContext e Migrations

## 🚀 Como executar

Pré-requisitos: .NET SDK 10 e SQL Server (LocalDB, Express ou Docker).

1. Clone:
   `git clone https://github.com/SEU-USUARIO/deskflow-api.git`
2. Acesse: `cd deskflow-api/DeskFlow.API`
3. Ajuste `appsettings.json`:
   `"ConnectionStrings": { "DefaultConnection": "Server=localhost;Database=DeskFlowDb;Trusted_Connection=True;TrustServerCertificate=True;" }`
4. Crie o banco: `dotnet ef database update`
   (ou execute o `database.sql` na raiz do repositório)
5. Rode: `dotnet run`
6. Swagger: `http://localhost:5021/swagger`

## 📋 Endpoints
| Método | Rota | Descrição |
|---|---|---|
| POST | /api/categorias | Cadastrar categoria |
| GET | /api/categorias | Listar categorias |
| GET | /api/categorias/{id} | Buscar categoria |
| PUT | /api/categorias/{id} | Atualizar |
| DELETE | /api/categorias/{id} | Remover (bloqueia se tiver chamados) |
| POST | /api/chamados | Abrir chamado (status Aberto automático) |
| GET | /api/chamados?status=&prioridade=&categoriaId= | Listar com filtros |
| GET | /api/chamados/{id} | Detalhes com Categoria + Interações |
| PATCH | /api/chamados/{id}/iniciar | Aberto → EmAndamento |
| PATCH | /api/chamados/{id}/encerrar | → Fechado com Solução |
| POST | /api/chamados/{id}/interacoes | Adicionar comentário (bloqueado se Fechado) |

## 🔄 Ciclo de vida
Aberto → EmAndamento → Fechado (validado na camada Service).

## 🎥 Vídeo
[Link do vídeo](https://link-do-video)
