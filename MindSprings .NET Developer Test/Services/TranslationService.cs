using MindSprings_.NET_Developer_Test.Models;
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
            var response = await _httpClient.GetFromJsonAsync<TranslationResponse>($"https://api.funtranslations.com/translate/leetspeak.json?text={text}");
            return response?.Contents.Translated;
        }
    }
}
