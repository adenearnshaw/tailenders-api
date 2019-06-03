# tailenders-api

Web Api to support the Tailenders inspired MidWicket Dating App.  

The project uses Asp .Net Core 2.0 with EF Core as an ORM. A .Net Standard Client library is also included to allow easy consumption of the endpoints.  

The api takes advantage of Azure B2C authentication to ensure that only requests with a valid token can access personal data.

## Database

### Creating Migrations

Location: TailendersApi.Repository  

* `dotnet ef migrations add <<MigrationName>>`
* `dotnet ef database update`

ref: [https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
