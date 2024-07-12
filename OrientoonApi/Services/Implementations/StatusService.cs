using OrientoonApi.Data.Repositories.Implementations;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Enum;
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
		public StatusService(IStatusRepository statusRepository)
		{
			_statusRepository = statusRepository;
		}




        public async Task<StatusModel> GetByIdAsync(string id)
        {
            if (!await _statusRepository.ExistsByIdAsync(id))
                throw new NotFoundException($"Status com Id: {id} não encontrado.");

			StatusModel statusModel = await _statusRepository.GetByIdAsync(id);

			return statusModel;
		}
		

		public async Task<IEnumerable<StatusModel>> GetAllAsync()
		{
			IEnumerable<StatusModel> statusModels = await _statusRepository.GetAllAsync();
			return statusModels;
		}

        public async Task<StatusModel> GetByNameAsync(StatusEnum? status)
        {
			if(!await _statusRepository.ExistsByNameAsync(status))
				throw new NotFoundException($"Status com nome: {status} não encontrado.");

            return await _statusRepository.GetByNameAsync(status);
        }
    }
}
