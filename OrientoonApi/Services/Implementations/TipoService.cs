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
            TipoModel tipoModel = tipoDto.Converter();
            await _tipoRepository.AddAsync(tipoModel);
            await _contextRepository.SaveChangesAsync();
            return tipoModel.Converter();
        }

        public async Task CreateListAsync(List<TipoDto> tipoDto)
        {
            foreach (TipoDto tipo in tipoDto)
            {
                TipoModel tipoModel = tipo.Converter();
                await _tipoRepository.AddAsync(tipoModel);
            }
        }


        public async Task<TipoForm> GetAsync(int id)
        {
            if (!await _tipoRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Tipo com Id: {id} não encontrado.");

            TipoModel tipoModel = await _tipoRepository.FindByIdAsync(id);

            return tipoModel.Converter();
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

        public async Task<TipoForm> UpdateAsync(int id, TipoDto tipoDto)
        {
            if (!await _tipoRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Tipo com Id: {id} não encontrado.");

            TipoModel tipoModel = await _tipoRepository.FindByIdAsync(id);

            if (tipoDto.Nome != null)
                tipoModel.NomeTipo = tipoDto.Nome;

            await _tipoRepository.UpdateAsync(tipoModel);
            await _contextRepository.SaveChangesAsync();
            return tipoModel.Converter();
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _tipoRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Tipo com Id: {id} não encontrado.");


            await _tipoRepository.DeleteAsync(id);
            await _contextRepository.SaveChangesAsync();
        }

        public async Task<TipoForm> GetByNomeAsync(string nome)
        {
            if (!(await _tipoRepository.ExistByNomeAsync(nome)))
                throw new NotFoundException($"Tipo com Nome: {nome} não encontrado.");

            TipoModel tipoModel = await _tipoRepository.FindByNomeAsync(nome);

            return tipoModel.Converter();
        }
    }
}
