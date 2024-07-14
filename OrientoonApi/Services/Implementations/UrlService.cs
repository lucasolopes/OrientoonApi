using OrientoonApi.Services.Interfaces;

namespace OrientoonApi.Services.Implementations
{
    public class UrlService : IUrlService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string getBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            return $"{request.Scheme}://{request.Host}";
        }

        public string getImagesBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            return $"{request.Scheme}://{request.Host}/imagens/";
        }

    }
}
