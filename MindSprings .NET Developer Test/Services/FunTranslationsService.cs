using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class FunTranslationsService
{
    private readonly HttpClient _httpClient;

    public FunTranslationsService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<string> TranslateToKlingon(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.funtranslations.com/translate/klingon.json");
        request.Content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("text", text)
        });

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to translate text. Status code: {response.StatusCode}");
        }

        var content = await response.Content.ReadAsStringAsync();
        dynamic responseData = JsonConvert.DeserializeObject(content);
        string translatedText = responseData.contents.translated;

        return translatedText;
    }
}
