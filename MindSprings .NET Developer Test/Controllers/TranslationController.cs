using Microsoft.AspNetCore.Mvc;
using MindSprings_.NET_Developer_Test.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MindSprings_.NET_Developer_Test.Controllers
{
    public class TranslationController : Controller
    {
        private readonly HttpClient _httpClient;

        public TranslationController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Translate(TranslationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string translatedText = await TranslateToLeetSpeak(model.InputText);

                return Json(new { translatedText });
            }
            catch (Exception )
            {
                return StatusCode(500, "Translation failed. Please try again later.");
            }
        }

        private async Task<string> TranslateToLeetSpeak(string inputText)
        {
            string apiUrl = "https://api.funtranslations.com/translate/klingon.json?text=" + inputText; // Corrected API URL
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
