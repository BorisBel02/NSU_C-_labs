using Col.DB;
using Colloseum.Model;
using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using CollosseumTest;
using DB.Entity;
using DB.Mapper;
using Microsoft.EntityFrameworkCore;
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

    [Test]
    public void DataBaseTest()
    {
        using var dbContext = new Context();
        IGods gods = new Gods();
        var experimentCondition = new ExperimentConditionEntity
        {
            Deck = CardMapper.MapRangeCardEntity(gods.GetDeck())
        };

        dbContext.Experiments.Add(experimentCondition);
        dbContext.SaveChanges();

            
        var conditions = dbContext.Experiments
            .Include(experimentConditionEntity => experimentConditionEntity.Deck).ToList();
            
        Assert.That(conditions, Has.Count.EqualTo(1));

        var savedDeck = conditions[0].Deck;
        Assert.That(savedDeck, Has.Count.EqualTo(36));

        for (int i = 0; i < 36; i++)
        {
            var card = gods.GetDeck()[i];
            var savesCard = savedDeck[i];
                
            Assert.Multiple(() =>
            {
                Assert.That(card.CardColour, Is.EqualTo(savesCard.CardColour));
                Assert.That(card.CardValue, Is.EqualTo(savesCard.CardValue));
                Assert.That(card.CardSuit, Is.EqualTo(savesCard.CardSuit));
            });
        }
    }
}