using OrientoonApi.Data.Repositories.Implementations;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Infrastructure.Exceptions;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Services.Implementations
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IContextRepository _contextRepository;

        public AutorService(IAutorRepository autorRepository, IContextRepository contextRepository)
        {
            _autorRepository = autorRepository;
            _contextRepository = contextRepository;
        }

        public async Task<AutorForm> CreateAsync(AutorDto AutorDto)
        {
            AutorModel AutorModel = new AutorModel(AutorDto);
            await _autorRepository.AddAsync(AutorModel);
            await _contextRepository.SaveChangesAsync();
            return AutorModel.Converter();
        }

        public async Task CreateListAsync(List<AutorDto> AutorDto)
        {
            foreach (AutorDto Autor in AutorDto)
            {
                await CreateAsync(Autor);
            }
        }

        //remover dps
        public async Task<AutorForm> GetAsync(string id)
        {
            await ExistAutor(id);

            AutorModel AutorModel = await _autorRepository.GetByIdAsync(id);

            return AutorModel.Converter();
        }

        private async Task ExistAutor(string id)
        {
            if (!await _autorRepository.ExistsByIdAsync(id))
                throw new NotFoundException($"Autor com Id: {id} não encontrado.");
        }

        public async Task<List<AutorForm>> GetListAsync(int batchSize, int pageNumber)
        {
            List<AutorModel> AutorModels = await _autorRepository.GetByAmountAsync(batchSize, pageNumber);
            List<AutorForm> AutorForms = new List<AutorForm>();
            foreach (AutorModel AutorModel in AutorModels)
            {
                AutorForms.Add(AutorModel.Converter());
            }
            return AutorForms;
        }

        public async Task<AutorForm> UpdateAsync(string id, AutorDto AutorDto)
        {
            await ExistAutor(id);

            AutorModel AutorModel = await _autorRepository.GetByIdAsync(id);

            if (AutorDto.Nome != null)
                AutorModel.nome = AutorDto.Nome;

            await _autorRepository.UpdateAsync(AutorModel);
            await _contextRepository.SaveChangesAsync();
            return AutorModel.Converter();
        }

        public async Task DeleteAsync(string id)
        {
            await ExistAutor(id);


            await _autorRepository.DeleteAsync(id);
            await _contextRepository.SaveChangesAsync();
        }


        public async Task<AutorModel> GetByNomeAsync(string nomeAutor)
        {
            if (!(await _autorRepository.ExistsByNameAsync(nomeAutor)) || nomeAutor == "")
                throw new NotFoundException($"Autor: {nomeAutor} não encontrado");

            return await _autorRepository.GetByNomeAsync(nomeAutor);
        }

        public async Task<AutorModel> GetByIdAsync(string autorId)
        {
            await ExistAutor(autorId);

            return await _autorRepository.GetByIdAsync(autorId);
        }
    }
}
