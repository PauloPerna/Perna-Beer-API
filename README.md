# Perna-Beer-API
## Introdução
Este repositório contém o código fonte da API disponibilizada publicamente em https://perna-beer-api.azurewebsites.net/. 
O objetivo desta API é realizar operações CRUD (Create, Read, Update, Delete) em uma única tabela (Brews) hospedada na Azure SQL Server. 
A tabela Brews contém informações de tipos diferentes de cerveja, um exemplo de suas primeiras 5 linhas são:

|Id|Name|Style|ABV|IBU|Description|
|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|
|1|WitBier|Belgian Witbier|5.00|20|A refreshing wheat beer with citrus and coriander notes.|
|2|Weissbier|German Wheat Beer|5.20|12|A light, refreshing wheat beer|
|3|IPA|India Pale Ale|6.50|70|A hoppy beer with a bitter finish|
|4|Hefeweizen|German Hefeweizen|5.50|15|A traditional German wheat beer with fruity and spicy notes.|
|5|Stout|Irish Dry Stout|4.00|40|A dark and roasty beer with a creamy texture and hints of coffee and chocolate.|

A documentação Swagger pode ser acessada através do link https://perna-beer-api.azurewebsites.net/swagger/index.html

## Getting Started
Para setar o ambiente local, é necessário exportar a variável ambiente `DB_CONNECTION_STRING` com as instruções para conexão ao banco de dados.
Além disso, também é necessário adicionar as dependências `Microsoft.EntityFrameworkCore` e `Microsoft.EntityFrameworkCore.SqlServer`

## Documentação da API
A API contém os seguintes EndPoints:

`GET https://perna-beer-api.azurewebsites.net/api/Brews/`

Retorna um JSON com todos os items da tabela 'Brews'.

`GET https://perna-beer-api.azurewebsites.net/api/Brews/{id}`

Retorna um JSON com o item da tabela 'Brews' com id = {id}.

`PUT https://perna-beer-api.azurewebsites.net/api/Brews/{id}`

Altera o item da tabela 'Brews' com id = {id}. É necessário passar as alterações no corpo da requisição em formato JSON como no exemplo abaixo.
Note que o 'id' do objeto no corpo da requisição e no parâmetro da requisição devem ser iguais.
```
{
  "id": 0,
  "name": "string",
  "style": "string",
  "abv": 0,
  "ibu": 0,
  "description": "string"
}
```

`DELETE https://perna-beer-api.azurewebsites.net/api/Brews/{id}`

Deleta a observação com id = {id}.

`POST https://perna-beer-api.azurewebsites.net/api/Brews/Create`

Insere no banco uma nova observação. É necessário passar a nova observação no corpo da requisição em formato JSON como no exemplo abaixo.
Repare que o Id não é gerado automaticamente e deve ser informado na requisição.

```
{
  "id": 0,
  "name": "string",
  "style": "string",
  "abv": 0,
  "ibu": 0,
  "description": "string"
}
```
Repare que, ao passar uma observação com id já existente no banco, a API retornará o erro 400 e informará o ID já existente. 

`POST https://perna-beer-api.azurewebsites.net/api/Brews/CreateMultiple`

Insere no banco uma série de novas observações. É necessário passar as novas observações no corpo da requisição em formato JSON como no exemplo abaixo.
```
[{
  "id": 0,
  "name": "string",
  "style": "string",
  "abv": 0,
  "ibu": 0,
  "description": "string"
},
{
  "id": 1,
  "name": "string",
  "style": "string",
  "abv": 0,
  "ibu": 0,
  "description": "string"
}]
```
Repare que, ao passar uma observação com id já existente no banco, a API retornará o erro 400 e informará os IDs já existentes. 
Ao passar IDs repetidos na requisição, a API retornará o erro 400 e informará os IDs repetidos na requisição.

Nenhuma autenticação é necessária para o funcionamento da API.
