using GestaoClientes.Dominio.Clientes;

namespace GestaoClientes.Aplicacao.Abstracoes;

public interface IClienteRepository
{
    Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken);
    Task<Cliente?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExisteCnpjAsync(Cnpj cnpj, CancellationToken cancellationToken);
}
