using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using MindSprings_.NET_Developer_Test.Models;
using Newtonsoft.Json;

namespace MindSprings_.NET_Developer_Test.Controllers
{
    public class TranslationController : Controller
    {
        private readonly HttpClient _httpClient;

        public TranslationController()
        {
            _httpClient = new HttpClient();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Translate(TranslationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                string translatedText = await TranslateToLeetSpeak(model.InputText);

                model.TranslatedText = translatedText;

                return View("Index", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Translation failed. Please try again later.");
                return View("Index", model);
            }
        }

        public ActionResult Records()
        {
            // Example: Get translations from a database or other source
            var translations = new List<TranslationModel>
            {
                new TranslationModel { InputText = "Hello", TranslatedText = "Bonjour" },
                new TranslationModel { InputText = "Goodbye", TranslatedText = "Au revoir" }
                // Add more translations as needed
            };

            return Json(translations, JsonRequestBehavior.AllowGet);
        }

        private async Task<string> TranslateToLeetSpeak(string inputText)
        {
            // Example: Call translation API
            // Replace this with your actual translation API logic
            string apiUrl = "http://funtranslations.com/api/translate/leetspeak.json?text=" + inputText;
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic responseData = JsonConvert.DeserializeObject(responseContent);
                string translatedText = responseData.contents.translated;
                return translatedText;
            }
            else
            {
                throw new Exception("Translation API request failed: " + response.ReasonPhrase);
            }
        }
    }
}
