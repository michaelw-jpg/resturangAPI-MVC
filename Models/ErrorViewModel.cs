namespace resturangAPI_MVC.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string? Message { get; set; }
        public string? Path { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
