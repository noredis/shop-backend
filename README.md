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
 - failure status code: 401
 - deadline: 28.02.2025
 - completed: 02.03.2025
## Реализовать возможность добавления товаров (пока для всех зарегистрированых пользователей)
 - method: POST /api/v1/product
 - input: application/json
   {
     "name": string,
     "category": string,
     "image": blob,
     "description": string,
     "price": double
   }
 - constraints:
   - price > 0
   - image - может быть null
   - category - пока просто строка
   - name - не пустой
   - description - может быть пустым
 - output status code: 201
 - failure status code: 400
 - output headers: [Location]
 - deadline: 11.03.2025
 - completed: 11.03.2025
## Допилить CRUD для товаров
 - PUT /api/v1/products/{id}
 - GET /api/v1/products (список вообще всех товаров)
 - GET /api/v1/products/{id}
 - DELETE /api/v1/products/{id}
 - PATCH /api/v1/products/{id}
 - GET возвращает 200, остальные 204 и пустое тело
 - валидация для редактирования, такая же, что для создания
 - deadline: 17.03.2025
 - completed: 16.03.2025
 - редактировать и удалять можно только те товары, что добавил авторизованный пользователь
## Собрать приложение в докер
 - deadline: 22.03.2025
 - completed: 24.03.2025
## Товар теперь может содержать несколько изображений
 - deadline: 31.03.2025
 - completed: 31.03.2025
## Разделить регистрацию и авторизацию продавца и клиента, продавец может добавлять товары, клиент только просматривать
- deadline: 27.04.2025
