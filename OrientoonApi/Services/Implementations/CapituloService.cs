using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Services.Implementations
{
    public class CapituloService : ICapituloService
    {
        private readonly ICapituloRepository _capituloRepository;
        private readonly IImagemRepository _imagemRepository;
        private readonly IConfiguration _configuration;



        public CapituloService(ICapituloRepository capituloRepository, IImagemRepository imagemRepository, IConfiguration configuration)
        {
            _capituloRepository = capituloRepository;
            _imagemRepository = imagemRepository;
            _configuration = configuration;
        }

        public async Task<CapituloModel> AddCapituloAsync(string mangaId, double numCap, IList<IFormFile> files)
        {
             var uploadPath = _configuration["FileUploadPath"];
            var capitulo = new CapituloModel
            {
                NumCapitulo = numCap,
                Id = mangaId,
                Caminho = Path.Combine(uploadPath, mangaId, numCap.ToString())
            };

            Directory.CreateDirectory(capitulo.Caminho);
            int ordem = 1;
            foreach(var file in files)
            {
                var imagem = new ImagemModel
                {
                    CapituloId = capitulo.Id,
                    Ordem = ordem,
                    Caminho = Path.Combine(capitulo.Caminho, file.FileName)
                };
                ordem++;
                using (var stream = new FileStream(imagem.Caminho, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                await _imagemRepository.AddAsync(imagem);
            }
            return await _capituloRepository.AddAsync(capitulo);

        }

        public async Task<CapituloModel> GetCapituloByIdAsync(string id)
        {
            return await _capituloRepository.GetByIdAsync(id);
        }
    }
}
