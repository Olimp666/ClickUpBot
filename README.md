# ClickUpBot
Бот для создания задач в трекере ClickUp

https://t.me/backend_clickupbot
## Настройка
В `App.Config` необходимо заполнить следующие поля:
  - Token - токен телеграм бота.
  - ClientId - id приложения ClickUp.
  - ClientSecret - секретынй код приложения ClickUp.
  - RedirectURL - url, куда будет перенаправляться пользователь.
  - ConnectionString - connection string для SQLite. По умолчанию `Data Source=app.db;Pooling=True;`
