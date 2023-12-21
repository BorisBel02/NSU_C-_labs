using System.Reflection.Metadata.Ecma335;
using Colloseum.Model.Deck;
using DB.Entity;

namespace DB.Mapper;

public class CardMapper
{
    public static CardEntity MapCardEntity(Card card)
    {
        var entity = new CardEntity
        {
            CardSuit = card.CardSuit,
            CardValue = card.CardValue,
            CardColour = card.CardColour
        };
        
        return entity;
    }

    public static Card MapCard(CardEntity cardEntity)
    {
        return new Card(cardEntity.CardSuit, cardEntity.CardValue);
    }

    public static List<CardEntity> MapRangeCardEntity(IEnumerable<Card> cards)
    {
        var cardEntityList = new List<CardEntity>();
        int index = 0;
        foreach (var card in cards)
        {
            var cardEntity = CardMapper.MapCardEntity(card);
            cardEntity.Index = index;
            cardEntityList.Add(cardEntity);
            ++index;
        }

        return cardEntityList;
    }

    public static Card[] MapRangeCard(IEnumerable<CardEntity> cardEntities)
    {
        var cards = new Card[36];
        int index = 0;
        foreach (var entity in cardEntities)
        {
            cards[index] = MapCard(entity);
            ++index;
        }

        return cards;
    }
}