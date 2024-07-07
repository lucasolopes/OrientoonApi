using OrientoonApi.Data.Repositories.Implementations;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;
using OrientoonApi.Models.Response;
using OrientoonApi.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace OrientoonApi.Services.Implementations
{
    public class CapituloService : ICapituloService
    {
        private readonly ICapituloRepository _capituloRepository;
        private readonly IImagemRepository _imagemRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public CapituloService(ICapituloRepository capituloRepository, IImagemRepository imagemRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _capituloRepository = capituloRepository;
            _imagemRepository = imagemRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CapituloForm> AddCapituloAsync(string orientoonId, double numCap, IList<IFormFile> files)
        {
            var request = _httpContextAccessor.HttpContext.Request;

            var host = $"{request.Scheme}://{request.Host}/imagens";

            var uploadPath = _configuration["FileUploadPath"];
            var capitulo = new CapituloModel
            {
                OrientoonId = orientoonId,
                NumCapitulo = numCap,
                Caminho = Path.Combine( orientoonId, numCap.ToString()),
            };

            int ordem = 1;

            foreach (var file in files)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".webp")
                {
                    await SaveImageCap(file, Path.Combine(uploadPath, capitulo.Caminho));

                    ImagemModel imagemModel = new ImagemModel
                    {
                        NomeArquivo = file.FileName,
                        Ordem = ordem,
                        CapituloId = capitulo.Id,
                        Caminho = Path.Combine(capitulo.Caminho, Path.GetFileName(file.FileName))
                    };

                    await _imagemRepository.AddAsync(imagemModel);

                    ordem++;
                }

            }
             

            return await _capituloRepository.AddAsync(capitulo);
        }

        private async Task SaveImageCap(IFormFile image,string diretorio)
        {
           
            var imageDirectory = Path.Combine(diretorio);
            if (!Directory.Exists(imageDirectory))
                Directory.CreateDirectory(imageDirectory);

            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(imageDirectory, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

        }

        public async Task<CapituloForm> GetCapituloByIdAsync(string id)
        {
            return await _capituloRepository.GetByIdAsync(id);
        }

    }
}
