using System.Text.Json.Serialization;

namespace RotaViagem.Application.DTOs
{
    public class RotaDto
    {      
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public double Valor { get; set; }
    }
}
