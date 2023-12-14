using Colloseum.Model.Deck;

namespace DB.Entity;

public class CardEntity
{
    public int CardId { get; set; }
    
    public Suit CardSuit { get; set; }
    
    public int CardValue { get; set; }
    
    public Colour CardColour { get; set; }
    public int Index { get; set; }
    public ExperimentConditionEntity ExperimentCondition {get; set; }
}