namespace GestaoClientes.Dominio.Excecoes;

public sealed class ExcecaoDeDominio : Exception
{
    public ExcecaoDeDominio(string mensagem) : base(mensagem) { }
}
