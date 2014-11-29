﻿namespace Grove.Costs
{
  public class RemoveCounters : Cost
  {
    private readonly int? _count;
    private readonly CounterType _counterType;

    private RemoveCounters() {}

    public RemoveCounters(CounterType counterType, int? count = null)
    {
      _count = count;
      _counterType = counterType;
    }

    protected override void CanPay(CanPayResult result)
    {
      result.CanPay(() => _count == null || Card.CountersCount(_counterType) >= _count);
    }

    public override void Pay(PayCostParameters p)
    {
      Card.RemoveCounters(_counterType, _count);
    }
  }
}