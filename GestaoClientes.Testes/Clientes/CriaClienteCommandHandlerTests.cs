using GestaoClientes.Aplicacao.Clientes.Criar;
using GestaoClientes.Dominio.Clientes;
using GestaoClientes.Infraestrutura.Repositorios;
using Xunit;

namespace GestaoClientes.Testes.Clientes;

public sealed class CriaClienteCommandHandlerTests
{
    public CriaClienteCommandHandlerTests()
    {
        ClienteRepositoryEmMemoria.Limpar();
    }

    [Fact]
    public async Task Deve_criar_cliente_com_sucesso_quando_dados_validos()
    {
        // Arrange
        var repositorio = new ClienteRepositoryEmMemoria();
        var handler = new CriaClienteCommandHandler(repositorio);

        var command = new CriaClienteCommand(
            NomeFantasia: "Empresa Teste",
            Cnpj: "12.345.678/0001-95"
        );

        // Act
        var resultado = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.True(resultado.Sucesso);
        Assert.NotNull(resultado.Valor);
        Assert.Equal("Empresa Teste", resultado.Valor!.NomeFantasia);
        Assert.True(resultado.Valor.Ativo);
    }

    [Fact]
    public async Task Deve_retornar_erro_quando_cnpj_ja_existir()
    {
        // Arrange
        var repositorio = new ClienteRepositoryEmMemoria();
        var handler = new CriaClienteCommandHandler(repositorio);

        var command = new CriaClienteCommand(
            NomeFantasia: "Empresa Teste",
            Cnpj: "12.345.678/0001-95"
        );

        await handler.HandleAsync(command, CancellationToken.None);

        // Act
        var resultado = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.Equal("CNPJ_JA_EXISTE", resultado.CodigoErro);
    }

    [Fact]
    public async Task Deve_retornar_erro_quando_nome_fantasia_for_invalido()
    {
        // Arrange
        var repositorio = new ClienteRepositoryEmMemoria();
        var handler = new CriaClienteCommandHandler(repositorio);

        var command = new CriaClienteCommand(
            NomeFantasia: "",
            Cnpj: "12.345.678/0001-95"
        );

        // Act
        var resultado = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.Equal("DADOS_INVALIDOS", resultado.CodigoErro);
    }
}
