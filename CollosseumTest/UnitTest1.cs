using Colloseum.Model;
using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using Moq;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CardColoursTest()
    {
        IGods gods = new Gods();

        Card[] deck = gods.GetDeck();

        int red = 0;
        int black = 0;
        
        foreach(var card in deck)
        {
            if (card.CardColour == Colour.Black)
            {
                ++black;
            }
            else
            {
                ++red;
            }
        }
        Assert.True((black == 18) && (red == 18));
    }

    [Test]
    public void CardValuesTest()
    {
        IGods gods = new Gods();

        Card[] deck = gods.GetDeck();

        int[] valuesQty = new int[9];

        foreach (var card in deck)
        {
            ++valuesQty[card.CardValue - 6];
        }

        foreach (var value in valuesQty)
        {
            Assert.AreEqual(value, 4);
        }
    }

    [Test]
    public void DummyStrategyTest()
    {
        IGods gods = new Gods();
        gods.Shuffle();
        var deck = gods.GetDeck();
        
        var fighter1 =
            Mock.Of<IFighter>(d => d.ChooseNumber() == 1 &&
                                   d.ChosenCard(It.IsAny<int>()) == deck[1]);
    }

    [Test]
    public void ExperimentTest()
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
        var godsMock = new Mock<IGods>();
        godsMock.Setup(g => g.GetDeck()).Returns(deck);//is it really as clumsy as i see rn? 
        var elon = new Fighter();
        var mark = new Fighter();

        var experiment = new Experiment(elon, mark, godsMock.Object);
        experiment.Run();
        
        godsMock.Verify(s => s.Shuffle(), Times.Once);
    }
}