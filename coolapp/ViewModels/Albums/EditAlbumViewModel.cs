using System.ComponentModel.DataAnnotations;

namespace coolapp.ViewModels.Albums
{
    public class EditAlbumViewModel
    {
        public short Id { get; set; }
        [Required(ErrorMessage = "Введите название альбома")]
        [Display(Name = "Название альбома")]
        public string NameAlbum { get; set; }

        [Required(ErrorMessage = "Введите ссылку на обложку")]
        [Display(Name = "Ссылка на обложку")]
        public string Cover { get; set; }
    }
}
