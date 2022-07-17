## Command Line Snippets Minimal API

### With the introduction of countless backend and deployment concepts during my first internship, I aim to practice and broaden my understanding of them.
#### Such concepts and technologies include, but are not limited to:
- .NET and C# in General
- MS SQL Server Management Studio
- Dependency Injection
- Object-Relational Mapping (with Auto Mapper)
- Entity Framework (utilize Code-First approach for data migration)
- Docker Build & Docker Compose
- Azure (SQL Server + Container Instance)
- Testing endpoints with Swagger

### Overall architecture of a minimal API compared to an MVC one
![image](https://user-images.githubusercontent.com/77269201/179403713-63947533-a258-4dba-b538-65cd1b41a4aa.png)
> The main difference is integrating controllers with Program.cs — essentially a barebone version of MVC

### Features missing from a minimal API compared to a an MVC one:
 - Model Validation (the ability to evaluate whether controllers are doing their jobs properly)
 - JSONPatch (the ability to partially update a model, instead of entirely replacing it)
 - Filters (the ability to inject extra logic at different stages, such as user authorization)

### Testing endpoints with Swagger UI
> - To be added

### Azure Dashboard
> - To be added


References:
> - https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api
> - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis
