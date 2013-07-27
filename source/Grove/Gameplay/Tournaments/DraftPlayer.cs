﻿namespace Grove.Gameplay.Tournaments
{
  using System.Collections.Generic;

  public class DraftPlayer
  {
    public IDraftingStrategy Strategy;
    public List<CardInfo> Library = new List<CardInfo>();    
  }
}