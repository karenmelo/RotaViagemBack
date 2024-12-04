using AutoMapper;
using Moq;
using RotaViagem.Application.DTOs;
using RotaViagem.Application.Services;
using RotaViagem.Domain.Entities;
using RotaViagem.Domain.Interfaces.Repositories;

namespace RotaViagem.Tests
{
    [Collection(nameof(RotaTestCollection))]
    public class RotaServiceTests
    {
        private readonly RotaTestsFixture _rotafixture;

        public RotaServiceTests(RotaTestsFixture rotafixture)
        {
            _rotafixture = rotafixture;
        }

        [Fact]
        public void RotaService_CriarRota_DeveExecutarComSucesso()
        {
            //arrange
            var rota = _rotafixture.GeraRotaValida();
            var rotaRepository = new Mock<IRotaRepository>();
            var mockMapper = new Mock<IMapper>();
            var rotaService = new RotaService(rotaRepository.Object, mockMapper.Object);            

            //act            
            rotaService.CreateAsync(mockMapper.Object.Map<RotaDto>(rota));


            //assert
            Assert.True(rota.IsValid());
            rotaRepository.Verify(r => r.CreateAsync(It.IsAny<Rota>()), Times.Once);
        }

        [Fact]
        public void RotaService_CriarRota_DeveExecutarComFalha()
        {
            //arrange
            var rota = _rotafixture.GeraRotaInvalida();
            var rotaRepository = new Mock<IRotaRepository>();
            var mapper = new Mock<IMapper>();
            var rotaService = new RotaService(rotaRepository.Object, mapper.Object);


            //act
            rotaService.CreateAsync(mapper.Object.Map<RotaDto>(rota));


            //assert
            Assert.False(rota.IsValid());
            rotaRepository.Verify(r => r.CreateAsync(rota), Times.Never);
        }
    }
}
