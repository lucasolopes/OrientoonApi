using OrientoonApi.Data.Repositories.Implementations;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Infrastructure.Exceptions;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Services.Implementations
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IContextRepository _contextRepository;

        public GeneroService(IGeneroRepository generoRepository, IContextRepository contextRepository)
        {
            _generoRepository = generoRepository;
            _contextRepository = contextRepository;
        }

        public async Task<GeneroForm> CreateAsync(GeneroDto generoDto)
        {
            GeneroModel generoModel = generoDto.Converter();
            await _generoRepository.AddAsync(generoModel);
            await _contextRepository.SaveChangesAsync();
            return generoModel.Converter();
        }

        public async Task CreateListAsync(List<GeneroDto> generoDto)
        {
            foreach (GeneroDto genero in generoDto)
            {
                GeneroModel generoModel = genero.Converter();
                await _generoRepository.AddAsync(generoModel);
            }
        }


        public async Task<GeneroForm> GetAsync(int id)
        {
            if (!await _generoRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Genero com Id: {id} não encontrado.");

            GeneroModel generoModel = await _generoRepository.FindByIdAsync(id);

            return generoModel.Converter();
        }

        public async Task<List<GeneroForm>> GetListAsync(int batchSize, int pageNumber)
        {
            List<GeneroModel> generoModels = await _generoRepository.GetByAmountAsync(batchSize, pageNumber);
            List<GeneroForm> generoForms = new List<GeneroForm>();
            foreach (GeneroModel generoModel in generoModels)
            {
                generoForms.Add(generoModel.Converter());
            }
            return generoForms;
        }

        public async Task<GeneroForm> UpdateAsync(int id, GeneroDto generoDto)
        {
            if (!await _generoRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Genero com Id: {id} não encontrado.");

            GeneroModel generoModel = await _generoRepository.FindByIdAsync(id);

            if (generoDto.Nome != null)
                generoModel.NomeGenero = generoDto.Nome;

            await _generoRepository.UpdateAsync(generoModel);
            await _contextRepository.SaveChangesAsync();
            return generoModel.Converter();
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _generoRepository.ExistByIdAsync(id))
                throw new NotFoundException($"Genero com Id: {id} não encontrado.");


            await _generoRepository.DeleteAsync(id);
            await _contextRepository.SaveChangesAsync();
        }

        public async Task<GeneroForm> GetByNomeAsync(string nome)
        {
            if (!(await _generoRepository.ExistByNomeAsync(nome)))
                throw new NotFoundException($"Genero com Nome: {nome} não encontrado.");

            GeneroModel generoModel = await _generoRepository.FindByNomeAsync(nome);

            return generoModel.Converter();
        }
    }
}
