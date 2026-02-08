using System.Collections.Concurrent;
using GestaoClientes.Aplicacao.Abstracoes;
using GestaoClientes.Dominio.Clientes;

namespace GestaoClientes.Infraestrutura.Repositorios;

public sealed class ClienteRepositoryEmMemoria : IClienteRepository
{
    // Armazenamento estático conforme orientação do desafio.
    // ConcurrentDictionary ajuda a evitar condições de corrida simples.
    private static readonly ConcurrentDictionary<Guid, Cliente> _clientesPorId = new();

    public Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _clientesPorId[cliente.Id] = cliente;
        return Task.CompletedTask;
    }

    public Task<Cliente?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _clientesPorId.TryGetValue(id, out var cliente);
        return Task.FromResult(cliente);
    }

    public Task<bool> ExisteCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existe = _clientesPorId.Values.Any(c => c.Cnpj.Valor == cnpj.Valor);
        return Task.FromResult(existe);
    }

    public static void Limpar()
    {
        _clientesPorId.Clear();
    }
}
