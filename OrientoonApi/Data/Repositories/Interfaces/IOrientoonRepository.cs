using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IOrientoonRepository : IGenericRepository<OrientoonModel>
    {
          //Task<OrientoonForm> GetByIdAsync(string id); criar override em GetByIdAsync
          Task<OrientoonForm> GetByIdAsync(string id);
          Task<List<OrientoonForm>> SearchAsync( int batchSize, int pageNumber,SearchDto? searchDto);
          Task<string> GetPathBannerById(string id);
          Task<OrientoonModel> GetModelByIdAsync(string id);
    }
}
