using OrientoonApi.Data.Repositories.Implementations;
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
            ArtistaModel artistaModel = new ArtistaModel(artistaDto);
            await _artistaRepository.AddAsync(artistaModel);
            await _contextRepository.SaveChangesAsync();
            return artistaModel.Converter();
        }

        public async Task CreateListAsync(List<ArtistaDto> artistaDto)
        {
            foreach (ArtistaDto artista in artistaDto)
            {
                await CreateAsync(artista);
            }
        }


        public async Task<ArtistaForm> GetAsync(string id)
        {
            if(!await _artistaRepository.ExistsByIdAsync(id))
                throw new NotFoundException($"Artista com Id: {id} não encontrado.");
            
            ArtistaModel artistaModel = await _artistaRepository.GetByIdAsync(id);

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

        public async Task<ArtistaForm> UpdateAsync(string id, ArtistaDto artistaDto)
        {
             if(!await _artistaRepository.ExistsByIdAsync(id))
                 throw new NotFoundException($"Artista com Id: {id} não encontrado.");

             ArtistaModel artistaModel = await _artistaRepository.GetByIdAsync(id);

             if(artistaDto.Nome != null)
                 artistaModel.nome = artistaDto.Nome;

             await _artistaRepository.UpdateAsync(artistaModel);
             await _contextRepository.SaveChangesAsync();
             return artistaModel.Converter();
        }

        public async Task DeleteAsync(string id)
        {
            if(!await _artistaRepository.ExistsByIdAsync(id))
                throw new NotFoundException($"Artista com Id: {id} não encontrado.");
            

            await _artistaRepository.DeleteAsync(id);
            await _contextRepository.SaveChangesAsync();
        }


        public async Task<ArtistaModel> GetByNomeAsync(string nomeArtista)
        {

            if (!(await _artistaRepository.ExistsByNameAsync(nomeArtista)) || nomeArtista == "")
                throw new NotFoundException($"Artista: {nomeArtista} não encontrado");


            return await _artistaRepository.GetByNomeAsync(nomeArtista);
        }

        public async Task<ArtistaModel> GetByIdAsync(string artistaId)
        {
            if (!await _artistaRepository.ExistsByIdAsync(artistaId))
                throw new NotFoundException($"Artista com Id: {artistaId} não encontrado.");

            return await _artistaRepository.GetByIdAsync(artistaId);
        }
    }
}
