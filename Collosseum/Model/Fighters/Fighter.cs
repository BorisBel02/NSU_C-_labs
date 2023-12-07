using Colloseum.Model.Deck;

namespace Colloseum.Model.Fighters;

public class Fighter : IFighter
{
    public Card[] FighterCards{ get; set; }

    public Fighter(Card[] cards)
    {
        FighterCards = cards;
    }

    public Fighter()
    {
        FighterCards = new Card[18];
    }

    public int ChooseNumber()
    {
        Card chosenCard = FighterCards[0];
        return chosenCard.CardValue;
    }

    public Card ChosenCard(int number)
    {
        return FighterCards[number];
    }
}