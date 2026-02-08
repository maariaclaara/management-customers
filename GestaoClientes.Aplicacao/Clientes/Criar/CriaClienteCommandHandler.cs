using GestaoClientes.Aplicacao.Abstracoes;
using GestaoClientes.Aplicacao.Resultados;
using GestaoClientes.Dominio.Clientes;
using GestaoClientes.Dominio.Excecoes;

namespace GestaoClientes.Aplicacao.Clientes.Criar;

public sealed class CriaClienteCommandHandler
{
    private readonly IClienteRepository _clienteRepository;

    public CriaClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Resultado<ClienteDto>> HandleAsync(CriaClienteCommand command, CancellationToken cancellationToken)
    {
        if (command is null)
            return Resultado<ClienteDto>.Falha("COMANDO_INVALIDO", "Comando inválido.");

        if (string.IsNullOrWhiteSpace(command.NomeFantasia))
            return Resultado<ClienteDto>.Falha("DADOS_INVALIDOS", "Nome fantasia é obrigatório.");

        if (string.IsNullOrWhiteSpace(command.Cnpj))
            return Resultado<ClienteDto>.Falha("DADOS_INVALIDOS", "CNPJ é obrigatório.");

        Cnpj cnpj;
        try
        {
            cnpj = Cnpj.Criar(command.Cnpj);
        }
        catch (ExcecaoDeDominio ex)
        {
            return Resultado<ClienteDto>.Falha("DADOS_INVALIDOS", ex.Message);
        }

        var jaExiste = await _clienteRepository.ExisteCnpjAsync(cnpj, cancellationToken);
        if (jaExiste)
            return Resultado<ClienteDto>.Falha("CNPJ_JA_EXISTE", "Já existe um cliente cadastrado com este CNPJ.");

        Cliente cliente;
        try
        {
            cliente = Cliente.Criar(command.NomeFantasia, cnpj);
        }
        catch (ExcecaoDeDominio ex)
        {
            return Resultado<ClienteDto>.Falha("DADOS_INVALIDOS", ex.Message);
        }

        await _clienteRepository.AdicionarAsync(cliente, cancellationToken);

        var dto = new ClienteDto(
            cliente.Id,
            cliente.NomeFantasia,
            cliente.Cnpj.Valor,
            cliente.Ativo
        );

        return Resultado<ClienteDto>.Ok(dto);
    }
}
