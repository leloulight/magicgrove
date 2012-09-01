﻿namespace Grove.Core.Controllers
{
  using System;
  using System.Linq;
  using Results;

  public abstract class ReturnCardsFromGraveyardToBattlefield : Decision<ChosenCards>
  {
    public Func<Card, bool> Filter = delegate { return true; };
    public int Count { get; set; }
    public string Text { get; set; }

    protected override bool ShouldExecuteQuery { get { return Controller.Graveyard.Where(Filter).Count() > Count; } }

    public override void ProcessResults()
    {
      if (ShouldExecuteQuery == false)
      {
        ReturnAll();
        return;
      }

      foreach (var permanent in Result)
      {
        permanent.PutToBattlefield();
      }
    }

    private void ReturnAll()
    {
      foreach (var card in Controller.Graveyard.Where(Filter).ToList())
      {
        card.PutToBattlefield();
      }
    }
  }
}