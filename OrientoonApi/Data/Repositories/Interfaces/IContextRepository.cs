using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IContextRepository
    {
        Task SaveChangesAsync();
    }
}
