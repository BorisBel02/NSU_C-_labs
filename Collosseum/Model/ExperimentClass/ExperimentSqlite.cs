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
    private Repo _repo;
    
    public ExperimentSqlite(
        IGods gods,
        IFighter elon,
        IFighter mark,
        Repo repo)
    {
        _gods = gods;
        _elon = elon;
        _mark = mark;
        _repo = repo;
    }
    public bool Run()
    {
        _gods.Shuffle();
        
        Array.Copy(_gods.GetDeck(), 0, _elon.FighterCards, 0, _gods.GetDeck().Length / 2);
        Array.Copy(_gods.GetDeck(), _gods.GetDeck().Length / 2, _mark.FighterCards,
            0, _gods.GetDeck().Length / 2);
        
        _repo.SaveExperiment(_gods.GetDeck());
        
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