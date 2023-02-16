namespace AquaPlayground.Models
{
    public class EmailConfigModel
    {
        public string SenderAddress { get; set; }

        public string Password { get; set; }

        public string ResetSubject { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSSL { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public bool IsBodyHTML { get; set; }
    }
}
