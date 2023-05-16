# WebAPI
 Web api sample

 ## Prerequisites
- Docker
- .Net 7.0

## Structure source code
- Request > Controler > Service > Model(DB) > Response

## Migration
- Create a migration: `dotnet ef migrations add InitialCreate`
- Applying Migrations: `dotnet ef database update`

## Swagger
- HTML: `<domain>/swagger`
- YAML: `<domain>/swagger/v1/swagger.yaml`

## How to run projects
- Run Sql Server: `docker-compose up`
- Applying Migrations: `dotnet ef database update`