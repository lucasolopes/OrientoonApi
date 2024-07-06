using OrientoonApi.Models.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace OrientoonApi.Data.Repositories.Interfaces
{
    public interface IImagemRepository
    {
        Task AddAsync(ImagemModel imagem);
        Task<List<ImagemModel>> GetByCapituloIdAsync(string capituloId);
    }
}
