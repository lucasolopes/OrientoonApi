using OrientoonApi.Enum;

namespace OrientoonApi.Models.Response
{
    public class StatusForm
    {
        public string Id { get; set; }
        public StatusEnum Status { get; set; }
    }
}
