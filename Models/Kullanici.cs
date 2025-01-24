using System.ComponentModel.DataAnnotations;
namespace MelMusic.Models
{
    public class Kullanici
    {
        [Key]
        public int id { get; set; }
        public string isim { get; set; } = "";
        public string mail { get; set; } = "";
        public string parola { get; set; } = "";
        public string rol { get; set; } = "User";
    }
}
