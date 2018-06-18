using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace AspNetCore.ItegrationTests
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder().UseStartup<AspNetCoreTodo.Startup>()
            .ConfigureAppConfiguration((context, config) => {
                config.SetBasePath(Path.Combine(
                    Directory.GetCurrentDirectory(),
                    @"C:\Dev\AspNetCoreTodo"
                ));
            });

            _server = new TestServer(builder); //inicializa un servidor de pruebas en base al directorio del proyecto

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5000");
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}