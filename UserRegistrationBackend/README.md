# User Registration API

API REST para gerenciamento de usuários com autenticação JWT, desenvolvida em ASP.NET Core 8.0.

## 🚀 Funcionalidades

- Autenticação via JWT (JSON Web Token)
- CRUD completo de usuários
- Hash seguro de senhas
- Validações de dados
- Documentação via Swagger

## 🛠️ Tecnologias

- ASP.NET Core 8.0
- Entity Framework Core
- MySQL
- JWT Authentication
- Swagger

## 📋 Pré-requisitos

- .NET 8.0 SDK
- MySQL Server
- IDE (VS Code, Visual Studio)

## ⚙️ Configuração

1. Clone o repositório
```bash
git clone https://github.com/henriquerfmendes/user-registration-api.git
cd user-registration-api
```

2. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`
```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=UserRegistrationDb;User=user_example;Password=password_example;"
    }
}
```

3. Configure as credenciais JWT no arquivo `appsettings.json`
```json
{
    "JwtConfig": {
        "Secret": "this_is_a_super_secret_key_with_minimum_32_chars_here_2024",
        "Issuer": "UserRegistrationAPI",
        "Audience": "UserRegistrationClient",
        "ExpirationInMinutes": 60
    }
}
```

4. Execute as migrações
```bash
dotnet ef database update
```

5. Inicie a aplicação
```bash
dotnet run
```

## 🔀 Endpoints

### Autenticação
- `POST /api/auth/login` - Login de usuário

### Usuários
- `POST /api/user` - Criar usuário
- `GET /api/user` - Listar usuários (requer autenticação)
- `GET /api/user/{id}` - Buscar usuário por ID (requer autenticação)
- `PUT /api/user/{id}` - Atualizar usuário (requer autenticação)
- `DELETE /api/user/{id}` - Deletar usuário (requer autenticação)

## 📦 Estrutura do Projeto

src/
├── Controllers/ # Controladores da API
├── Services/ # Lógica de negócios
├── Repositories/ # Acesso a dados
├── Models/ # Entidades
├── DTOs/ # Objetos de transferência de dados
├── Validators/ # Validações
└── Configurations/ # Configurações

## 🔒 Segurança

- Senhas armazenadas com hash + salt
- Autenticação via JWT
- Validação de tokens
- Proteção contra emails duplicados

## 📝 Documentação

A documentação da API está disponível via Swagger UI em:

```
http://localhost:5203/swagger
```
ou
```
https://localhost:7203/swagger
```
