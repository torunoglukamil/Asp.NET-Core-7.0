namespace YMA.Models.Models
{
    public class ResponseModel
    {
        public int status_code { get; set; }

        public string? message { get; set; }

        public string? type { get; set; }

        public dynamic? data { get; set; }
    }
}
