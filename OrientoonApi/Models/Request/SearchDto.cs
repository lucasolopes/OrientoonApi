using OrientoonApi.Enum;

namespace OrientoonApi.Models.Request
{
    public class SearchDto
    {
        public string? titulo { get; set;}
        public List<string>? genero { get; set;}
        public string? tipo { get; set; }
        public StatusEnum? status { get; set; }
        public string? autorId { get; set; }
        public string? artistaId { get; set; }
        public string? orderBy { get; set; }
    }
}
