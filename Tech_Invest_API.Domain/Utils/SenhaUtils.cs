using System.Security.Cryptography;
using System.Text;

namespace Tech_Invest_API.Domain.Utils;

public class SenhaUtils
{
    public static (byte[] Hash, byte[] Salt) GerarHashSenha(string senha, byte[]? salt = null)
    {
        using var hmac = salt is null ? new HMACSHA512() : new HMACSHA512(salt);
        return (hmac.ComputeHash(Encoding.UTF8.GetBytes(senha)), hmac.Key);
    }

    public static bool ValidarSenha(string senha, (byte[] Hash, byte[] Salt) senhaEncriptada)
    {
        return GerarHashSenha(senha, senhaEncriptada.Salt).Hash.SequenceEqual(senhaEncriptada.Hash);
    }
}
