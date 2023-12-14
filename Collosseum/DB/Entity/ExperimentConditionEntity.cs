namespace DB.Entity;

public class ExperimentConditionEntity
{
    public int ExperimentId { get; set; }
    public List<CardEntity> Deck { get; set; }
}