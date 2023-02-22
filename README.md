# Location Locator

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

You will not get very far when using this API locally however. This is because the request will be using the `localhost`/`127.0.0.1` IP address which is a `reserved` address. The requester's IP address must be `public` to have its location returned; not `reserved` or `private`.

## Specification
The GET action `localhost:5071/api/location` returns your location. Here are the following responses given the request:
- Request sent using a `public` IP address returns a 200 with a JSON object that contains your country, region and city name.
- Request sent using a `public` IP address but forwarded through proxies will behave the same way.
- Request sent using a `reserved` IP address returns a 400 with a message detailing the problem.
- Request sent using a `private` IP address returns a 400 with a message detailing the problem.

## What else it has
- Client-side caching. It makes sense to use client-side caching here because only that particular client will be making a request with that request. The duration of the cache is set to 12 hours because 1-2% of IP addresses change locations every day ([source](https://ipinfo.io/blog/how-many-ips-change-geolocation-over-a-year/)). I settled on 12-hour cache duration as these location changes are usually slight and I assume 12-hour old data will not significantly impact the clients.
- Testing coverage of the API and the IP GeoLocator service integration. To test the API, I used middleware to fake the request's IP address.
- An unhandled exception will return a 500 response with no sensitive details such as stacktrace returned to the client.
- Logs errors & warnings where appropriate.

## What I have not done
- Authentication:  Checking who is sending the request.
- Authorisation: Checking what actions a requester is allowed to perform.
- Rate limiting, server-side caching, CORS, status page, timing metrics + alerts, public API documentation, handling headers such as `Accept` etc.
- Persistent storage: I didn't have enough time for that unfortunately! I had a time limit due to an offer I must respond to on Friday, two other coding tests and only having the evening to work on the coding tests. In terms of persistent storage, I might look at using Entity Framework with SQLite or PostgreSQL. I would add a DBContext service which would handle the database operations. I would handle the opening and closing of a database connection with a `using` statement. I would create an index on the IP_Address column for a fast lookup (assuming that is how it will be used). Potentially a NoSQL database would make more sense here; it depends on how system will be used in the future.
