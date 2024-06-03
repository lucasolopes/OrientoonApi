using OrientoonApi.Models.Response;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;

namespace OrientoonApi.Models.Entities
{
    [Table("Orientoon")]
    public class OrientoonModel
    {

        public OrientoonModel()
        {
        }

        public OrientoonModel(string Titulo, string Descricao, DateTime? DataLancamento, string Status)
        {
           //convert DataLancamento to DateTime
            
            this.Titulo = Titulo;
            this.Descricao = Descricao;
            this.DataLancamento = DataLancamento;
            this.Status = Status;
        }


        [Key]
        [Required]
        public string Id { get; set; }

        // [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [MaxLength(255)]
        [MinLength(1)]
        [Display(Name = "Titulo")]
        //[DataType(DataType.Text)]
        [StringLength(255)]
        [DataType(DataType.Text)]
        [Newtonsoft.Json.JsonProperty("Titulo")]
        // [JsonProperty("titulo")]
        public string Titulo { get; set; }

        [MaxLength(500)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(500)]
        public string CBanner { get; set; }


        [MaxLength(500)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(500)]
        //[JsonProperty("descricao")]
        public string Descricao { get; set; }


        [DataType(DataType.DateTime)]
        //[JsonProperty("DataLancamento")]
        [Display(Name = "DataLançamento")]
        public DateTime? DataLancamento { get; set; }


        public Double Avaliacao { get; set; }


        public ICollection<GeneroOrientoonModel> GeneroOrientoons { get; set; }


        public int ArtistaId { get; set; }

        [JsonIgnore]
        public ArtistaModel Artista { get; set; }


        public int AutorId { get; set; }

        [JsonIgnore]
        public AutorModel Autor { get; set; }


        public ICollection<TipoOrientoonModel> TipoOrientoon { get; set; }


        [MaxLength(50)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(50)]
        //[JsonProperty("Status")]
        public string Status { get; set; }

        public ICollection<CapituloModel> Capitulos { get; set; }

        public OrientoonForm Converter()
        {
            return new OrientoonForm
            {
                Id = this.Id,
                Titulo = this.Titulo,
                Descricao = this.Descricao,
                DataLancamento = this.DataLancamento,
                NomeArtista = this.Artista.NomeArtista,
                NomeAutor = this.Autor.NomeAutor,
                Status = this.Status
            };
        }

    }
}
