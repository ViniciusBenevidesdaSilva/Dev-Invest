using Tech_Invest_API.Domain.Utils.Enums;

namespace Tech_Invest_API.Domain.Model;

public class Usuario : BaseEntity
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public byte[] SenhaHash { get; set; }
    public byte[] SenhaSalt { get; set; }
    public EnumUserRole UserRole { get; set; } = EnumUserRole.User;
    public (byte[] Hash, byte[] Salt) Senha
    {
        get => (SenhaHash, SenhaSalt);
        set
        {
            SenhaHash = value.Hash;
            SenhaSalt = value.Item2;
        }
    }

    public override void UpdateFrom(BaseEntity updatedEntity)
    {
        base.UpdateFrom(updatedEntity);

        if(updatedEntity is Usuario usuarioAtualizado)
        {
            this.Nome = usuarioAtualizado.Nome;
            this.Email = usuarioAtualizado.Email;
            this.Senha = usuarioAtualizado.Senha;
            this.UserRole = usuarioAtualizado.UserRole;
        }
    }
}
