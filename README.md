# SimpleWebApplication
Простое WebAPI приложение, поддерживающие получение(одного/нескольких), добавление и удаление пользователей.
Пользователь - тип данных следующего вида:
User
-> Login
-> Password
-> CreatedDate
-> UserGroup
---> Code (Admin/User)
---> Description
-> UserState
---> Code (Active/Blocked)
---> Description

## Технологический стек:
.Net C#, ASP.NET core, EF core, PostgreSQL, Swagger
