using Microsoft.AspNetCore.Mvc;
using MindSprings_.NET_Developer_Test.Models;

public class TranslationController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new TranslationModel());
    }

    [HttpPost]
    public IActionResult ConvertToLeet(TranslationModel model)
    {
        if (ModelState.IsValid)
        {
            model.TranslatedText = ConvertToLeetSpeak(model.InputText);
            return View("Index", model);
        }
        return View("Index");
    }

    private string ConvertToLeetSpeak(string input)
    {
        // Your conversion logic goes here
        // Example: Convert "e" to "3", "a" to "4", etc.
        return input.Replace("e", "3").Replace("a", "4").Replace("t", "7").Replace("l", "1").Replace("o", "0");
    }
}

