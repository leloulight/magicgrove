﻿namespace Grove.Gameplay.Decisions.Playback
{
  using System;
  using Results;

  public class PlaySpellOrAbility : Decisions.PlaySpellOrAbility
  {
    protected override bool ShouldExecuteQuery { get { return true; } }

    protected override void ExecuteQuery()
    {
      Result = (ChosenPlayable) Game.LoadDecisionResult();
    }
  }
}