using System.ComponentModel.DataAnnotations;

namespace coolapp.ViewModels.EditorsForAlbums
{
    public class EditEditorsForAlbumsViewModel
    {
        public short Id { get; set; }
        [Required]
        [Display(Name = "Исполнитель")]
        public short IdEditor { get; set; }
        [Required]
        [Display(Name = "Альбом")]
        public short IdAlbum { get; set; }
    }
}
