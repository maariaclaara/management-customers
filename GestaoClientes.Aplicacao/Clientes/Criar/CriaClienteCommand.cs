namespace GestaoClientes.Aplicacao.Clientes.Criar;

public sealed record CriaClienteCommand(
    string NomeFantasia,
    string Cnpj
);
