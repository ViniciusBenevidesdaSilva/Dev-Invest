namespace Tech_Invest_API.Domain.Model;

public class Usuario : Entity
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public byte[] SenhaHash { get; set; }
    public byte[] SenhaSalt { get; set; }

    public (byte[] Hash, byte[] Salt) Senha 
    {
        get => (SenhaHash, SenhaSalt);
        set
        {
            SenhaHash = value.Hash;
            SenhaSalt = value.Item2;
        }
    }
}
