using System.ComponentModel.DataAnnotations;

namespace CarsShop.Configuration
{
    public class EmailSettingsConfig
    {
        [Required]
        public string SmtpServer { get; set; } = "localhost";

        [Required]
        public string SmtpUser { get; set; } = "user";

        [Required]
        public string SmtpPassword { get; set; } = "pass";

        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;



    }
}
