using AutoMapper;
using RotaViagem.Application.DTOs;
using RotaViagem.Application.Services.Interfaces;
using RotaViagem.Domain.Entities;
using RotaViagem.Domain.Interfaces.Repositories;

namespace RotaViagem.Application.Services
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        private readonly IMapper _mapper;
        public RotaService(IRotaRepository rotaRepository, IMapper mapper)
        {
            _rotaRepository = rotaRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<RotaDto>> GetRotas()
        {
            var result = await _rotaRepository.GetRotas();
            return _mapper.Map<IEnumerable<RotaDto>>(result);
        }

        public async Task CreateAsync(RotaDto rota)
        {
            await _rotaRepository.CreateAsync(_mapper.Map<Rota>(rota));
        }

        public async Task<RotaDto> GetById(int id)
        {
            var result = await _rotaRepository.GetById(id);
            return _mapper.Map<RotaDto>(result);
        }

        public async Task<IEnumerable<RotaDto>> GetByOrginDestiny(string origem, string destino)
        {
            var result = await _rotaRepository.GetByOrginDestiny(origem, destino);
            return _mapper.Map<IEnumerable<RotaDto>>(result);
        }

        public async Task RemoveAsync(int id)
        {
            var rota = await _rotaRepository.GetById(id);
            await _rotaRepository.RemoveAsync(_mapper.Map<Rota>(rota));
        }

        public async Task<RotaDto> UpdateAsync(RotaDto rota)
        {
            var result = await _rotaRepository.UpdateAsync(_mapper.Map<Rota>(rota));
            return _mapper.Map<RotaDto>(result);
        }

        public async Task<(List<string> Rota, double valor)> GetMelhorRota(string origem, string destino)
        {

            var rotas = await _rotaRepository.GetRotas();
            var grafo = Grafo(rotas.ToList());
            var result = CalcularMelhorRota(origem, destino, grafo);


            return result;
        }

        private static Dictionary<string, List<(string destino, double valor)>> Grafo(List<Rota> rotas)
        {

            var grafo = new Dictionary<string, List<(string destino, double valor)>>();
            foreach (var rota in rotas)
            {
                if (!grafo.ContainsKey(rota.Origem))
                    grafo[rota.Origem] = new List<(string destino, double valor)>();

                grafo[rota.Origem].Add((rota.Destino, rota.Valor));

                if (!grafo.ContainsKey(rota.Destino))
                    grafo[rota.Destino] = new List<(string destino, double valor)>();

            }

            return grafo;
        }

        private static (List<string> Rota, double valor) CalcularMelhorRota(string origem, string destino, Dictionary<string, List<(string destino, double valor)>> grafo)
        {

            var custos = new Dictionary<string, double>();
            var anteriores = new Dictionary<string, string>();
            var visitados = new HashSet<string>();
            var fila = new PriorityQueue<string, double>();

            foreach (var cidade in grafo.Keys)
                custos[cidade] = int.MaxValue;

            custos[origem] = 0;
            fila.Enqueue(origem, 0);

            while (fila.Count > 0)
            {
                var atual = fila.Dequeue();
                visitados.Add(atual);

                // Explorar os vizinhos
                if (grafo.ContainsKey(atual))
                {
                    foreach (var (vizinho, custo) in grafo[atual])
                    {
                        if (visitados.Contains(vizinho)) continue;

                        var novoCusto = custos[atual] + custo;
                        if (novoCusto < custos[vizinho])
                        {
                            custos[vizinho] = novoCusto;
                            anteriores[vizinho] = atual;
                            fila.Enqueue(vizinho, novoCusto);
                        }
                    }
                }
            }

            // Construir o caminho mais curto
            var caminho = new List<string>();
            var atualNo = destino;

            while (anteriores.ContainsKey(atualNo))
            {
                caminho.Add(atualNo);
                atualNo = anteriores[atualNo];
            }
            caminho.Add(origem);
            caminho.Reverse();

            return (caminho, custos[destino]);

        }

       
    }
}

