using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace coolapp.Models.Data
{
    public class Editor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите имя исполнителя")]
        [Display(Name = "Имя исполнителя")]
        public string NameOfEditor { get; set; }

        ICollection<EditorsForAlbums> EditorsForAlbums { get; set; }

    }
}
