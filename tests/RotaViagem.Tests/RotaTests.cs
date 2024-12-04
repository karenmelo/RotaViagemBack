using RotaViagem.Domain.Entities;

namespace RotaViagem.Tests;

public class RotaTests
{
    [Fact]
    [Trait("Rota", "Rota de Viagem Valida")]
    public void Rota_NovaRota_DeveSerValida()
    {
        //arrange
        var rota = new Rota(              
                "GRU",
                "BRC",
                10);


        //act
        var result = rota.IsValid();

        //assert
        Assert.True(result);
        Assert.Equal(0, rota.ValidationResult.Errors.Count);
    }

    [Fact]
    [Trait("Rota", "Rota de Viagem Invalida")]
    public void Rota_NovaRota_DeveSerInvalida()
    {
        //arrange
        var rota = new Rota(
                "",
                "",
                0);


        //act
        var result = rota.IsValid();

        //assert
        Assert.False(result);
        Assert.NotEqual(0, rota.ValidationResult.Errors.Count);
    }
}
