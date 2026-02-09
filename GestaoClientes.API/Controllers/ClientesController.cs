using GestaoClientes.Aplicacao.Clientes.Criar;
using GestaoClientes.Aplicacao.Clientes.ObterPorId;
using Microsoft.AspNetCore.Mvc;

namespace GestaoClientes.API.Controllers;

[ApiController]
[Route("clientes")]
public sealed class ClientesController : ControllerBase
{
    private readonly CriaClienteCommandHandler _criaClienteHandler;
    private readonly ObtemClientePorIdQueryHandler _obtemClienteHandler;

    public ClientesController(
        CriaClienteCommandHandler criaClienteHandler,
        ObtemClientePorIdQueryHandler obtemClienteHandler)
    {
        _criaClienteHandler = criaClienteHandler;
        _obtemClienteHandler = obtemClienteHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriaClienteCommand command, CancellationToken cancellationToken)
    {
        var resultado = await _criaClienteHandler.HandleAsync(command, cancellationToken);

        if (!resultado.Sucesso)
            return BadRequest(resultado.CodigoErro ?? resultado.MensagemErro);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = resultado.Valor!.Id },
            resultado.Valor
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var query = new ObtemClientePorIdQuery(id);

        var resultado = await _obtemClienteHandler.HandleAsync(query, cancellationToken);

        if (resultado.Valor is null)
            return NotFound();

        return Ok(resultado.Valor);
    }
}
