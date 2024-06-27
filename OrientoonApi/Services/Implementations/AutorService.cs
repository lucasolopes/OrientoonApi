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
            AutorModel AutorModel = AutorDto.Converter();
            await _autorRepository.AddAsync(AutorModel);
            await _contextRepository.SaveChangesAsync();
            return AutorModel.Converter();
        }

        public async Task CreateListAsync(List<AutorDto> AutorDto)
        {
            foreach (AutorDto Autor in AutorDto)
            {
                AutorModel AutorModel = Autor.Converter();
                await _autorRepository.AddAsync(AutorModel);
            }
        }


        public async Task<AutorForm> GetAsync(string id)
        {
            if (!await _autorRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Autor com Id: {id} não encontrado.");

            AutorModel AutorModel = await _autorRepository.FindByIdAsync(id);

            return AutorModel.Converter();
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
            if (!await _autorRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Autor com Id: {id} não encontrado.");

            AutorModel AutorModel = await _autorRepository.FindByIdAsync(id);

            if (AutorDto.Nome != null)
                AutorModel.NomeAutor = AutorDto.Nome;

            await _autorRepository.UpdateAsync(AutorModel);
            await _contextRepository.SaveChangesAsync();
            return AutorModel.Converter();
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _autorRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Autor com Id: {id} não encontrado.");


            await _autorRepository.DeleteAsync(id);
            await _contextRepository.SaveChangesAsync();
        }

        public async Task<AutorForm> GetByNomeAsync(string nome)
        {
            if (!(await _autorRepository.ExistByNomeAsync(nome)))
                throw new NotFoundException($"Autor com Nome: {nome} não encontrado.");

            AutorModel AutorModel = await _autorRepository.FindByNomeAsync(nome);

            return AutorModel.Converter();
        }
    }
}
