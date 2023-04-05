# Perna-Beer-API
## Introduction
This repository contains the source code of the API publicly available at https://perna-beer-api.azurewebsites.net/.

The purpose of this API is to perform CRUD (Create, Read, Update, Delete) operations on a single table (Brews) hosted on Azure SQL Server.

The Brews table contains information on different types of beer, with an example of its first 5 rows shown below:

|Id|Name|Style|ABV|IBU|Description|
|:--------|:--------|:--------|:--------|:--------|:--------|
|1|WitBier|Belgian Witbier|5.00|20|A refreshing wheat beer with citrus and coriander notes.|
|2|Weissbier|German Wheat Beer|5.20|12|A light, refreshing wheat beer|
|3|IPA|India Pale Ale|6.50|70|A hoppy beer with a bitter finish|
|4|Hefeweizen|German Hefeweizen|5.50|15|A traditional German wheat beer with fruity and spicy notes.|
|5|Stout|Irish Dry Stout|4.00|40|A dark and roasty beer with a creamy texture and hints of coffee and chocolate.|

The Swagger documentation can be accessed at https://perna-beer-api.azurewebsites.net/swagger/index.html.

## Getting Started
To set up the local environment, it is necessary to export the environment variable DB_CONNECTION_STRING with the instructions for connecting to the database. It is also necessary to export some value to the environment variable TOKEN, which will be used for JWT authentication of the API user.

## JWT Authentication
To access most of the API endpoints, it's necessary to obtain a JWT token for authentication. This can be done through the following request:
```
POST https://perna-beer-api.azurewebsites.net/api/Auth/login
{
  "username": "Ploo",
  "password": "password"
}
```
You can access the API via the Swagger UI at https://perna-beer-api.azurewebsites.net/swagger/index.html. Note that there is an "Authorize" field in the UI where you can enter the JWT token obtained from the previous request. Swagger will then automatically fill in the 'Authorize' field with the correct parameter for subsequent requests.

## API Documentation
The API contains the following endpoints:

`POST https://perna-beer-api.azurewebsites.net/api/Auth/register`

Creates a new user. A JSON object with the following format is required in the request body:
```
{
  "username": "string",
  "password": "string"
}
```

`POST https://perna-beer-api.azurewebsites.net/api/Auth/login`

Creates a JWT token for accessing restricted endpoints. A JSON object with the following format is required in the request body:
```
{
  "username": "string",
  "password": "string"
}
```

`GET https://perna-beer-api.azurewebsites.net/api/Brews/`

Returns a JSON with all items from the 'Brews' table.

This endpoint can also receive the following filter parameters:

- abvMin (decimal): filters the results to only include brews with an ABV (alcohol by volume) greater than or equal to this value.
- abvMax (decimal): filters the results to only include brews with an ABV (alcohol by volume) less than or equal to this value.
- ibuMin (decimal): filters the results to only include brews with an IBU (international bitterness unit) greater than or equal to this value.
- ibuMax (decimal): filters the results to only include brews with an IBU (international bitterness unit) less than or equal to this value.

Example usage:
`GET https://perna-beer-api.azurewebsites.net/api/Brews/?abvMin=4.5&abvMax=5.5&ibuMin=20&ibuMax=40`

This will return all brews with an ABV between 4.5 and 5.5 and an IBU between 20 and 40.

`GET https://perna-beer-api.azurewebsites.net/api/Brews/{id}`

Returns a JSON representation of the item from the 'Brews' table that matches the specified id.

`PUT https://perna-beer-api.azurewebsites.net/api/Brews/{id}`

Updates the item from the 'Brews' table that matches the specified {id}. The updates must be passed in the request body in JSON format, as shown in the example below.

Note that the 'id' of the object in the request body and the request parameter must be the same.
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

Deletes the item from the 'Brews' table that matches the specified {id}.

`POST https://perna-beer-api.azurewebsites.net/api/Brews/Create`

Inserts a new item into the database. The new item must be passed in the request body in JSON format, as shown in the example below.
Note that the Id is not generated automatically and must be informed in the request.

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
Note that when passing an item with an existing id in the database, the API will return error 400 and inform the existing id.

`POST https://perna-beer-api.azurewebsites.net/api/Brews/CreateMultiple`

Inserts a series of new items into the database. The new items must be passed in the request body in JSON format, as shown in the example below.
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
Note that when passing an item with an existing id in the database, the API will return status 400 and inform the existing ids.
When passing duplicate ids in the request, the API will return error 400 and inform the duplicate ids in the request.
