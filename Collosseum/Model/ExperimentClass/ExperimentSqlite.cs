using Col.DB;
using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using DB;
using DB.Entity;
using DB.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Colloseum.Model;

public class ExperimentSqlite : IConditionExperiment
{
    private IGods _gods;
    private IFighter _elon;
    private IFighter _mark;
    private Context _db;
    
    public ExperimentSqlite(
        IGods gods,
        IFighter elon,
        IFighter mark,
        Context db)
    {
        _gods = gods;
        _elon = elon;
        _mark = mark;
        _db = db;
    }
    public bool Run()
    {
        _gods.Shuffle();
        
        Array.Copy(_gods.GetDeck(), 0, _elon.FighterCards, 0, _gods.GetDeck().Length / 2);
        Array.Copy(_gods.GetDeck(), _gods.GetDeck().Length / 2, _mark.FighterCards,
            0, _gods.GetDeck().Length / 2);
        //well, maybe i could just use ArrayList instead of simple array...

        var cardEntityList = CardMapper.MapRangeCardEntity(_gods.GetDeck());

        var experimentEntity = new ExperimentConditionEntity
        {
            Deck = cardEntityList
        };
        
        _db.Add(experimentEntity);
        
        return _elon.ChosenCard(_mark.ChooseNumber()).CardColour
               == _mark.ChosenCard(_elon.ChooseNumber()).CardColour;
    }

    public bool RunCondition(Card[] deck)
    {
        Array.Copy(deck, 0, _elon.FighterCards, 0, deck.Length / 2);
        Array.Copy(deck, deck.Length / 2, _mark.FighterCards,
            0, deck.Length / 2);
        
        return _elon.ChosenCard(_mark.ChooseNumber()).CardColour
               == _mark.ChosenCard(_elon.ChooseNumber()).CardColour;
    }
}