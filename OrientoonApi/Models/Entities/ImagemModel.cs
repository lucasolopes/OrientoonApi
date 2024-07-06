namespace OrientoonApi.Models.Entities
{
    public class ImagemModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NomeArquivo { get; set; }
        public int Ordem { get; set; }
        public string CapituloId { get; set; }
        public CapituloModel Capitulo { get; set; }
    }
}
