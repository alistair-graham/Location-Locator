# Location-Locator

## Tech Stack
- ASP.NET Core 7
- C# 11
- RESTful Web API
- Xunit testing framework
- HttpClient
- Swagger

## Overview
An API that extracts the IP address from a request a returns the location (country, region, city) of the requester.

## How to run
- Clone the project and download it locally
- Run the solution by either command:

&emsp;`dotnet run --launch-profile LocationLocator-Development`

&emsp;`dotnet run --launch-profile LocationLocator-Production`

Or run it from an IDE. If you run it from an IDE using the development launch profile, it automatically opens the Swagger API specification in your browser. You can manually test the API via Swagger or from a request creator like Postman.

You will not get very far when using this API locally however. This is because the request will be using the localhost/127.0.0.1 IP address which is one of the reserved. The requester's IP address must be public to have its location returned; not reserved or private.

## What it does

The GET action `localhost:5071/api/location` returns your location. Here are the following responses given the request:
- Request sent using a public IP address returns a 200 with a JSON object that contains your country, region and city name.
- Request sent using a public IP address but forwarded through proxies will behave the same way.
- Request sent using a reserved IP address returns a 400 with a message detailing the problem.
- Request sent using a private IP address returns a 400 with a message detailing the problem.
- Any unexpected and unhandled server exceptions will result in a 500 response with no sensitive information such as stacktrace returned.
