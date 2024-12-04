using RotaViagem.Domain.Entities;

namespace RotaViagem.Domain.Interfaces.Repositories;

public interface IRotaRepository{

    Task<Rota> GetById(int id);
    Task<IEnumerable<Rota>> GetByOrginDestiny(string origem, string destino);
    Task<IEnumerable<Rota>> GetRotas();
    Task<Rota> CreateAsync(Rota rota);
    Task<Rota> UpdateAsync(Rota rota);
    Task<Rota> RemoveAsync(Rota rota);
}