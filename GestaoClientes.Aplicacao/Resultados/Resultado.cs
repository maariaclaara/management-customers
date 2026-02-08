namespace GestaoClientes.Aplicacao.Resultados;

public sealed class Resultado<T>
{
    public bool Sucesso { get; }
    public string? CodigoErro { get; }
    public string? MensagemErro { get; }
    public T? Valor { get; }

    private Resultado(bool sucesso, T? valor, string? codigoErro, string? mensagemErro)
    {
        Sucesso = sucesso;
        Valor = valor;
        CodigoErro = codigoErro;
        MensagemErro = mensagemErro;
    }

    public static Resultado<T> Ok(T valor) =>
        new(true, valor, null, null);

    public static Resultado<T> Falha(string codigoErro, string mensagemErro) =>
        new(false, default, codigoErro, mensagemErro);
}
