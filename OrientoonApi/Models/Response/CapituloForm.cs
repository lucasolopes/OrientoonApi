using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Response
{
    public class CapituloForm
    {
        public string Id;

        public Double NumCapitulo;

        public DateOnly DataLancamento;

        public Double AvaliacaoCap;

        public string OrientoonId;

        public List<ImagemForm> Imagens;
    }
}
