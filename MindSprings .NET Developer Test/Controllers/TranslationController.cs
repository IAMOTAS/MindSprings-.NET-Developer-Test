using Microsoft.AspNetCore.Mvc;
using MindSprings_.NET_Developer_Test.Models;
using MindSprings_.NET_Developer_Test.Services;
using System;
using System.Threading.Tasks;

namespace MindSprings_.NET_Developer_Test.Controllers
{
    public class TranslationController : Controller
    {
        private readonly FunTranslationsService _klingonTranslationService;
        private readonly TranslationService _translationService;

        public TranslationController(FunTranslationsService klingonTranslationService, TranslationService translationService)
        {
            _klingonTranslationService = klingonTranslationService ?? throw new ArgumentNullException(nameof(klingonTranslationService));
            _translationService = translationService ?? throw new ArgumentNullException(nameof(translationService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TranslateSentence(SentenceModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Translate the sentence to Klingon
                string klingonTranslation = await _klingonTranslationService.TranslateToKlingon(model.InputSentence);

                // Translate the sentence to Leet Speak
                string leetTranslation = await _translationService.TranslateToLeetSpeak(model.InputSentence);

                return Json(new { klingonTranslation, leetTranslation });
            }
            catch (Exception)
            {
                return StatusCode(500, "Translation failed. Please try again later.");
            }
        }
    }
}
