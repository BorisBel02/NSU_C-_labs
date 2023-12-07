using Colloseum.Model.Deck;

namespace Colloseum.Model.Fighters;

public interface IFighter
{
    public int ChooseNumber();
    public Card ChosenCard(int number);
}