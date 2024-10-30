using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryApplication.Models
{
    public class KitapTuru
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(25)]
        [Required(ErrorMessage ="Kitap Türü Adı Boş Bırakılamaz!")]
        [DisplayName("Kitap Türü Adı")]
        public string Ad { get; set; }

    }
}
 