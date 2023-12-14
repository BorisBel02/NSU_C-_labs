using Colloseum.Model.Deck;
using Colloseum.Model.Fighters;
using Microsoft.Extensions.Hosting;

namespace Colloseum.Model;

public class Experiment : IExperiment
{
   private Deck.IGods _gods;

   private IFighter _elon;
   private IFighter _mark;

   public Experiment(
      IFighter elon,
      IFighter mark,
      IGods gods
      )
   {
      _gods = gods;
      _elon = elon;
      _mark = mark;
   }

   public bool Run()
   {
      _gods.Shuffle();
      
      Array.Copy(_gods.GetDeck(), 0, _elon.FighterCards, 0, _gods.GetDeck().Length / 2);
      Array.Copy(_gods.GetDeck(), _gods.GetDeck().Length / 2, _mark.FighterCards,
         0, _gods.GetDeck().Length / 2);

      return _elon.ChosenCard(_mark.ChooseNumber()).CardColour
             == _mark.ChosenCard(_elon.ChooseNumber()).CardColour;
   }
}