using System.ComponentModel.DataAnnotations;

namespace MindSprings_.NET_Developer_Test.Models
{
    public class TranslationModel
    {
        [Required(ErrorMessage = "Please enter some text.")]
        [StringLength(1000, ErrorMessage = "The text cannot exceed 1000 characters.")]
        public string InputText { get; set; }

        public string TranslatedText { get; set; }
    }
}
