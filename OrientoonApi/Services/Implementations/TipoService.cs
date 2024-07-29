using OrientoonApi.Data.Repositories.Implementations;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Infrastructure.Exceptions;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Services.Implementations
{
    public class TipoService : ITipoService
    {
        private readonly ITipoRepository _tipoRepository;
        private readonly IContextRepository _contextRepository;

        public TipoService(ITipoRepository tipoRepository, IContextRepository contextRepository)
        {
            _tipoRepository = tipoRepository;
            _contextRepository = contextRepository;
        }

        public async Task<TipoForm> CreateAsync(TipoDto tipoDto)
        {
            TipoModel tipoModel = new TipoModel(tipoDto);
            await _tipoRepository.AddAsync(tipoModel);
            await _contextRepository.SaveChangesAsync();
            return tipoModel.Converter();
        }

        public async Task CreateListAsync(List<TipoDto> tipoDto)
        {
            foreach (TipoDto tipo in tipoDto)
            {
                TipoModel tipoModel = new TipoModel(tipo);
                await _tipoRepository.AddAsync(tipoModel);
            }
        }


        public async Task<TipoForm> GetAsync(string id)
        {
            await ExistTipo(id);

            TipoModel tipoModel = await _tipoRepository.GetByIdAsync(id);

            return tipoModel.Converter();
        }

        private async Task ExistTipo(string id)
        {
            if (!await _tipoRepository.ExistsByIdAsync(id))
                throw new NotFoundException($"Tipo com Id: {id} não encontrado.");
        }

        public async Task<List<TipoForm>> GetListAsync(int batchSize, int pageNumber)
        {
            List<TipoModel> tipoModels = await _tipoRepository.GetByAmountAsync(batchSize, pageNumber);
            List<TipoForm> tipoForms = new List<TipoForm>();
            foreach (TipoModel tipoModel in tipoModels)
            {
                tipoForms.Add(tipoModel.Converter());
            }
            return tipoForms;

        }

        public async Task<TipoForm> UpdateAsync(string id, TipoDto tipoDto)
        {
            await ExistTipo(id);

            TipoModel tipoModel = await _tipoRepository.GetByIdAsync(id);

            if (tipoDto.Nome != null)
                tipoModel.nome = tipoDto.Nome;

            await _tipoRepository.UpdateAsync(tipoModel);
            await _contextRepository.SaveChangesAsync();
            return tipoModel.Converter();
        }

        public async Task DeleteAsync(string id)
        {
            await ExistTipo(id);


            await _tipoRepository.DeleteAsync(id);
            await _contextRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string nome)
        {
            return await _tipoRepository.ExistsByNameAsync(nome);
        }

        public async Task<TipoModel> GetByNomeAsync(string nome)
        {
            if (!(await _tipoRepository.ExistsByNameAsync(nome)))
                throw new NotFoundException($"Tipo com Nome: {nome} não encontrado.");

            return await _tipoRepository.GetByNomeAsync(nome);
        }
    }
}
