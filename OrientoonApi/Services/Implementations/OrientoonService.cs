using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Repositories;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Infrastructure.Exceptions;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Request;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;
using System.Globalization;

namespace OrientoonApi.Services.Implementations
{
	public class OrientoonService : IOrientoonService
	{
		private readonly IOrientoonRepository _orientoonRepository;
		private readonly IArtistaRepository _artistaRepository;
		private readonly IAutorRepository _autorRepository;
		private readonly IContextRepository _contextRepository;
		private readonly IStatusRepository _statusRepository;
		private readonly IGeneroRepository _generoRepository;
		private readonly IGeneroOrientoonRepository _generoOrientoonRepository;
		private readonly ITipoRepository _tipoRepository;
		private readonly ITipoOrientoonRepository _tipoOrientoonRepository;

		public OrientoonService(IOrientoonRepository orientoonRepository, IArtistaRepository artistaRepository, IAutorRepository autorRepository, IContextRepository contextRepository, IStatusRepository statusRepository, IGeneroRepository generoRepository, IGeneroOrientoonRepository generoOrientoonRepository,ITipoRepository tipoRepository, ITipoOrientoonRepository tipoOrientoonRepository)
		{
			_orientoonRepository = orientoonRepository;
			_artistaRepository = artistaRepository;
			_autorRepository = autorRepository;
			_contextRepository = contextRepository;
			_statusRepository = statusRepository;
			_generoRepository = generoRepository;
			_generoOrientoonRepository = generoOrientoonRepository;
			_tipoRepository = tipoRepository;
			_tipoOrientoonRepository = tipoOrientoonRepository;
		}

		public async Task<OrientoonForm> CreateAsync(OrientoonDto orientoonDto)
		{
			OrientoonModel orientoonModel = orientoonDto.Converter();

				//orientoonModel id randon text somente letras e numeros
			orientoonModel.Id = Guid.NewGuid().ToString("N");

				//implmentar na propria controller depois
			orientoonModel.CBanner = "implementar depois a funcao";
				
			if(!(await _artistaRepository.ExistByNomeAsync(orientoonDto.NomeArtista)))
				throw new NotFoundException($"Artista: {orientoonDto.NomeArtista} não encontrado");
			
			  
			ArtistaModel artista = await _artistaRepository.FindByNomeAsync(orientoonDto.NomeArtista);

			if(!(await _autorRepository.ExistByNomeAsync(orientoonDto.NomeAutor)))
				throw new NotFoundException($"Autor: {orientoonDto.NomeAutor} não encontrado");
			
			AutorModel autor = await _autorRepository.FindByNomeAsync(orientoonDto.NomeAutor);

			if(!(await _statusRepository.ExistByStatusAsync(orientoonDto.Status)))
				throw new NotFoundException($"Status: {orientoonDto.Status} não encontrado");
			

			StatusModel status = await _statusRepository.FindByStatusAsync(orientoonDto.Status);

			orientoonModel.Artista = artista;
			orientoonModel.Autor = autor;
			orientoonModel.Status = status;
			orientoonModel.NormalizedTitulo = orientoonModel.Titulo.ToUpper(CultureInfo.InvariantCulture);

			await _orientoonRepository.AddAsync(orientoonModel);
			await _contextRepository.SaveChangesAsync();
			//criar um response depois e retornar ele
			return orientoonModel.Converter();

		}



		public async Task<OrientoonForm> GetAsync(string id)
		{
			if(!(await _orientoonRepository.ExistBtIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

			return await _orientoonRepository.FindByIdAsync(id);
		}

		/*public async Task<List<OrientoonForm>> GetListAsync(int batchSize, int pageNumber)
		{
			return await _orientoonRepository.GetByAmountAsync(batchSize, pageNumber);
		}*/

		public async Task CreateListAsync(List<OrientoonDto> orientoon)
		{
			foreach (var orientoonDto in orientoon)
			{
				 await CreateAsync(orientoonDto);
			} 
		}

		public async Task<OrientoonForm> UpdateAsync(string id, OrientoonPutDto orientoon)
		{

			if(!(await _orientoonRepository.ExistBtIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

			//OrientoonForm oldOrientoon = await _orientoonRepository.FindByIdAsync(id);

			OrientoonModel oldOrientoon = (await _orientoonRepository.FindByIdAsync(id)).Converter();

			OrientoonModel newOrientoon = orientoon.Converter();

			if (orientoon.NomeAutor != ""  )
				if (!(await _autorRepository.ExistByNomeAsync(orientoon.NomeAutor)))
					throw new NotFoundException($"Autor: {orientoon.NomeAutor} não encontrado");
				
			
			if(orientoon.NomeArtista != "")
				if(!(await _artistaRepository.ExistByNomeAsync(orientoon.NomeArtista)))
					throw new NotFoundException($"Artista: {orientoon.NomeArtista} não encontrado");


			if (orientoon.Status != "")
				if(!(await _statusRepository.ExistByStatusAsync(orientoon.Status)))
					throw new NotFoundException($"Status: {orientoon.Status} não encontrado");


			string newNomeAutor = orientoon.NomeAutor;
			string newNomeArtista = orientoon.NomeArtista;

			AutorModel oldAutor = await _autorRepository.FindByIdAsync(oldOrientoon.AutorId);
			ArtistaModel oldArtista = await _artistaRepository.FindByIdAsync(oldOrientoon.ArtistaId);


			string newNomeStatus = orientoon.Status;
			StatusModel oldStatus = await _statusRepository.FindByIdAsync(oldOrientoon.StatusId);


			//verifique se os valores são diferentes e nao nulos e atualize
			if (newOrientoon.Titulo != "" && newOrientoon.Titulo != oldOrientoon.Titulo)
				oldOrientoon.Titulo = newOrientoon.Titulo;
			
			if (newOrientoon.Descricao != "" && newOrientoon.Descricao != oldOrientoon.Descricao)
				oldOrientoon.Descricao = newOrientoon.Descricao;
			
			if (newOrientoon.DataLancamento != null && newOrientoon.DataLancamento != oldOrientoon.DataLancamento)
				oldOrientoon.DataLancamento = newOrientoon.DataLancamento;
			
			if (newNomeArtista != "" && newNomeArtista != oldArtista.NomeArtista)
				oldArtista.NomeArtista = newNomeArtista;
			
			if (newNomeAutor != "" && newNomeAutor != oldAutor.NomeAutor)
				oldAutor.NomeAutor = newNomeAutor;
			
			if(newNomeStatus != "" && newNomeStatus != oldStatus.Status)
				oldStatus.Status = newNomeStatus;
			
			oldOrientoon.Autor = oldAutor;
			oldOrientoon.Artista = oldArtista;
			oldOrientoon.Status = oldStatus;

			await _orientoonRepository.UpdateAsync(oldOrientoon);
			await _contextRepository.SaveChangesAsync();
			return oldOrientoon.Converter();
		}

		public async Task DeleteAsync(string id)
		{
			if(!(await _orientoonRepository.ExistBtIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

			await _orientoonRepository.DeleteAsync(id);
			await _contextRepository.SaveChangesAsync();
		}

		public async Task<IEnumerable<OrientoonForm>> SearchAsync( int batchSize, int pageNumber, SearchDto? searchDto)
		{


			return await _orientoonRepository.SearchAsync(batchSize, pageNumber,searchDto);
		}

		public async Task AddGeneroAsync(string id, GeneroDto generoDto)
		{
			if(!(await _orientoonRepository.ExistBtIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

			if(!(await _generoRepository.ExistByNomeAsync(generoDto.Nome)))
				throw new NotFoundException($"Genero: {generoDto.Nome} não encontrado");

			if (await _generoOrientoonRepository.ExistByGeneroIdAsync(id, generoDto.Nome))
				throw new ConflictException($"Genero: {generoDto.Nome} já existe no Orientoon");

			GeneroModel genero = await _generoRepository.FindByNomeAsync(generoDto.Nome);
			await _generoOrientoonRepository.AddAsync(id, genero.Id);
			await _contextRepository.SaveChangesAsync();
			
		}

		public async Task DeleteGeneroAsync(string id, GeneroDto generoDto)
		{
		    if(!(await _orientoonRepository.ExistBtIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

		    if(!(await _generoRepository.ExistByNomeAsync(generoDto.Nome)))
				throw new NotFoundException($"Genero: {generoDto.Nome} não encontrado");

			GeneroModel genero = await _generoRepository.FindByNomeAsync(generoDto.Nome);
			await _generoOrientoonRepository.DeleteAsync(id, genero.Id);
			await _contextRepository.SaveChangesAsync();

		}



        public async Task AddTipoAsync(string id, TipoDto tipoDto)
        {
            if(!(await _orientoonRepository.ExistBtIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

			if(!(await _tipoRepository.ExistByNomeAsync(tipoDto.Nome)))
				throw new NotFoundException($"Tipo: {tipoDto.Nome} não encontrado");

			if (await _tipoOrientoonRepository.ExistByTipoIdAsync(id, tipoDto.Nome))
				throw new ConflictException($"Tipo: {tipoDto.Nome} já existe no Orientoon");

			TipoModel tipo = await _tipoRepository.FindByNomeAsync(tipoDto.Nome);
			await _tipoOrientoonRepository.AddAsync(id, tipo.Id);
			await _contextRepository.SaveChangesAsync();
        }

        public async Task DeleteTipoAsync(string id, TipoDto tipoDto)
        {
            if(!(await _orientoonRepository.ExistBtIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");
			if(!(await _tipoRepository.ExistByNomeAsync(tipoDto.Nome)))
				throw new NotFoundException($"Tipo: {tipoDto.Nome} não encontrado");

			TipoModel tipo = await _tipoRepository.FindByNomeAsync(tipoDto.Nome);
			await _tipoOrientoonRepository.DeleteAsync(id, tipo.Id);
			await _contextRepository.SaveChangesAsync();
        }
    }
}
