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

    public static CardDto MapCardDto(Card card)
    {
        return new CardDto
        {
            CardSuit = card.CardSuit,
            CardValue = card.CardValue
        };
    }

    public static List<CardDto> MapRangeCardDto(IEnumerable<Card> cards)
    {
        return cards.Select(MapCardDto).ToList();
    }

    public static Card MapCardFromDto(CardDto dto)
    {
        return new Card(dto.CardSuit, dto.CardValue);
    }

    public static Card[] MapRangeCardFromDto(List<CardDto> dtos)
    {
        var cards = new Card[dtos.Count];

        for (int i = 0; i < dtos.Count; i++)
        {
            cards[i] = new Card(dtos[i].CardSuit, dtos[i].CardValue);
        }

        return cards;
    }
}