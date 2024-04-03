using System.ComponentModel.DataAnnotations;

namespace MindSprings_.NET_Developer_Test.Controllers
{
    public class SentenceModel
    {
        [Required]
        public string InputSentence { get; set; }
    }
}
