using Microsoft.AspNetCore.Mvc;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

		private readonly IArtistaService _artistaService;
		private readonly IAutorService _autorService;
		private readonly IStatusService _statusService;
        private readonly IContextRepository _contextRepository;
		private readonly IGeneroService _generoService;
        private readonly IGeneroOrientoonRepository _generoOrientoonRepository;
        private readonly ITipoOrientoonRepository _tipoOrientoonRepository;
		private readonly ITipoService _tipoService;
        private readonly IConfiguration _configuration;
		private readonly ICapituloService _capituloService;

        public OrientoonService(IOrientoonRepository orientoonRepository, IContextRepository contextRepository,IGeneroOrientoonRepository generoOrientoonRepository, ITipoOrientoonRepository tipoOrientoonRepository, IHttpContextAccessor httpContextAccessor, IArtistaService artistaService, IAutorService autorService, IStatusService statusService, IGeneroService generoService, ITipoService tipoService, IConfiguration configuration, ICapituloService capituloService)
		{
			_orientoonRepository = orientoonRepository;

			_httpContextAccessor = httpContextAccessor;
			_artistaService = artistaService;
			_autorService = autorService;
			_statusService = statusService;
            _contextRepository = contextRepository;
			_generoService = generoService;
			_generoOrientoonRepository = generoOrientoonRepository;
            _tipoOrientoonRepository = tipoOrientoonRepository;
			_tipoService = tipoService;
			_configuration = configuration;
			_capituloService = capituloService;
        }

		public async Task<OrientoonForm> CreateAsync(OrientoonDto orientoonDto, IFormFile banner)
        {
			OrientoonModel orientoonModel = new OrientoonModel(orientoonDto);

            string filePath = await SaveBanner(banner, orientoonModel);

            orientoonModel.CBanner = filePath;

            orientoonModel.Artista = await _artistaService.GetByIdAsync(orientoonDto.IdArtista);
            orientoonModel.Autor = await _autorService.GetByIdAsync(orientoonDto.IdAutor);
            orientoonModel.Status = await _statusService.GetByIdAsync(orientoonDto.IdStatus);
            orientoonModel.NormalizedName = orientoonModel.nome.ToUpper(CultureInfo.InvariantCulture);

            await _orientoonRepository.AddAsync(orientoonModel);
            await _contextRepository.SaveChangesAsync();
			//criar um response depois e retornar ele
			return orientoonModel.Converter();

        }

        private async Task<string> SaveBanner(IFormFile banner, OrientoonModel orientoonModel)
        {
            var uploadPath = _configuration["FileUploadPath"];
            var bannerDirectory = Path.Combine(uploadPath, orientoonModel.Id);
            if (!Directory.Exists(bannerDirectory))
                Directory.CreateDirectory(bannerDirectory);

            var fileName = Path.GetFileName(banner.FileName);
            var filePath = Path.Combine(bannerDirectory, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await banner.CopyToAsync(stream);
            }

            return Path.Combine(orientoonModel.Id, fileName);
        }


        public async Task<OrientoonForm> GetAsync(string id)
		{
			if(!(await _orientoonRepository.ExistsByIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

            var request = _httpContextAccessor.HttpContext.Request;
            OrientoonModel orientoonModel = await _orientoonRepository.GetByIdAsync(id);

           // var host = $"{request.Scheme}://{request.Host}/imagens/";
           // orientoonForm.Banner = host + orientoonForm.Banner;
            return orientoonModel.Converter();
        }

		/*public async Task<List<OrientoonForm>> GetListAsync(int batchSize, int pageNumber)
		{
			return await _orientoonRepository.GetByAmountAsync(batchSize, pageNumber);
		}*/

		public async Task CreateListAsync(List<OrientoonDto> orientoon)
		{
			/*foreach (var orientoonDto in orientoon)
			{
				 await CreateAsync(orientoonDto);
			} */
		}

		public async Task<OrientoonForm> UpdateAsync(string id, OrientoonPutDto orientoon)
		{
			OrientoonModel oldOrientoon = await _orientoonRepository.GetByIdAsync(id);

			OrientoonModel newOrientoon = new OrientoonModel(orientoon);

			ArtistaModel newArtista = new ArtistaModel();
			AutorModel newAutor = new AutorModel();

			StatusModel newStatus = null;

			if(orientoon.IdArtista != null)
				newArtista = await _artistaService.GetByIdAsync(orientoon.IdArtista);
			
			if(orientoon.IdAutor != null)
                newAutor = await _autorService.GetByIdAsync(orientoon.IdAutor);

			if(orientoon.Status != null)
                newStatus = await _statusService.GetByNameAsync(orientoon.Status);

            
			ArtistaModel oldArtista = await _artistaService.GetByIdAsync(oldOrientoon.ArtistaId);

            AutorModel oldAutor = await _autorService.GetByIdAsync(oldOrientoon.AutorId);


            StatusModel oldStatus = await _statusService.GetByIdAsync(oldOrientoon.StatusId);

			oldOrientoon.AdultContent = orientoon.AdultContent ?? oldOrientoon.AdultContent;

			//verifique se os valores são diferentes e nao nulos e atualize
			if (newOrientoon.nome != null && newOrientoon.nome != oldOrientoon.nome)
				oldOrientoon.nome = newOrientoon.nome;
			
			if (newOrientoon.Descricao != null && newOrientoon.Descricao != oldOrientoon.Descricao)
				oldOrientoon.Descricao = newOrientoon.Descricao;
			
			if (newOrientoon.DataLancamento != null && newOrientoon.DataLancamento != oldOrientoon.DataLancamento)
				oldOrientoon.DataLancamento = newOrientoon.DataLancamento;

			if(newOrientoon.Status != null && newOrientoon.Status != oldOrientoon.Status)
				oldOrientoon.Status = newOrientoon.Status;
			
			if (newArtista.nome != oldArtista.nome && newArtista.nome != null)
				oldOrientoon.Artista = newArtista;
			
			if (newAutor.nome != oldAutor.nome && newAutor.nome != null)
				oldOrientoon.Autor = newAutor;

			if (newStatus != null)
				if(newStatus.nome != oldStatus.nome )
                oldOrientoon.Status = newStatus;

			await _orientoonRepository.UpdateAsync(oldOrientoon);
			await _contextRepository.SaveChangesAsync();
			return oldOrientoon.Converter();
		}

		public async Task DeleteAsync(string id)
		{
			if(!(await _orientoonRepository.ExistsByIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

			await _orientoonRepository.DeleteAsync(id);
			await _contextRepository.SaveChangesAsync();
		}

		public async Task<IEnumerable<OrientoonForm>> SearchAsync( int batchSize, int pageNumber, SearchDto? searchDto)
		{
            List<OrientoonModel> orientoonModels = await _orientoonRepository.SearchAsync(batchSize, pageNumber,searchDto);
			return orientoonModels.Select(o => o.Converter()).ToList();
		}

		public async Task AddGeneroAsync(string id, GeneroDto generoDto)
		{
			if(!(await _orientoonRepository.ExistsByIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

			if(!(await _generoService.ExistsByNameAsync(generoDto.Nome)))
				throw new NotFoundException($"Genero: {generoDto.Nome} não encontrado");

			if (await _generoOrientoonRepository.ExistByGeneroIdAsync(id, generoDto.Nome))
				throw new ConflictException($"Genero: {generoDto.Nome} já existe no Orientoon");



			GeneroModel genero = await _generoService.GetByNomeAsync(generoDto.Nome);
			await _generoOrientoonRepository.AddAsync(id, genero.Id);
			await _contextRepository.SaveChangesAsync();
			
		}

		public async Task DeleteGeneroAsync(string id, GeneroDto generoDto)
		{
		    if(!(await _orientoonRepository.ExistsByIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

		    if(!(await _generoService.ExistsByNameAsync(generoDto.Nome)))
				throw new NotFoundException($"Genero: {generoDto.Nome} não encontrado");

			GeneroModel genero = await _generoService.GetByNomeAsync(generoDto.Nome);
			await _generoOrientoonRepository.DeleteAsync(id, genero.Id);
			await _contextRepository.SaveChangesAsync();

		}



        public async Task AddTipoAsync(string id, TipoDto tipoDto)
        {
            if(!(await _orientoonRepository.ExistsByIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");

			if(!(await _tipoService.ExistsByNameAsync(tipoDto.Nome)))
				throw new NotFoundException($"Tipo: {tipoDto.Nome} não encontrado");

			if (await _tipoOrientoonRepository.ExistByTipoIdAsync(id, tipoDto.Nome))
				throw new ConflictException($"Tipo: {tipoDto.Nome} já existe no Orientoon");

			TipoModel tipo = await _tipoService.GetByNomeAsync(tipoDto.Nome);
			await _tipoOrientoonRepository.AddAsync(id, tipo.Id);
			await _contextRepository.SaveChangesAsync();
        }

        public async Task DeleteTipoAsync(string id, TipoDto tipoDto)
        {
            if(!(await _orientoonRepository.ExistsByIdAsync(id)))
				throw new NotFoundException($"Orientoon Id: {id} não encontrado");
			if(!(await _tipoService.ExistsByNameAsync(tipoDto.Nome)))
				throw new NotFoundException($"Tipo: {tipoDto.Nome} não encontrado");

			TipoModel tipo = await _tipoService.GetByNomeAsync(tipoDto.Nome);
			await _tipoOrientoonRepository.DeleteAsync(id, tipo.Id);
			await _contextRepository.SaveChangesAsync();
        }

        public async Task<OrientoonAggregateForm> GetAggregateAsync(string id)
        {
			OrientoonAggregateForm orientoonAggregate = new OrientoonAggregateForm();
            orientoonAggregate.Orientoon = await GetAsync(id);
			orientoonAggregate.CapituloList = await _capituloService.GetCapituloFormsByOrientoonIdAsync(id);
			return orientoonAggregate;
        }

        public async Task<OrientoonForm> GetRandomAsync()
        {
            return (await _orientoonRepository.GetRandomAsync()).Converter();
        }
    }
}
