using OrientoonApi.Models.Entities;

namespace OrientoonApi.Models.Request
{
    public class ArtistaDto
    {
        public string Nome { get; set; }

        public ArtistaModel Converter()
        {
            return new ArtistaModel
            {
                NomeArtista = this.Nome
            };
        }
    }
}
