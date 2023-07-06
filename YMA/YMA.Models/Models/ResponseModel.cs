using Microsoft.AspNetCore.Http;

namespace YMA.Entities.Models
{
    public class ResponseModel
    {
        public int status_code { get; set; } = StatusCodes.Status400BadRequest;

        public string? message { get; set; }

        public string? type { get; set; }

        public dynamic? data { get; set; }
    }
}
