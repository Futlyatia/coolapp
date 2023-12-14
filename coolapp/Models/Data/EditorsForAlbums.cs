using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace coolapp.Models.Data
{
    public class EditorsForAlbums
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }
        [Required]
        [Display(Name = "Исполнитель")]
        public short IdEditor { get; set; }
        [Required]
        [Display(Name = "Альбом")]
        public short IdAlbum { get; set; }

        [Display(Name = "Исполнитель")]
        [ForeignKey("IdEditor")]
        public Editor Editor { get; set; }

        [Display(Name = "Альбом")]
        [ForeignKey("IdAlbum")]
        public Album Album { get; set; }

    }
}
