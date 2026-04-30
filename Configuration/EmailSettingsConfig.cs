using System.ComponentModel.DataAnnotations;

namespace CarsShop.Configuration
{
    public class EmailSettingsConfig
    {
        [Required]
        public string SmtpServer { get; set; } = "smtp.gmail.com";

        [Required]
        public int SmtpPort { get; set; } = 587;   // ✅ fixed name

        [Required]
        public string SmtpUser { get; set; } = "";

        [Required]
        public string SmtpPass { get; set; } = ""; // ✅ fixed name

        public bool EnableSsl { get; set; } = true;
    }
}