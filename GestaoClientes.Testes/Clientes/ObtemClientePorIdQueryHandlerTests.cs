using GestaoClientes.Aplicacao.Clientes.Criar;
using GestaoClientes.Aplicacao.Clientes.ObterPorId;
using GestaoClientes.Infraestrutura.Repositorios;
using Xunit;

namespace GestaoClientes.Testes.Clientes;

public sealed class ObtemClientePorIdQueryHandlerTests
{
    public ObtemClientePorIdQueryHandlerTests()
    {
        ClienteRepositoryEmMemoria.Limpar();
    }

    [Fact]
    public async Task Deve_retornar_cliente_quando_id_existir()
    {
        // Arrange
        var repositorio = new ClienteRepositoryEmMemoria();

        var criaHandler = new CriaClienteCommandHandler(repositorio);
        var criaResultado = await criaHandler.HandleAsync(
            new CriaClienteCommand("Empresa Teste", "12.345.678/0001-95"),
            CancellationToken.None);

        var id = criaResultado.Valor!.Id;

        var handler = new ObtemClientePorIdQueryHandler(repositorio);

        // Act
        var resultado = await handler.HandleAsync(
            new ObtemClientePorIdQuery(id),
            CancellationToken.None);

        // Assert
        Assert.True(resultado.Sucesso);
        Assert.NotNull(resultado.Valor);
        Assert.Equal(id, resultado.Valor!.Id);
    }

    [Fact]
    public async Task Deve_retornar_null_quando_id_nao_existir()
    {
        // Arrange
        var repositorio = new ClienteRepositoryEmMemoria();
        var handler = new ObtemClientePorIdQueryHandler(repositorio);

        // Act
        var resultado = await handler.HandleAsync(
            new ObtemClientePorIdQuery(Guid.NewGuid()),
            CancellationToken.None);

        // Assert
        Assert.True(resultado.Sucesso);
        Assert.Null(resultado.Valor);
    }
}
