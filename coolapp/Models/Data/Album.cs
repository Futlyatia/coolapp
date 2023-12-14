using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace coolapp.Models.Data
{
    public class Album
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите название альбома")]
        [Display(Name = "Название альбома")]
        public string NameAlbum { get; set; }

        [Required(ErrorMessage = "Введите ссылку на обложку")]
        [Display(Name = "Ссылка на обложку")]
        public string Cover { get; set; }

        public DateTime DatePost { get; set; }
        ICollection<EditorsForAlbums> EditorsForAlbums { get; set; }

    }
}
