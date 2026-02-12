# Чеклист розвитку AI Planner

> Цей файл містить плани, кроки та прогрес проєкту. Оновлюй його по мірі виконання.

---

## Фази розробки

### Фаза 1 — Ядро

- [x] **1.1** Створити solution structure (Planner.sln, проекти)
- [ ] **1.2** Planner.Core — сутності Plan, PlanTask та інтерфейси репозиторіїв
- [ ] **1.3** Planner.Infrastructure — EF Core, PostgreSQL, DbContext, репозиторії
- [ ] **1.4** Planner.Services — сервіси для планів і задач
- [ ] **1.5** Planner.API — REST контролери, Swagger
- [ ] **1.6** Docker Compose для PostgreSQL
- [ ] **1.7** EF міграції, підключення до БД
- [ ] **1.8** Planner.API.Tests — базові юніт-тести

### Фаза 2 — Авторизація

- [ ] **2.1** ASP.NET Core Identity
- [ ] **2.2** JWT автентифікація
- [ ] **2.3** Google OAuth

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

**Що робити зараз:** Додати project references (якщо ще не зроблено) → Фаза 1.2 — Planner.Core.

---

## Що вже зроблено

- [x] Ініціалізація репозиторію (GitHub)
- [x] `.gitignore` для .NET
- [x] Очищення структури проєкту
- [x] Solution structure (Planner.sln, 5 проєктів у src/ та tests/)

---

## Нотатки

_(Додавай сюди власні помітки, питання, ідеї)_
