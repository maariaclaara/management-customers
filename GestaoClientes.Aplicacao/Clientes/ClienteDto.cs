namespace GestaoClientes.Aplicacao.Clientes;

public sealed record ClienteDto(
    Guid Id,
    string NomeFantasia,
    string Cnpj,
    bool Ativo
);
