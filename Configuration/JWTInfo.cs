using System.ComponentModel.DataAnnotations;

namespace CarsShop.Configuration
{
    public class JWTInfo
    {
        [Required]
        public string Key { get; set; }
        
        [Required]
        public string Issuer { get; set; }
        
        [Required]
        public string Audience { get; set; }

        [Range(1, 60)]
        public int ExpiresInMinutes { get; set; }

    }
}
