using OrientoonApi.Data.Repositories.Implementations;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Infrastructure.Exceptions;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;


namespace OrientoonApi.Status.Implementations
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IContextRepository _contextRepository;
        public StatusService(IStatusRepository statusRepository, IContextRepository contextRepository)
        {
            _statusRepository = statusRepository;
            _contextRepository = contextRepository;
        }

        public async Task<StatusForm> CreateAsync(StatusDto statusDto)
        {
            StatusModel statusModel = statusDto.Converter();
            await _statusRepository.AddAsync(statusModel); 
            await _contextRepository.SaveChangesAsync();
            return statusModel.Converter();
        }

        public async Task CreateListAsync(List<StatusDto> statusDto)
        {
            foreach (StatusDto status in statusDto)
            {
                StatusModel statusModel = status.Converter();
                await _statusRepository.AddAsync(statusModel);
            }
        }


        public async Task<StatusForm> GetAsync(string id)
        {
            if (!await _statusRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Status com Id: {id} não encontrado.");

            StatusModel statusModel = await _statusRepository.FindByIdAsync(id);

            return statusModel.Converter();
        }

        public async Task<List<StatusForm>> GetListAsync(int batchSize, int pageNumber)
        {
            List<StatusModel> statusModels = await _statusRepository.GetByAmountAsync(batchSize, pageNumber);
            List<StatusForm> statusForms = new List<StatusForm>();
            foreach (StatusModel statusModel in statusModels)
            {
                statusForms.Add(statusModel.Converter());
            }
            return statusForms;
        }

        public async Task<StatusForm> UpdateAsync(string id, StatusDto statusDto)
        {
            if (!await _statusRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Status com Id: {id} não encontrado.");

            StatusModel statusModel = await _statusRepository.FindByIdAsync(id);

            if (statusDto.Status != null)
                statusModel.Status = statusDto.Status;

            await _statusRepository.UpdateAsync(statusModel);
            await _contextRepository.SaveChangesAsync();
            return statusModel.Converter();
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _statusRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Status com Id: {id} não encontrado.");


            await _statusRepository.DeleteAsync(id);
            await _contextRepository.SaveChangesAsync();
        }

        public async Task<StatusForm> GetByNomeAsync(string nome)
        {
            if (!(await _statusRepository.ExistByStatusAsync(nome)))
                throw new NotFoundException($"Status com Nome: {nome} não encontrado.");

            StatusModel statusModel = await _statusRepository.FindByStatusAsync(nome);

            return statusModel.Converter();
        }
    }
}
