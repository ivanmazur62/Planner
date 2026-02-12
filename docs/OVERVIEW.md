# AI Planner — Огляд проєкту

## Що це

Pet-проект планувальника часу з AI-рекомендаціями. Використовується для навчання .NET і для портфоліо.

**Платформи:** Web + macOS Desktop.

---

## Функціонал (запланований)

| Функція | Опис | Фаза |
|---------|------|------|
| Реєстрація та авторизація | Зокрема Google Authentication | 2 |
| CRUD планів і задач | Додавання, редагування, видалення | 1 |
| Нагадування | Про плани та задачі | 5 |
| AI-рекомендації | Оптимізація планів, поради щодо розподілу часу (OpenAI) | 4 |

---

## Стек технологій

### Обовʼязкові

- ASP.NET Core Web API
- Entity Framework Core + PostgreSQL
- REST API + Swagger
- SOLID, патерни проєктування
- Юніт-тести (xUnit / NUnit)
- Docker
- Azure (деплой)
- RabbitMQ або Hangfire (нагадування)
- gRPC (опційно)

### Фронтенд

- Web: Blazor WebAssembly
- Desktop macOS: .NET MAUI або Avalonia

### AI

- OpenAI API

---

## Доменні сутності

### Plan (План)

- Id, Title, Description
- CreatedAt, DueDate
- Звʼязок з задачи

### PlanTask (Задача)

- Id, PlanId, Title, Description
- IsCompleted
- CreatedAt, DueDate

---

## Мова комунікації

Українська.
