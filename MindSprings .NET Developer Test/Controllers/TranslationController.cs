using System;
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
                // If model validation fails, return to the view with validation errors
                return View("Index", model);
            }

            try
            {
                // Call translation API
                string translatedText = await TranslateToLeetSpeak(model.InputText);

                // Set translated text to model
                model.TranslatedText = translatedText;

                // Display the result
                return View("Index", model);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during translation
                ModelState.AddModelError("", "Translation failed. Please try again later.");
                return View("Index", model);
            }
        }

        private async Task<string> TranslateToLeetSpeak(string inputText)
        {
            // You would replace this URL with the actual API endpoint for translation
            string apiUrl = "http://funtranslations.com/api/translate/leetspeak.json?text=" + inputText;

            // Send request to translation API
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            // Check if request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize JSON response to get translated text
                dynamic responseData = JsonConvert.DeserializeObject(responseContent);
                string translatedText = responseData.contents.translated;

                return translatedText;
            }
            else
            {
                // If request fails, throw an exception
                throw new Exception("Translation API request failed: " + response.ReasonPhrase);
            }
        }
    }
}
