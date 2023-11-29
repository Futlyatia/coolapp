using System.ComponentModel.DataAnnotations;

namespace coolapp.ViewModels.Editors
{
    public class EditEditorViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите имя исполнителя")]
        [Display(Name = "Имя исполнителя")]
        public string NameOfEditor { get; set; }
    }
}