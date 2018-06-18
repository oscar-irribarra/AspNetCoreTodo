using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.ItegrationTests;
using Xunit;

namespace AspNetCoreTodo.IntegrationTests
{
    public class TodoRouteShould : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public TodoRouteShould(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task ChallengeAnonymousUser()
        {
            //Arrange
            var peticion = new HttpRequestMessage(
                HttpMethod.Get, "/todo"); //el controlador todo cuenta con la etiqueta Authorize

            //Act -> prueba si el cliente no esta logeado lo <direcciona> a la <vista login>
            var respuesta = await _client.SendAsync(peticion);

            //Assert 
            Assert.Equal(HttpStatusCode.Redirect, respuesta.StatusCode); 

            Assert.Equal("http://localhost:5000/Account/Login?ReturnUrl=%2Ftodo", 
                          respuesta.Headers.Location.ToString());
        }
    }
}