# Архітектура AI Planner

## Clean Architecture

```
Core → Infrastructure → Services → API
```

### Шари

| Шар | Проєкт | Призначення |
|-----|--------|-------------|
| **Core** | Planner.Core | Домен, сутності, інтерфейси. Без зовнішніх залежностей. |
| **Infrastructure** | Planner.Infrastructure | EF Core, репозиторії, зовнішні сервіси. Реалізує інтерфейси з Core. |
| **Services** | Planner.Services | Бізнес-логіка, AI. Залежить від Core. |
| **API** | Planner.API | Контролери, DI, middleware. Точка входу. |

### Залежності

```
API → Services, Infrastructure
Services → Core
Infrastructure → Core
```

**Правило:** Core не залежить ні від чого. Зовнішні шари посилаються на внутрішні.

---

## Структура (планована)

```
Planner/
├── src/
│   ├── Planner.API/              # ASP.NET Core Web API
│   ├── Planner.Core/            # Домен, інтерфейси
│   ├── Planner.Infrastructure/  # EF, репозиторії
│   ├── Planner.Services/       # Бізнес-логіка, AI
│   ├── Planner.Web/              # Blazor WebAssembly (пізніше)
│   └── Planner.Desktop/         # .NET MAUI (пізніше)
├── tests/
│   └── Planner.API.Tests/
├── docs/
├── .cursor/rules/
└── Planner.sln
```

---

## Патерни

- **Repository** — абстракція доступу до даних
- **Dependency Injection** — впровадження залежностей через конструктор
- **Services** — бізнес-логіка в окремих класах

---

## База даних

- **СУБД:** PostgreSQL
- **ORM:** Entity Framework Core
- **Міграції:** EF Core Migrations
