using Microsoft.AspNetCore.Mvc;
using MindSprings_.NET_Developer_Test.Services;
using System.Threading.Tasks;

namespace MindSprings_.NET_Developer_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly TranslationService _translationService;

        public HomeController(TranslationService translationService)
        {
            _translationService = translationService;
        }

        public async Task<IActionResult> Index()
        {
            string translatedText = await _translationService.TranslateToLeetSpeak("Hello");
            ViewBag.TranslatedText = translatedText;
            return View();
        }
    }
}
