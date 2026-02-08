using GestaoClientes.Dominio.Excecoes;

namespace GestaoClientes.Dominio.Clientes;

public sealed class Cliente
{
    public Guid Id { get; private set; }
    public string NomeFantasia { get; private set; } = string.Empty;
    public Cnpj Cnpj { get; private set; } = null!;
    public bool Ativo { get; private set; }

    private Cliente() { } // Para serializers/ORMs futuros (sem quebrar invariantes)

    private Cliente(Guid id, string nomeFantasia, Cnpj cnpj, bool ativo)
    {
        Id = id;
        NomeFantasia = nomeFantasia;
        Cnpj = cnpj;
        Ativo = ativo;

        Validar();
    }

    public static Cliente Criar(string? nomeFantasia, Cnpj cnpj)
    {
        var cliente = new Cliente(Guid.NewGuid(), nomeFantasia?.Trim() ?? string.Empty, cnpj, ativo: true);
        return cliente;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
    }

    private void Validar()
    {
        if (string.IsNullOrWhiteSpace(NomeFantasia))
            throw new ExcecaoDeDominio("Nome fantasia é obrigatório.");

        if (NomeFantasia.Length > 200)
            throw new ExcecaoDeDominio("Nome fantasia não pode ter mais que 200 caracteres.");

        if (Cnpj is null)
            throw new ExcecaoDeDominio("CNPJ é obrigatório.");
    }
}
