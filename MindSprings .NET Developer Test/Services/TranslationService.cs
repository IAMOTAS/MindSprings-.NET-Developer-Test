using MindSprings_.NET_Developer_Test.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MindSprings_.NET_Developer_Test.Services
{
    public class TranslationService
    {
        private readonly HttpClient _httpClient;

        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> TranslateToLeetSpeak(string text)
        {
            Debug.WriteLine("Text inputted = " + text);
            
            var response = await _httpClient.GetFromJsonAsync<TranslationResponse>($"https://api.funtranslations.com/translate/leetspeak.json?text={text}");

            // Check if the response and its contents are not null
            if (response != null && response.Contents != null)
            {
                return response?.Contents?.TranslatedText ?? "Sidney";
            }
            else
            {
                // Handle the case where the response or its contents are null
                return "Translation failed"; // Or throw an exception, log an error, etc.
            }
        }
    }
}
