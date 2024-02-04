namespace Tech_Invest_API.Domain.Model;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public virtual void UpdateFrom(BaseEntity updatedEntity)
    {
        if (GetType() != updatedEntity.GetType())
            throw new Exception("A conversão deve ser feita entre o mesmo tipo de entidade");
    }
}
