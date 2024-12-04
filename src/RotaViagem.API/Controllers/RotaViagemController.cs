using Microsoft.AspNetCore.Mvc;
using RotaViagem.Application.DTOs;
using RotaViagem.Application.Services.Interfaces;

namespace RotaViagem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RotaViagemController :  ControllerBase
{
    private readonly IRotaService _service;
   
    public RotaViagemController(IRotaService service)
    {
        _service = service;          
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]   
    public async Task<IActionResult> GetRotas()
    {
        try
        {
            var result = await _service.GetRotas();

            return Ok(result);
        }
        catch (Exception ex)
        {

            return BadRequest(ex);
            throw;
        }
    }


    [HttpPost]
    [Route("criar-rota")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRota([FromBody] RotaDto rota)
    {
        try
        {
            await _service.CreateAsync(rota);
            return Created();
        }
        catch (Exception ex)
        {

            return BadRequest(ex);
            throw;
        }
    }

    [HttpGet]
    [Route("pesquisar-rota/{origem}/{destino}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRotaByOriginDestiny(string origem, string destino)
    {
        try
        {
            var result = await _service.GetMelhorRota(origem, destino);
            
            if(result.Rota is null)
                return NotFound("Rota não encontrada!");

            return Ok(new {
                result.Rota,
                result.valor
            });
        }
        catch (Exception ex)
        {

            return BadRequest(ex);
            throw;
        }
    }

    [HttpPut]
    [Route("atualizar-rota/{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRota([FromBody] RotaDto rota, int id)
    {
        try
        {
            if (rota.Id != id)
            {
                return BadRequest("A rota não pode ser alterada.");                
            }

            var result = await _service.UpdateAsync(rota);

            if(result is null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {

            return BadRequest(ex);
            throw;
        }
    }

    [HttpDelete]
    [Route("deletar-rota/{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveRota(int id)
    {
        try
        {
            await _service.RemoveAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {

            return BadRequest(ex);
            throw;
        }
    }
}
