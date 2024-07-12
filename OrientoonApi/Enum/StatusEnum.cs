

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OrientoonApi.Enum
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        [EnumMember(Value = "EmAndamento")]
        EmAndamento,
        [EnumMember(Value = "Concluido")]
        Concluido,
        [EnumMember(Value = "Cancelado")]
        Cancelado,
        [EnumMember(Value = "Hiato")]
        Hiato
    }
}
