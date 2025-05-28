# Lead Manager

Sistema full stack para gerenciamento de **leads comerciais**, desenvolvido como parte de um teste técnico para vaga Full Stack (.NET + React).

---

## 🧰 Tecnologias Utilizadas

- **Backend**: .NET 6 Web API, Entity Framework Core, CQRS com MediatR, SQL Server
- **Frontend**: React + TypeScript + Axios + React Router
- **Infraestrutura**: Docker, Docker Compose
- **Outros**: Clean Architecture, Context API, Fake Email Service

---

## 📁 Estrutura do Projeto

```
lead-manager/
├── LeadManager.API/            # API .NET 6
├── LeadManager.Application/    # Regras de negócio e CQRS
├── LeadManager.Domain/         # Entidades e contratos
├── LeadManager.Infrastructure/ # Banco de dados, serviços
├── LeadManager.Tests/          # Testes unitários
├── frontend/                   # SPA em React + TS
└── docker-compose.yml          # Orquestração com SQL Server
```

---

## 🚀 Como Executar

### Pré-requisitos:

- Docker e Docker Compose instalados
- Node.js 24.1.0 (caso queira rodar frontend localmente)

---

### Executar com Docker (recomendado)

```bash
docker-compose up --build
```

#### Serviços expostos:

| Serviço     | URL                          |
|-------------|-------------------------------|
| Frontend    | http://localhost:5173         |
| API         | https://localhost:7009         |
| Swagger     | http://localhost:7009/swagger |
| SQL Server  | localhost:1433                |

---

## 🔧 Executar Localmente (sem Docker)

### 1. API

```bash
cd LeadManager.API
dotnet ef database update
dotnet run
```

### 2. Frontend

```bash
cd frontend
npm install
npm run dev
```

---

## 📚 Funcionalidades

- Visualização de **leads no status "Invited"**
- Botões para **Aceitar** ou **Recusar** o lead
- Aplicação de **10% de desconto** se preço > $500
- Atualização de status no banco de dados
- Mock de envio de e-mail para `vendas@test.com`
- Visualização dos leads **aceitos** em outra aba

---

## 🧪 Testes

```bash
cd LeadManager.Tests
dotnet test
```

---

## 🛠️ Padrões Aplicados

- Clean Architecture
- CQRS com MediatR
- DDD básico
- Separação de responsabilidades (API, Application, Domain, Infrastructure)
