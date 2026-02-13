# AI Planner

Планувальник часу з AI-рекомендаціями.

## Документація

- [**Чеклист**](CHECKLIST.md) — плани, кроки, прогрес
- [**Огляд**](docs/OVERVIEW.md) — функціонал, стек, домен
- [**Архітектура**](docs/ARCHITECTURE.md) — Clean Architecture, структура

## Стек

**Backend:** ASP.NET Core 8, EF Core, PostgreSQL, Swagger  
**Auth:** ASP.NET Core Identity, JWT, Google OAuth  
**Tools:** FluentValidation, Docker Compose, xUnit, Moq  

**Плановано:** Blazor WebAssembly, .NET MAUI, OpenAI, Hangfire/RabbitMQ

## Запуск

```bash
# PostgreSQL
docker compose up -d

# API
cd src/Planner.API && dotnet run
```

Swagger: `https://localhost:7055/swagger`
