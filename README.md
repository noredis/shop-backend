# shop-backend

## Регистрация пользователей
- method: POST /api/v1/register
- input: application/json
  {
    "full_name": string,
    "email": string,
    "password": string,
    "password_confirmation": string
  }
- constraints:
   - full_name - не пустой
   - email - валидная электронная почта
   - password - минимум 6 символов, обязательно должен быть как минимум один символ в нижнем регистре, один символ в верхнем регистре, одна цифра, один спец. символ, не должно быть отсупов (пробелов, табов, переносов строки)
   - password_confirmation - должен быть такой же какой и password
- store: postgres, хранить обязательно id, full_name, email и хэшированный password, можно добавить created_at, updated_at
- output: application/json
  {
    "id": number/string,
    "full_name": string,
    "email": string,
    ...other_fields
  }
- output status code: 201
- output headers:
  - Content-Type: application/json
  - Location: /api/v1/user/{id}
- deadline: 22.02.2025
- completed: 19.02.2025

## Выделить бизнес-логику в сервисы, инвертировать зависимости при помощи репозитория
 - deadline: 25.02.2025
 - completed: 23.02.2025

## Реализовать авторизацию при помощи JWT, сделать методы GET /users и GET /user/{id} доступными только авторизованному пользователю
 - method: POST /api/v1/login
 - input: application/json
   {
     "email": string,
     "password": string
   }
 - output
   {
     "access_token": string,
     "refresh_token": string
   }
 - constraints:
   - access_token - действует 24 часа, после чего пользователь должен отправить запрос POST /api/v1/refresh с refresh-токеном, и получить новые токены
   - refresh_token - нет срока годности
 - output status code: 200
 - deadline: 28.02.2025
