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
You can access the API via the Swagger UI at https://perna-beer-api.azurewebsites.net/swagger/index.html. Note that there is an "Authorize" field in the UI where you can enter the JWT token obtained from the previous request. Swagger will then automatically fill in the 'Authorize' parameter with the correct value for subsequent requests.

## API Documentation

### Auth
#### POST `/api/Auth/register`

Creates a new user. 

##### Authentication

This endpoint requires a valid JWT token in the Authorization header. To obtain a token, see the /api/Auth/login endpoint.

##### Request Body

The request body must contain a JSON object with the following properties:
 - `username` (string, required): The username for the new user.
 - `password` (string, required): The password for the new user.

##### Response

If the user is created successfully, the response will have a status code of 201 (Created). The response body will be a JSON object containing the newly created user's information.

If the username already exists in the database, the response will have a status code of 400 (Bad Request) and the response body will contain an error message indicating that the username is already taken.

##### Example Request

```
POST /api/Auth/register HTTP/1.1
Host: https://perna-beer-api.azurewebsites.net
Content-Type: application/json
Authorization: Bearer <jwt_token>

{
  "username": "John Doe",
  "password": "SecretPassword123"
}
```

##### Example Response

```
HTTP/1.1 201 Created
Content-Type: application/json

{
  "username": "John Doe"
}
```

#### POST `/api/Auth/login`

Creates a JWT token for accessing restricted endpoints.

##### Request Body

The request body must contain a JSON object with the following properties:
 - `username` (string, required): The username.
 - `password` (string, required): The password.

##### Response

If the username and password are correct, the response will have a status code of 200 (OK) and the response body will contain a JWT token that can be used to access restricted endpoints.

##### Example Request

```
POST /api/Auth/login HTTP/1.1
Host: https://perna-beer-api.azurewebsites.net
Content-Type: application/json

{
  "username": "John Doe",
  "password": "SecretPassword123"
}
```

##### Example Response

```
HTTP/1.1 200 OK
Content-Type: application/json

"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGxvbyIsImV4cCI6MTY4MDgxNDUwMX0.uoT3NJ0yuv9235kRWRDiGU3kijzBpiFxuC7N25Glmdu1bRuCBoWki3f9ocLNftZrBwpv27LL14MxAjpKknZmdQ"
```

### Brews
#### GET `/api/Brews/`

Returns a JSON with all items from the 'Brews' table.

##### Parameters 

- abvMin (decimal): filters the results to only include brews with an ABV (alcohol by volume) greater than or equal to this value.
- abvMax (decimal): filters the results to only include brews with an ABV (alcohol by volume) less than or equal to this value.
- ibuMin (decimal): filters the results to only include brews with an IBU (international bitterness unit) greater than or equal to this value.
- ibuMax (decimal): filters the results to only include brews with an IBU (international bitterness unit) less than or equal to this value.

##### Response

The response is a JSON array of objects, each representing a brew in the 'Brews' table. The object has the following properties:

 - `id` (integer): The unique identifier of the brew.
 - `name` (string): The name of the brew.
 - `style` (string): The style of the brew.
 - `abv` (decimal): The ABV (alcohol by volume) of the brew.
 - `ibu` (decimal): The IBU (international bitterness unit) of the brew.
 - `description` (string): A description of the brew.

##### Example Request

```
GET /api/Brews?abvMin=4.5&abvMax=5.5&ibuMin=20&ibuMax=40 HTTP/1.1
Host: https://perna-beer-api.azurewebsites.net
```

##### Example Response

```
HTTP/1.1 200 OK
Content-Type: application/json

[{"id":1,"name":"WitBier","style":"Belgian Witbier","abv":5.00,"ibu":20,"description":"A refreshing wheat beer with citrus and coriander notes"},{"id":7,"name":"Pilsner","style":"Czech Pilsner","abv":4.50,"ibu":35,"description":"A light and crisp lager with a floral and herbal hop profile."},{"id":8,"name":"Brown Ale","style":"English Brown Ale","abv":5.00,"ibu":25,"description":"A malt-forward beer with nutty and caramel flavors and a subtle hop bitterness."}]
```

#### GET `/api/Brews/{id}`

Returns a JSON representation of the item from the 'Brews' table that matches the specified id.

##### Parameters 

- id (integer): The unique identifier of the brew to retrieve.

##### Response

The response is a JSON array of objects, each representing a brew in the 'Brews' table. The object has the following properties:

 - `id` (integer): The unique identifier of the brew.
 - `name` (string): The name of the brew.
 - `style` (string): The style of the brew.
 - `abv` (decimal): The ABV (alcohol by volume) of the brew.
 - `ibu` (decimal): The IBU (international bitterness unit) of the brew.
 - `description` (string): A description of the brew.

##### Example Request

```
GET /api/Brews/2 HTTP/1.1
Host: https://perna-beer-api.azurewebsites.net
```

##### Example Response

```
HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": 2,
  "name": "Weissbier",
  "style": "German Wheat Beer",
  "abv": 5.2,
  "ibu": 12,
  "description": "A light, refreshing wheat beer"
}
```

#### PUT `/api/Brews/{id}`

Updates the item from the 'Brews' table that matches the specified `id`. The updates must be passed in the request body in JSON format, as shown in the example below.

##### Authentication

This endpoint requires a valid JWT token in the Authorization header. To obtain a token, see the /api/Auth/login endpoint.

##### Parameters

- id (integer): The unique identifier of the brew to update.

##### Request Body

The request body must contain a JSON object with all the following properties:

 - `name` (string): The new name of the brew.
 - `style` (string): The new style of the brew.
 - `abv` (decimal): The new ABV (alcohol by volume) of the brew.
 - `ibu` (decimal): The new IBU (international bitterness unit) of the brew.
 - `description` (string): The new description of the brew.

##### Response

If the update is successful, the response will have a status code of 204 (No Content).

##### Example Request

```
PUT /api/Brews/2 HTTP/1.1
Host: https://perna-beer-api.azurewebsites.net
Content-Type: application/json
Authorization: Bearer <jwt_token>

{
  "name": "Weissbier",
  "style": "German Wheat Beer",
  "abv": 5.2,
  "ibu": 12,
  "description": "A light, refreshing wheat beer"
}
```

##### Example Response

```
HTTP/1.1 204 No Content
```

#### DELETE `/api/Brews/{id}`

Deletes the item from the 'Brews' table that matches the specified {id}.

##### Authentication

This endpoint requires a valid JWT token in the Authorization header. To obtain a token, see the /api/Auth/login endpoint.

##### Parameters

- id (integer): The unique identifier of the brew to deleted.

##### Response

If the brew is deleted successfully, the response will have a status code of 204 (No Content).

##### Example Request

```
DELETE /api/Brews/10 HTTP/1.1
Host: https://perna-beer-api.azurewebsites.net
Content-Type: application/json
Authorization: Bearer <jwt_token>
``` 

##### Example Response

```
HTTP/1.1 204 No Content
```

#### GET `/api/Brews/Search?q={search_string}`

Returns a list of Brew objects whose name, style or description contains the provided search string. The search is not case sensitive.

##### Parameters

 - `q` (required): A string representing the search term.

##### Response

The response returns a JSON object with the following properties:

 - `id` (integer): The unique identifier of the brew.
 - `name` (string): The name of the brew.
 - `style` (string): The style of the brew.
 - `abv` (number): The Alcohol By Volume (ABV) of the brew.
 - `ibu` (integer): The International Bitterness Units (IBU) of the brew.
 - `description` (string): The description of the brew.

##### Example Request

```
GET /api/Brews/Search?q=IPA HTTP/1.1
Host: https://perna-beer-api.azurewebsites.net
```

##### Example Respose

```
HTTP/1.1 200 OK
Content-Type: application/json
[{"id": 3,    "name": "IPA",    "style": "India Pale Ale",    "abv": 6.5,    "ibu": 70,    "description": "A hoppy beer with a bitter finish"  },  {   "id": 11,    "name": "Hoppy IPA",    "style": "IPA",    "abv": 7,    "ibu": 70,    "description": "A hop-forward IPA with a strong malt backbone."  }]
```

#### POST `/api/Brews/Create`

Inserts a new item into the database. The new item must be passed in the request body in JSON format, as shown in the example below.
The id will be autogenerated and if there's an id in the JSON object it will be ignored.

##### Authentication

This endpoint requires a valid JWT token in the Authorization header. To obtain a token, see the /api/Auth/login endpoint.

##### Request Body

The request body must contain a JSON object with all the following properties:

 - `name` (string): The name of the brew.
 - `style` (string): The style of the brew.
 - `abv` (decimal): The ABV (alcohol by volume) of the brew.
 - `ibu` (decimal): The IBU (international bitterness unit) of the brew.
 - `description` (string): The description of the brew.

##### Response

The response is a JSON object with the following properties:

 - `id` (integer): The identifier of the newly created brew
 - `name` (string): The name of the newly created brew.
 - `style` (string): The style of the newly created brew.
 - `abv` (decimal): The ABV (alcohol by volume) of the newly created brew.
 - `ibu` (decimal): The IBU (international bitterness unit) of the newly created brew.
 - `description` (string): The description of the newly created brew.

##### Example Request

```
POST /api/Brews/Create HTTP/1.1
Host: https://perna-beer-api.azurewebsites.net
Content-Type: application/json
Authorization: Bearer <jwt_token>

{
  "name": "Belgian Wit",
  "style": "Belgian Wheat Beer",
  "abv": 5.0,
  "ibu": 20,
  "description": "Belgian Wit is a refreshing beer brewed with wheat, orange peel, and coriander. It has a cloudy appearance and a light, crisp flavor with a hint of citrus."
}
```

##### Example Response

```
HTTP/1.1 201 Created
Content-Type: application/json

{
  "id": 1,
  "name": "Belgian Wit",
  "style": "Belgian Wheat Beer",
  "abv": 5.0,
  "ibu": 20,
  "description": "Belgian Wit is a refreshing beer brewed with wheat, orange peel, and coriander. It has a cloudy appearance and a light, crisp flavor with a hint of citrus."
}
```

#### POST `/api/Brews/CreateMultiple`

Inserts a series of new items into the database. The new items must be passed in the request body in JSON format, as shown in the example below.
The Id will be autogenerated and if there's an id in the JSON object it will be ignored.

##### Authentication

This endpoint requires a valid JWT token in the Authorization header. To obtain a token, see the /api/Auth/login endpoint.

##### Request Body

The request body must contain an JSON array of objects with all the following properties:

 - `name` (string): The name of the brew.
 - `style` (string): The style of the brew.
 - `abv` (decimal): The ABV (alcohol by volume) of the brew.
 - `ibu` (decimal): The IBU (international bitterness unit) of the brew.
 - `description` (string): The description of the brew.

##### Response

The response is a JSON array of objects, each representing a brew that was successfully created in the 'Brews' table. The object has the following properties:

 - `id` (integer): The identifier of the newly created brew
 - `name` (string): The name of the newly created brew.
 - `style` (string): The style of the newly created brew.
 - `abv` (decimal): The ABV (alcohol by volume) of the newly created brew.
 - `ibu` (decimal): The IBU (international bitterness unit) of the newly created brew.
 - `description` (string): The description of the newly created brew.
    
##### Example Request

```
POST /api/Brews/CreateMultiple HTTP/1.1
Host: https://perna-beer-api.azurewebsites.net
Content-Type: application/json
Authorization: Bearer <jwt_token>

[{
  "name": "Stout",
  "style": "Imperial Stout",
  "abv": 10.5,
  "ibu": 80,
  "description": "A dark, full-bodied, roasty, malty ale with a complementary oatmeal flavor."
},
{
  "name": "IPA",
  "style": "New England IPA",
  "abv": 7.2,
  "ibu": 55,
  "description": "A hoppy beer that is characterized by its juicy and hazy appearance, with low bitterness and a citrusy aroma."
}]
```

##### Example Response
```
HTTP/1.1 201 Created
Content-Type: application/json

[{"id": 15,"name": "Stout","style": "Imperial Stout","abv": 10.5,"ibu": 80,"description": "A dark, full-bodied, roasty, malty ale with a complementary oatmeal flavor."},{"id": 16,"name": "IPA","style": "New England IPA","abv": 7.2,"ibu": 55,"description": "A hoppy beer that is characterized by its juicy and hazy appearance, with low bitterness and a citrusy aroma."}]
```
