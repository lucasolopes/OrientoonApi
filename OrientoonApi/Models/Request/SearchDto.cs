namespace OrientoonApi.Models.Request
{
    public class SearchDto
    {
        public string? titulo { get; set;}
        public List<string>? genero { get; set;}
        public string? tipo { get; set; }
        public string? status { get; set; }
        public string? autor { get; set; }
        public string? artista { get; set; }
        public string? orderBy { get; set; }
    }
}
