﻿namespace Grove.Costs
{
  public class NoCost : Cost
  {
    public override CanPayResult CanPayPartial()
    {
      return true;
    }

    public override void PayPartial(PayCostParameters p) {}
  }
}