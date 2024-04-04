using System.ComponentModel.DataAnnotations;

namespace MindSprings_.NET_Developer_Test.Models
{
    public class TranslationContents
    {
        public int Id { get; set; }

        [Required]
        public string? OriginalText { get; set; }

        [Required]
        public string? TranslatedText { get; set; }
    }
}
