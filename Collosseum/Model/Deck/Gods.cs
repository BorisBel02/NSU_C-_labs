namespace Colloseum.Model.Deck;

public class Gods : IGods
{
    private Card[] Cards { get; set; }//maybe it would be better to create new class for the Deck

    public Gods()
    {
        //deck initialization
        var deck = new Card[36];
        int num = 0;
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            for (int i = 6; i <= 14; ++i)
            {
                var card = new Card(s, i);
                deck[num] = card;
                ++num;
            }
        }

        Cards = deck;
    }

    public Card[] GetDeck()
    {
        return Cards;
    }

    public void Shuffle()
    {
        var random = new Random();
        for (int i = GetDeck().Length - 1; i > 0; --i)
        {
            int j = random.Next(i + 1);
            if (i != j)
            {
                var tmp = GetDeck()[i];
                GetDeck()[i] = GetDeck()[j];
                GetDeck()[j] = tmp;
            }
        }
    }
}