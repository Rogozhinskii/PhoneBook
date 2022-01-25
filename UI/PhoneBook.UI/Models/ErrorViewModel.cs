using System;

namespace PhoneBook.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Path { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public int StatusCode { get; set; }
    }
}
