# Weather parser from gismeteo application
 
Parser - a system that will provide information about the weather in the selected city for the next 10 days.

The system consists of:

- MySql database (stores weather information. Default server = 'localhost', port = '3306' user = 'root', password = 'root' database = 'forecast');
- Parser (grabber) of the site http://www.gismeteo.ru/ (a console application that takes weather data for all cities presented on the main page of the site in the block "Popular points of Russia" for 10 days and saves them in the database);
- WebApi service (provides clients with weather data, a layer between the client application and the database);
- WPF client application (displays information about the weather in the selected city on the specified date; the client application interacts only with the WebApi service)

## Deployment

The system needs a Mysql database on localhost with user='root' and password='root'

## Build with

* [AutoMapper] - A convention-based object-object mapper.
* [FluentValidation] - A validation library for .NET that uses a fluent interface to construct strongly-typed validation rules.
* [HtmlAgillityPack] - This is an agile HTML parser
* [Microsoft.AspNetCore.Mvc] - ASP.NET Core MVC is a web framework
* [Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer] - ASP.NET Core MVC API explorer functionality for discovering metadata.
* [Microsoft.EntityFrameworkCore] - Entity Framework Core is a modern object-database mapper for .NET.
* [Microsoft.Extensions.Configuration] - Implementation of key-value pair based configuration. Includes the memory configuration provider.
* [Microsoft.Extensions.Hosting] - Hosting and startup infrastructures for applications.
* [Newtonsoft.Json] - Json.NET is a popular high-performance JSON framework for .NET
* [Pomelo.EntityFrameworkCore.MySql] - Pomelo's MySQL database provider for Entity Framework Core.
* [RestSharp] - Simple REST and HTTP API Client
* [Serilog] - Simple .NET logging with fully-structured events
* [Swashbuckle.AspNetCore] - Swagger tools for documenting APIs built on ASP.NET Core
