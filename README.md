# WebFinancy

## Sobre 
Este é uma api rest em asp net para controle finançeiro, onde pode criar um usuario e realizar login, além de deletar e atualizar, e através do login usando jwt o usuario pode criar anotações de gastos, atualizar, deletar, e ainda ver os gastos totais, qual foi o maior gasto, menor gastos, dispesas e até receita. A api está documentada usando o Swagger, e para isso está em http://localhost:5176/swagger/index.html

### as tecnologias usadas foram:

-C#

-AspNet core(.net7)

-Entity Framework

-jwt

## EndPoints  (URL base: http://localhost:5176)

### -User
`(Criar user)`

`POST` api/user/criar

`(Fazer Login)`

`POST` api/user/login

`(Atualizar user)`

`PUT` api/user

`(Deletar user)`

`DELETE` api/user/{id}


### -Financy

`(Resgatar todas as financies)`

`GET` api/Financy 

`(Resgatar financy por id)`

`GET` api/Financy/{id}

`(Criar financy)`

`POST` api/Financy

`(Atualizar financy)`

`PUT` api/Financy

`(Deletar financy por id)`

`DELETE` api/Financy/{id}

`(Resgatar total de gastos)`

`GET` api/FinancyBalance/total

`(Resgatar financy com maior gasto)`

`GET` api/FinancyBalance/maior

`(Resgatar financy com menor gasto)`

`GET` api/FinancyBalance/menor

`(Resgatar dispesas)`

`GET` api/FinancyBalance/dispesas

`(Resgatar receitas)`

`GET` api/FinancyBalance/receitas

