using Colloseum.Model.Deck;
using DB.Entity;
using DB.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Col.DB;

public class Repo
{
    private Context _db;

    public Repo(Context db)
    {
        _db = db;
    }
    public void SaveExperiment(Card[] cards)
    {
        var experimentConditionEntity = new ExperimentConditionEntity
        {
            Deck = CardMapper.MapRangeCardEntity(cards)
        };

        /*var entity = */_db.Add(experimentConditionEntity);
        //Console.WriteLine($"writing entity with id = {entity.Entity.ExperimentId}");
        _db.SaveChanges();
    }

    public Card[] GetCondition(int num)
    {
        try
        {
            var condition = _db.Experiments
                .Include(experimentConditionEntity => experimentConditionEntity.Deck)
                .First(e => e.ExperimentId == num);
            return CardMapper.MapRangeCard(condition.Deck);
        }
        catch (Exception e)
        {
            Console.WriteLine("no experiment with id: " + num);
        }

        return null;
    }

    
}