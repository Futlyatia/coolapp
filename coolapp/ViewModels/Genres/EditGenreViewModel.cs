using System.ComponentModel.DataAnnotations;

namespace coolapp.ViewModels.Genres
{
    public class EditGenreViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите жанр")]
        [Display(Name = "Жанр")]
        public string NameOfGenre { get; set; }
    }
}