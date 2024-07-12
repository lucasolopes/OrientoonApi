using OrientoonApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrientoonApi.Models.Response
{
    public class CapituloForm : CapituloInfoForm
    {
        

        public List<ImagemForm> Imagens;
    }
}
