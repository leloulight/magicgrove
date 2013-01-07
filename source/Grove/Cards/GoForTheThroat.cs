﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Cards.Effects;
  using Core.Dsl;
  using Core.Targeting;

  public class GoForTheThroat : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Go for the Throat")
        .ManaCost("{1}{B}")
        .Type("Instant")
        .Text("Destroy target nonartifact creature.")
        .FlavorText("Having flesh is increasingly a liability on Mirrodin.")
        .Effect<DestroyTargetPermanents>()
        .Timing(Timings.InstantRemovalTarget())
        .Category(EffectCategories.Destruction)
        .Targets(
          TargetSelectorAi.Destroy(), 
          Target(
            Validators.Card(card => card.Is().Creature && !card.Is().Artifact), 
            Zones.Battlefield()));
    }
  }
}