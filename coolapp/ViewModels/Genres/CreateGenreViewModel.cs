using System.ComponentModel.DataAnnotations;

namespace coolapp.ViewModels.Genres
{
    public class CreateGenreViewModel
    {
        [Required(ErrorMessage = "Введите жанр")]
        [Display(Name = "Жанр")]
        public string NameOfGenre { get; set; }
    }
}