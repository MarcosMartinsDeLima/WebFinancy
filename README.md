# WebFinancy

## Sobre 
Este é uma api rest em asp net para controle finançeiro, onde o usuario pode criar anotações de gastos, atualizar, deletar, e ainda ver os gastos totais, e qual foi o maior gasto,Além disso ele pode e deve
criar uma conta para poder se autenticar, usando jwt tmb
as tecnologias usadas foram:

-C#

-AspNet core(.net7)

-Entity Framework

-jwt

## EndPoints  (URL base: http://localhost:5176)

### -User
`Criar user`

`POST` api/user/criar

`Fazer Login`

`POST` api/user/login


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

