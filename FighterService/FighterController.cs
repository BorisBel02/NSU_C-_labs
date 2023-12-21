using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FighterService;

[ApiController]
[Route("[controller]")]
public class FighterController : ControllerBase
{
    
    [HttpPost("/cards")]
    public IActionResult ChooseCard([FromBody] Card[] cards)
    {
        var fighter = new Fighter(cards);
        Console.WriteLine(cards);
        return Ok(fighter.ChooseNumber());
    }

    [HttpGet("/api")]
    public IActionResult Get()
    {
        return new JsonResult("Application is running");
    }
}