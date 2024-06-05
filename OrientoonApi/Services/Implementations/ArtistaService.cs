using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Infrastructure.Exceptions;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Services.Implementations
{
    public class ArtistaService : IArtistaService
    {

        private readonly IArtistaRepository _artistaRepository;
        private readonly IContextRepository _contextRepository;

        public ArtistaService(IArtistaRepository artistaRepository, IContextRepository contextRepository)
        {
            _artistaRepository = artistaRepository;
            _contextRepository = contextRepository;
        }

        public async Task<ArtistaForm> CreateAsync(ArtistaDto artistaDto)
        {
            ArtistaModel artistaModel = artistaDto.Converter();
            await _artistaRepository.AddAsync(artistaModel);
            await _contextRepository.SaveChangesAsync();
            return artistaModel.Converter();
        }

        public async Task CreateListAsync(List<ArtistaDto> artistaDto)
        {
            foreach (ArtistaDto artista in artistaDto)
            {
                ArtistaModel artistaModel = artista.Converter();
                await _artistaRepository.AddAsync(artistaModel);
            }
        }


        public async Task<ArtistaForm> GetAsync(int id)
        {
            if(!await _artistaRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Artista com Id: {id} não encontrado.");
            
            ArtistaModel artistaModel = await _artistaRepository.FindByIdAsync(id);

            return artistaModel.Converter();
        }

        public async Task<List<ArtistaForm>> GetListAsync(int batchSize, int pageNumber)
        {
            List<ArtistaModel> artistaModels = await _artistaRepository.GetByAmountAsync(batchSize, pageNumber);
            List<ArtistaForm> artistaForms = new List<ArtistaForm>();
            foreach (ArtistaModel artistaModel in artistaModels)
            {
                artistaForms.Add(artistaModel.Converter());
            }
            return artistaForms;

        }

        public async Task<ArtistaForm> UpdateAsync(int id, ArtistaDto artistaDto)
        {
            if(!await _artistaRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Artista com Id: {id} não encontrado.");
            
            ArtistaModel artistaModel = await _artistaRepository.FindByIdAsync(id);

            if(artistaDto.Nome != null)
                artistaModel.NomeArtista = artistaDto.Nome;
            
            await _artistaRepository.UpdateAsync(artistaModel);
            await _contextRepository.SaveChangesAsync();
            return artistaModel.Converter();
        }

        public async Task DeleteAsync(int id)
        {
            if(!await _artistaRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Artista com Id: {id} não encontrado.");
            

            await _artistaRepository.DeleteAsync(id);
            await _contextRepository.SaveChangesAsync();
        }

        public async Task<ArtistaForm> GetByNomeAsync(string nome)
        {
            if(!(await _artistaRepository.ExistByNomeAsync(nome)))
                throw new NotFoundException($"Artista com Nome: {nome} não encontrado.");

            ArtistaModel artistaModel = await _artistaRepository.FindByNomeAsync(nome);

            return artistaModel.Converter();
        }
    }
}
