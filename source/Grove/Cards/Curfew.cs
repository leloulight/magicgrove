﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Cards.Effects;
  using Core.Dsl;

  public class Curfew : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Curfew")
        .ManaCost("{U}")
        .Type("Instant")
        .Text("Each player returns a creature he or she controls to its owner's hand.")
        .FlavorText(". . . But I'm not tired'")
        .Cast(p =>
          {
            p.Timing = Timings.InstantRemovalPlayerChooses(1);
            p.Effect = Effect<EachPlayerReturnsCreaturesToHand>(e => e.Count = 1);
          });
    }
  }
}