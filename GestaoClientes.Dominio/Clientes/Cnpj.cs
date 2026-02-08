using System.Text.RegularExpressions;
using GestaoClientes.Dominio.Excecoes;

namespace GestaoClientes.Dominio.Clientes;

public sealed record Cnpj
{
    public string Valor { get; }

    private Cnpj(string valorNormalizado)
    {
        Valor = valorNormalizado;
    }

    public static Cnpj Criar(string? valor)
    {
        var normalizado = Normalizar(valor);

        if (!EhValido(normalizado))
            throw new ExcecaoDeDominio("CNPJ inválido.");

        return new Cnpj(normalizado);
    }

    public static string Normalizar(string? valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            return string.Empty;

        // remove tudo que não for dígito
        return Regex.Replace(valor, "[^0-9]", "");
    }

    public static bool EhValido(string? valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            return false;

        var cnpj = Normalizar(valor);

        if (cnpj.Length != 14)
            return false;

        // evita sequências do tipo 00000000000000, 11111111111111 etc.
        if (cnpj.Distinct().Count() == 1)
            return false;

        var dvCalculado = CalcularDigitosVerificadores(cnpj[..12]);
        var dvInformado = cnpj.Substring(12, 2);

        return dvCalculado == dvInformado;
    }

    public override string ToString() => Valor;

    private static string CalcularDigitosVerificadores(string base12)
    {
        var primeiro = CalcularDigito(base12, new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });
        var segundo = CalcularDigito(base12 + primeiro, new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });
        return $"{primeiro}{segundo}";
    }

    private static int CalcularDigito(string textoNumerico, int[] pesos)
    {
        var soma = 0;

        for (var i = 0; i < pesos.Length; i++)
        {
            soma += (textoNumerico[i] - '0') * pesos[i];
        }

        var resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
