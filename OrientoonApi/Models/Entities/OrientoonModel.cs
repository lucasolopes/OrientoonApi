using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrientoonApi.Models.Request;
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

        public OrientoonModel(OrientoonDto orientoonDto)
        {
           //convert DataLancamento to DateTime
            
            this.nome =  orientoonDto.Titulo;
            this.Descricao = orientoonDto.Descricao;
            this.DataLancamento = orientoonDto.DataLancamento;
        }

        public OrientoonModel(OrientoonPutDto orientoonPutDto)
        {
            this.nome = orientoonPutDto.Titulo;
            this.Descricao = orientoonPutDto.Descricao;
            this.DataLancamento = orientoonPutDto.DataLancamento;
        }


        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [MaxLength(255)]
        [MinLength(1)]
        [StringLength(255)]
        [DataType(DataType.Text)]
        [Column("nome")]
        public string nome { get; set; }

        //[NotMapped]
        [JsonIgnore]
        [Newtonsoft.Json.JsonProperty("NormalizedTitulo")]
        [MaxLength(255)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(255)]
        public string NormalizedName { get; set; }

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


        public string ArtistaId { get; set; }

        public ArtistaModel Artista { get; set; }


        public string AutorId { get; set; }

        public AutorModel Autor { get; set; }

        [Newtonsoft.Json.JsonProperty("AdultContent")]
        public bool AdultContent { get; set; }

        public ICollection<TipoOrientoonModel> TipoOrientoon { get; set; }


        [MaxLength(50)]
        [MinLength(1)]
        [DataType(DataType.Text)]
        [StringLength(50)]
        //[JsonProperty("Status")]
        public string StatusId { get; set; }

        public StatusModel Status { get; set; }

        public ICollection<CapituloModel> Capitulos { get; set; }

        public OrientoonForm Converter()
        {
            return new OrientoonForm
            {
                Id = this.Id,
                Titulo = this.nome,
                Banner = this.CBanner,
                Descricao = this.Descricao,
                DataLancamento = this.DataLancamento,
                NomeArtista = this.Artista.nome,
                NomeAutor = this.Autor.nome,
                Status = this.Status.nome,
                AdultContent = this.AdultContent
            };
        }

    }
}
