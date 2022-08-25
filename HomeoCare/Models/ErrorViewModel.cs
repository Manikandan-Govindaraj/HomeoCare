using System;

namespace HomeoCare.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool IsException { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
