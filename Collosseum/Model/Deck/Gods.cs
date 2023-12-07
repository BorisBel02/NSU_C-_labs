namespace Colloseum.Model.Deck;

public class Gods
{
    private static Gods? _deckObject;
    private Card[] Cards { get; set; }

    private Gods()
    {
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

    public static Gods GetInstance()
    {
        _deckObject ??= new Gods();

        return _deckObject;
    }

    public Card[] GetDeck()
    {
        return _deckObject.Cards;
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