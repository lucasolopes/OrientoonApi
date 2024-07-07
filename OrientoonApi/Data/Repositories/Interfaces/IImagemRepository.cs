using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;
using static System.Net.Mime.MediaTypeNames;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IImagemRepository
    {
        Task AddAsync(ImagemModel imagem);
        Task<List<ImagemForm>> GetByCapituloIdAsync(string capituloId);
    }
}
