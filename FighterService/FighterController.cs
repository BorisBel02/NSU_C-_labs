using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using DB.Mapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FighterService;

[ApiController]
[Route("[controller]")]
public class FighterController : ControllerBase
{
    
    [HttpPost("/cards")]
    public IActionResult ChooseCard([FromBody] List<CardDto> cards)
    {
        Console.WriteLine($"cards qty={cards.Count}");
        var fighter = new Fighter(CardMapper.MapRangeCardFromDto(cards));
        return Ok(fighter.ChooseNumber());
    }

    [HttpGet("/api")]
    public IActionResult Get()
    {
        return new JsonResult(new Gods().GetDeck());
    }
}