using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrientoonApi.Controllers;
using OrientoonApi.Data.Repositories.Implementations;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Implementations;
using OrientoonApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOrientoon.Helpers.Fixtures;

namespace TestOrientoon.UnitTest.Api
{
    public class ArtistaControllerTest : IClassFixture<DataBaseFixture>
    {
        private readonly DataBaseFixture _fixture;
        private readonly ArtistaController _controller;
        
        public ArtistaControllerTest(DataBaseFixture fixture)
        {
            _fixture = fixture;
            _controller = new ArtistaController(_fixture.ArtistaService);
        }

        [Fact]
        public async void GetArtista()
        {
            var artistaId = "1";
            var expectedArtista = new ArtistaForm { Id = artistaId, Nome = "Oda" };

            var result = await _controller.Get(artistaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ArtistaForm>(okResult.Value);
            Assert.Equal(expectedArtista.Id, returnValue.Id);
            Assert.Equal(expectedArtista.Nome, returnValue.Nome);

        }
    }
}
