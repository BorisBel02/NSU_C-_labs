using Colloseum.Model.Deck;

namespace Colloseum.Model;

public interface IConditionExperiment : IExperiment
{
    public bool RunCondition(Card[] deck);
}