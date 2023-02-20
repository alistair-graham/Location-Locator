using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace LocationAPI.Tests.Tests.Integration.Helpers
{
    public class CustomRemoteIpStartupFilter : IStartupFilter
    {
        private readonly IPAddress? _ipAddress;

        public CustomRemoteIpStartupFilter(IPAddress? ipAddress)
        {
            _ipAddress = ipAddress;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<CustomRemoteIpAddressMiddleware>(_ipAddress);
                next(app);
            };
        }

        private class CustomRemoteIpAddressMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly IPAddress? _ipAddress;

            public CustomRemoteIpAddressMiddleware(RequestDelegate next, IPAddress? ipAddress)
            {
                _next = next;
                _ipAddress = ipAddress;
            }

            public async Task Invoke(HttpContext httpContext)
            {
                httpContext.Connection.RemoteIpAddress = _ipAddress;
                await _next(httpContext);
            }
        }
    }
}

