using System.Text.Json.Serialization;

namespace Colloseum.Model.Deck;

public record Card
{
    public Suit CardSuit { get; init; }
    public int CardValue { get; init; }
    public Colour CardColour { get; init; }
    
    public Card(Suit suit, int cardValue)
    {
         CardSuit = suit;
         CardValue = cardValue;
         CardColour = CardSuit is Suit.Diamonds or Suit.Hearts
             ? Colour.Red
             : Colour.Black;
    }

    [JsonConstructor]
    public Card(Suit suit, int cardValue, Colour cardColour)
    {
        CardSuit = suit;
        CardValue = cardValue;
        CardColour = cardColour;
    }
}