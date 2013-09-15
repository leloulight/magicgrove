﻿namespace Grove.Artifical.TargetingRules
{
  using System.Collections.Generic;
  using System.Linq;
  using Gameplay;
  using Gameplay.Misc;
  using Gameplay.States;
  using Gameplay.Targeting;

  public class CostSacrificeToReduceToughness : TargetingRule
  {
    private readonly int _amount;

    private CostSacrificeToReduceToughness() {}

    public CostSacrificeToReduceToughness(int amount)
    {
      _amount = amount;
    }

    protected override IEnumerable<Targets> SelectTargets(TargetingRuleParameters p)
    {      
      var costCandidates = new List<Card>();
      var effectCandidates = new List<Card>();
      
      if (IsBeforeYouDeclareAttackers(p.Controller))
      {
        costCandidates = p.Candidates<Card>(selector: s => s.Cost)
          .OrderBy(x => x.Score)
          .Take(1)
          .ToList();

        effectCandidates = p.Candidates<Card>(ControlledBy.Opponent, selector: s => s.Effect)
          .Select(x => new
            {
              Target = x,
              Score = x.Life <= _amount ? x.Score : 0
            })
          .Where(x => x.Score > 0)
          .OrderByDescending(x => x.Score)
          .Select(x => x.Target)
          .Take(1)
          .ToList();
      }            
      else if (!Stack.IsEmpty)
      {
        costCandidates = p.Candidates<Card>(selector: s => s.Cost)
          .Where(x => Stack.CanBeDestroyedByTopSpell(x.Card()))
          .OrderBy(x => x.Score)
          .Take(1)
          .ToList();

        effectCandidates = p.Candidates<Card>(ControlledBy.Opponent, selector: s => s.Effect)
          .Select(x => new
            {
              Target = x,
              Score = x.Life <= _amount ? x.Score : 0
            })
          .Where(x => x.Score > 0)
          .OrderByDescending(x => x.Score)
          .Select(x => x.Target)
          .Take(1)
          .ToList();
      }

      return Group(costCandidates, effectCandidates,
        (t, tgs) => tgs.AddCost(t), (t, tgs) => tgs.AddEffect(t));
    }
  }
}