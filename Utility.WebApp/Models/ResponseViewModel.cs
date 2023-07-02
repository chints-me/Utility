namespace Utility.WebApp.Models
{
    public class ResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? ValidationErrors { get; set; }
        public string? RedirectUrl { get; set; }
        public bool IsRedirectionRequired { get; set; }
        public string? Data { get; set; }
    }
}
