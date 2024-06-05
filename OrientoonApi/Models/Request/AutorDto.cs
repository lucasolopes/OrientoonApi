using OrientoonApi.Models.Entities;

namespace OrientoonApi.Models.Request
{
    public class AutorDto
    {
        public string Nome { get; set; }

        public AutorModel Converter()
        {
            return new AutorModel
            {
                NomeAutor = this.Nome
            };
        }
    }
}
