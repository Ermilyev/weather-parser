# Парсер погоды с gismeteo
 
Parser - система, которая предоставит информацию о погоде в выбранном городе на ближайшие 10 дней.

Система состоит из:

- База данных MySql (хранит информацию о погоде. Сервер по умолчанию = 'localhost', порт = '3306' пользователь = 'root', пароль = 'root' база данных = '4people')
- Парсер (граббер) сайта http://www.gismeteo.ru/ (консольное приложение которое забирает данные о погоде по всем городам, представленным на главной странице сайта в блоке «Популярные пункты России» за 10 дней и сохраняет их в базе данных);
- WebApi сервис (снабжает клиентов данными о погоде, прослойка между клиентским приложением и базой данных);
- Клиентское приложение WinForms (отображает сведения о погоде в выбранном городе на указанную дату; клиентское приложение взаимодействует только c WebApi сервисом)

## Развертывание

Системе требуется база данных Mysql на локальном хосте с пользователем = 'root' и паролем = 'root'

## Построен с

* [AutoMapper] - A convention-based object-object mapper.
* [FluentValidation] - A validation library for .NET that uses a fluent interface to construct strongly-typed validation rules.
* [HtmlAgillityPack] - This is an agile HTML parser
* [Microsoft.AspNetCore.Mvc] - ASP.NET Core MVC is a web framework
* [Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer] - ASP.NET Core MVC API explorer functionality for discovering metadata.
* [Microsoft.Extensions.Configuration] - Implementation of key-value pair based configuration. Includes the memory configuration provider.
* [Microsoft.Extensions.Hosting] - Hosting and startup infrastructures for applications.
* [Newtonsoft.Json] - Json.NET is a popular high-performance JSON framework for .NET
* [Pomelo.EntityFrameworkCore.MySql] - Pomelo's MySQL database provider for Entity Framework Core.
* [RestSharp] - Simple REST and HTTP API Clien
* [Serilog] - Simple .NET logging with fully-structured events
* [Swashbuckle.AspNetCore] - Swagger tools for documenting APIs built on ASP.NET Core
