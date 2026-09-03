# TodoCs

API REST de tarefas (todos) e usuários, construída em **ASP.NET Core**.

## O que a API faz

- Cadastro de usuários e login com **JWT**
- CRUD de todos vinculados ao usuário autenticado
- Atualização e exclusão lógica de usuário (status `INACTIVE`)
- Documentação interativa com **Scalar** (OpenAPI) em ambiente de desenvolvimento

## Stack

| Tecnologia | Uso |
|---|---|
| .NET 10 / ASP.NET Core | API |
| Entity Framework Core | Persistência |
| SQL Server | Banco de dados |
| JWT Bearer | Autenticação |
| AutoMapper | Mapeamento entidade ↔ DTO |
| Scalar | Referência OpenAPI |

## Estrutura

```
Controllers/   Auth, User, Todo
Services/      regras de negócio e autenticação
Models/        User, Todo
Dtos/          contratos de entrada e saída
Mapping/       perfis AutoMapper
Database/      AppDbContext
Migrations/    schema inicial
```

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local ou remoto)
- Ferramenta de EF Core (opcional, para aplicar migrations):

```bash
dotnet tool install --global dotnet-ef
```

## Configuração

1. Clone o repositório e copie o arquivo de exemplo de ambiente:

```bash
cp .env.example .env
```

2. Preencha o `.env`:

```env
# DB
ConnectionStrings__DefaultConnection=Server=localhost;Database=TodoCs;Trusted_Connection=True;TrustServerCertificate=True;

# JWT
AppSettings__Secret=sua-chave-secreta-com-pelo-menos-32-caracteres
AppSettings__Issuer=TodoCs
AppSettings__Audience=TodoCs
```

A aplicação carrega variáveis de ambiente (incluindo `.env` via DotNetEnv) na inicialização.

3. Aplique as migrations:

```bash
dotnet ef database update
```

4. Execute:

```bash
dotnet run
```

Por padrão (perfil HTTPS):

- HTTPS: `https://localhost:7206`
- HTTP: `http://localhost:5065`

Documentação Scalar (somente Development): `https://localhost:7206/scalar`

## Autenticação

1. Crie um usuário em `POST /api/User` (rota pública).
2. Faça login em `POST /api/Auth/login`.
3. Envie o token nas demais rotas protegidas:

```http
Authorization: Bearer <token>
```

O token expira em **1 hora**. Senhas são armazenadas com `PasswordHasher` do ASP.NET Identity.

## Endpoints

### Auth — `api/Auth`

| Método | Rota | Auth | Descrição |
|---|---|---|---|
| POST | `/api/Auth/login` | Não | Login. Body: `{ "email", "password" }`. Retorna `{ token, expiresIn }` |

### User — `api/User`

| Método | Rota | Auth | Descrição |
|---|---|---|---|
| POST | `/api/User` | Não | Cria usuário. Body: `{ "firstName", "lastName", "email", "password" }` |
| GET | `/api/User` | JWT + role `Admin` | Lista usuários ativos |
| GET | `/api/User/{id}` | JWT | Retorna o próprio usuário |
| PUT | `/api/User/{id}` | JWT | Atualiza o próprio usuário |
| DELETE | `/api/User/{id}` | JWT | Inativa o próprio usuário (soft delete) |

### Todo — `api/Todo`

Todas as rotas exigem JWT. A listagem e a busca por id consideram apenas os todos do usuário autenticado.

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/Todo` | Lista os todos do usuário |
| GET | `/api/Todo/{id}` | Busca um todo |
| POST | `/api/Todo` | Cria todo. Body: `{ "title" }` |
| PUT | `/api/Todo/{id}` | Atualiza título e `isCompleted` |
| DELETE | `/api/Todo/{id}` | Remove o todo |

## Modelo de dados (resumo)

**User:** `Id`, `FirstName`, `LastName`, `Email`, `Password` (hash), `Status` (`ACTIVE` / `INACTIVE`).

**Todo:** `Id`, `Title`, `IsCompleted`, `CreatedAt`, `UserId` (FK para User, cascade no delete físico).

## Licença

Este projeto está licenciado sob a [MIT License](./LICENSE). A licença permite uso, cópia, modificação, fusão, publicação, distribuição e venda do software, desde que o aviso de copyright e a permissão sejam mantidos.

## Contato

Autor: Samuel Filho — [samuelfilho-dev/todo-api-cs](https://github.com/samuelfilho-dev/todo-api-cs)
