using System.ComponentModel.DataAnnotations;
namespace MelMusic.Models
{
    public class Mizika
    {
        [Key]
        public int id { get; set; }
        public string isim { get; set; } = "";
        public int fiyat { get; set; }
        public string aciklama { get; set; }="";
        public string resim { get; set; }="";
    }
}
