using OrientoonApi.Models.Entities;

namespace OrientoonApi.Models.Request
{
    public class StatusDto
    {
        public string Status { get; set; }
        public StatusModel Converter()
        {
            return new StatusModel
            {
                Status = this.Status
            };
        }
    }
}
