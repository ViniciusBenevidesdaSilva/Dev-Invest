using Tech_Invest_API.Domain.Model;

namespace Tech_Invest_API.Domain.Interfaces.General;

internal interface IUpdatable<T> where T : BaseEntity
{
    void UpdateFrom(T updatedEntity);
}
