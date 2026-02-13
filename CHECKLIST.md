# Чеклист розвитку AI Planner

> Цей файл містить плани, кроки та прогрес проєкту. Оновлюй його по мірі виконання.

---

## Фази розробки

### Фаза 1 — Ядро

- [x] **1.1** Створити solution structure (Planner.sln, проекти)
- [x] **1.2** Planner.Core — сутність PlannerTask та інтерфейс IPlannerTaskRepository
- [x] **1.3** Planner.Infrastructure — EF Core, PostgreSQL, DbContext, репозиторії
- [x] **1.4** Planner.Services — сервіси для задач
- [x] **1.5** Planner.API — REST контролери, Swagger
- [x] **1.6** Docker Compose для PostgreSQL
- [x] **1.7** EF міграції, підключення до БД
- [x] **1.8** Planner.API.Tests — базові юніт-тести

### Фаза 2 — Авторизація

- [x] **2.1** ASP.NET Core Identity
- [x] **2.2** JWT автентифікація
- [x] **2.3** Google OAuth

### Фаза 2.4 — Покращення API

- [x] **2.4.1** UserId — прив’язка задач до користувача, ICurrentUserService, фільтрація в репозиторії
- [x] **2.4.2** User Secrets — винесення JWT Key та Google ClientSecret з appsettings
- [x] **2.4.3** DTO — PlannerTaskDto, CreatePlannerTaskRequest, UpdatePlannerTaskRequest замість entity
- [x] **2.4.4** Валідація — DataAnnotations або FluentValidation для Request-моделей
- [x] **2.4.5** IJwtService — винесення генерації JWT з AuthController в окремий сервіс
- [x] **2.4.6** Extension methods — AddPlannerDbContext, AddPlannerAuthentication тощо для Program.cs
- [x] **2.4.7** Exception handler — глобальна обробка помилок, ProblemDetails
- [x] **2.4.8** Health checks — `/health` для PostgreSQL
- [x] **2.4.9** Логування — ILogger у сервісах та контролерах, структуровані повідомлення
- [x] **2.4.10** Маршрути — явні `[Route("api/...")]`, lowercase URLs
- [x] **2.4.11** CORS — налаштування для Blazor-клієнта
- [ ] **2.4.12** Unit of Work — опційно, для складних транзакцій

### Фаза 3 — Web UI

- [ ] **3.1** Planner.Web — Blazor WebAssembly
- [ ] **3.2** Інтеграція з API, основний функціонал

### Фаза 4 — AI

- [ ] **4.1** Інтеграція OpenAI API
- [ ] **4.2** AI рекомендації для планів

### Фаза 5 — Нагадування

- [ ] **5.1** Hangfire або RabbitMQ
- [ ] **5.2** Сервіс нагадувань

### Фаза 6 — Desktop

- [ ] **6.1** Planner.Desktop — .NET MAUI для macOS

### Фаза 7 — Деплой

- [ ] **7.1** Docker образ API
- [ ] **7.2** Azure деплой

---

## Поточний крок

**Що робити зараз:** Фаза 2.4.12 — Unit of Work (опційно) або Фаза 3 — Web UI.

---

## Що вже зроблено

- [x] Ініціалізація репозиторію (GitHub)
- [x] `.gitignore` для .NET
- [x] Очищення структури проєкту
- [x] Solution structure (Planner.sln, 5 проєктів у src/ та tests/)
- [x] Planner.Core — PlannerTask, IPlannerTaskRepository
- [x] Planner.Infrastructure — ApplicationDbContext, PlannerTaskRepository
- [x] Planner.Services — IPlannerTaskService, PlannerTaskService
- [x] Planner.API — PlannerTasksController, DI, Swagger, REST ендпоінти
- [x] Docker Compose — PostgreSQL 16 для локальної розробки
- [x] EF міграції — InitialCreate, таблиця PlannerTasks
- [x] Planner.API.Tests — PlannerTaskService unit tests (xUnit, Moq)
- [x] ASP.NET Core Identity — ApplicationUser, таблиці AspNet*
- [x] JWT автентифікація — AuthController, [Authorize], Swagger Bearer
- [x] Google OAuth — вхід через Google, JWT після callback
- [x] User Secrets — Jwt:Key, Google ClientId/ClientSecret винесено в user-secrets
- [x] DTO — PlannerTaskDto, CreatePlannerTaskRequest, UpdatePlannerTaskRequest
- [x] Валідація — FluentValidation для Request-моделей
- [x] IJwtService — винесення генерації JWT в окремий сервіс
- [x] Extension methods — AddPlannerDbContext, AddPlannerAuthentication тощо
- [x] Exception handler — глобальна обробка помилок, ProblemDetails
- [x] Health checks — `/health` для PostgreSQL
- [x] Логування — ILogger у PlannerTaskService, AuthController
- [x] Маршрути — api/planner-tasks, api/auth, lowercase URLs
- [x] CORS — налаштування для Blazor-клієнта
- [x] UserId — ICurrentUserService, прив’язка задач до користувача, фільтрація в репозиторії

---

## Нотатки

_(Додавай сюди власні помітки, питання, ідеї)_
