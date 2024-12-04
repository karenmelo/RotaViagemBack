using RotaViagem.Domain.Validations;

namespace RotaViagem.Domain.Entities;

public class Rota : Entity
{
    //public int Id { get; set; }
    public string Origem { get; private set; }
    public string Destino { get; private set; }
    public double Valor { get; private set; }



    public Rota(string origem, string destino, double valor)
    {
        Origem = origem;
        Destino = destino;
        Valor = valor;
    }

    public bool IsValid()
    {
        ValidationResult = new RotaValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

