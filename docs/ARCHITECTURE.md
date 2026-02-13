# Архітектура AI Planner

## Clean Architecture

```
Core → Infrastructure → Services → API
```

### Шари

| Шар | Проєкт | Призначення |
|-----|--------|-------------|
| **Core** | Planner.Core | Домен, сутності, інтерфейси. Без зовнішніх залежностей. |
| **Infrastructure** | Planner.Infrastructure | EF Core, репозиторії, Identity. Реалізує інтерфейси з Core. |
| **Services** | Planner.Services | Бізнес-логіка, AI (пізніше). Залежить від Core. |
| **API** | Planner.API | Контролери, DI, middleware, валідація. Точка входу. |

### Залежності

```
API → Services, Infrastructure
Services → Core
Infrastructure → Core
```

**Правило:** Core не залежить ні від чого. Зовнішні шари посилаються на внутрішні.

---

## Структура проєкту

```
Planner/
├── src/
│   ├── Planner.API/              # ASP.NET Core Web API ✅
│   ├── Planner.Core/             # Домен, інтерфейси ✅
│   ├── Planner.Infrastructure/   # EF Core, репозиторії, Identity ✅
│   ├── Planner.Services/        # Бізнес-логіка ✅
│   ├── Planner.Web/              # Blazor WebAssembly (плановано)
│   └── Planner.Desktop/          # .NET MAUI (плановано)
├── tests/
│   └── Planner.API.Tests/       # xUnit, Moq, coverlet ✅
├── docs/
├── .cursor/rules/
└── Planner.sln
```

---

## Патерни

- **Repository** — абстракція доступу до даних (`IPlannerTaskRepository`)
- **Dependency Injection** — впровадження залежностей через конструктор
- **Services** — бізнес-логіка в окремих класах (`IPlannerTaskService`)
- **DTO** — PlannerTaskDto, CreatePlannerTaskRequest, UpdatePlannerTaskRequest замість entity в API
- **Current User** — `ICurrentUserService` для отримання UserId з JWT

---

## API

- **Маршрути:** `api/planner-tasks`, `api/auth`, lowercase URLs
- **Валідація:** FluentValidation для Request-моделей
- **Помилки:** глобальний exception handler, ProblemDetails
- **Документація:** Swagger/OpenAPI
- **Health:** `/health` — перевірка PostgreSQL

---

## База даних

- **СУБД:** PostgreSQL 16
- **ORM:** Entity Framework Core (Npgsql)
- **Міграції:** EF Core Migrations
- **Identity:** ASP.NET Core Identity (ApplicationUser, AspNet*)
