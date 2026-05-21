# Task Management System - .NET 9


## Technologies
- ASP.NET Core Web API (.NET 9)
- Entity Framework Core
- SQL Server
- JWT Authentication
- Clean Architecture implemented in this Assessment
- Used CQRS 
- make a code to make migration in database while the project run and make roles ...
- Swagger

## Features
- Register
- Login
- Projects CRUD
- Tasks CRUD
- JWT Authentication
- Global Exception Handling
- Generic Response Wrapper

## Run Commands

## the files of Migrations is exists inside the project in the TaskManagementSystem.Infrastructure layer 



## you do not need to make migration manually 
- because code exists in program.cs make migration while the project running ...

### Migration
1- Add-Migration InitialCreate

### Update Database
2- Update-Database

#  and You can run the 1 and 2 previous instruction on the TaskManagementSystem.Infrastructure layer 
the database will update in your device


### Run
dotnet run >> run the project


# after run I have to you Important notes 
1- you can make register firstly
2- you can make write the user name without spaces its must ..
3- after register you can go to authorized lock as a button at the right upper in the swagger 
4- you can click on it then add the token that return after the register or login proccess the make close for popup ..
5- finally you can access and use any endpoint you want ....