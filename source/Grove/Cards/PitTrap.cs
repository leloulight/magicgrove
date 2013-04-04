﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai.TargetingRules;
  using Core.Ai.TimingRules;
  using Core.Costs;
  using Core.Dsl;
  using Core.Effects;
  using Core.Mana;

  public class PitTrap : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Pit Trap")
        .ManaCost("{2}")
        .Type("Artifact")
        .Text("{2},{T}, Sacrifice Pit Trap: Destroy target attacking creature without flying. It can't be regenerated.")
        .FlavorText("Yotian soldiers were designed to fight, not watch their feet.")
        .Cast(p => p.TimingRule(new FirstMain()))
        .ActivatedAbility(p =>
          {
            p.Text =
              "{2},{T}, Sacrifice Pit Trap: Destroy target attacking creature without flying. It can't be regenerated.";

            p.Cost = new AggregateCost(
              new PayMana(2.Colorless(), ManaUsage.Abilities),
              new Tap(),
              new Sacrifice());

            p.Effect = () => new DestroyTargetPermanents(canRegenerate: false);

            p.TargetSelector.AddEffect(trg => trg.Is
              .Card(c => c.Is().Creature && c.IsAttacker)
              .On.Battlefield());

            p.TimingRule(new Steps(activeTurn: false, passiveTurn: true, steps: Step.DeclareAttackers));
            p.TargetingRule(new Destroy());            
            p.TimingRule(new TargetRemoval());
          });
    }
  }
}