# WebFinancy

## Sobre 
Este é uma api rest em asp net para controle finançeiro, onde o usuario pode criar anotações de gastos, atualizar, deletar, e ainda ver os gastos totais, e qual foi o maior gasto
as tecnologias usadas foram:

-C#

-AspNet core(.net7)

-Entity Framework

## Rotas  (URL base: http://localhost:5176)
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
