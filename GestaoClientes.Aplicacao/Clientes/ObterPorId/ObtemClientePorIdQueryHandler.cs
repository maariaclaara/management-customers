using GestaoClientes.Aplicacao.Abstracoes;
using GestaoClientes.Aplicacao.Resultados;

namespace GestaoClientes.Aplicacao.Clientes.ObterPorId;

public sealed class ObtemClientePorIdQueryHandler
{
    private readonly IClienteRepository _clienteRepository;

    public ObtemClientePorIdQueryHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Resultado<ClienteDto?>> HandleAsync(ObtemClientePorIdQuery query, CancellationToken cancellationToken)
    {
        if (query.Id == Guid.Empty)
            return Resultado<ClienteDto?>.Falha("DADOS_INVALIDOS", "O ID informado é inválido.");

        var cliente = await _clienteRepository.ObterPorIdAsync(query.Id, cancellationToken);

        if (cliente is null)
            return Resultado<ClienteDto?>.Ok(null);

        var dto = new ClienteDto(
            cliente.Id,
            cliente.NomeFantasia,
            cliente.Cnpj.Valor,
            cliente.Ativo
        );

        return Resultado<ClienteDto?>.Ok(dto);
    }
}
