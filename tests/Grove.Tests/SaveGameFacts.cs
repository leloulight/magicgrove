﻿namespace Grove.Tests
{
  using Artifical;
  using Gameplay;
  using Gameplay.Misc;
  using Infrastructure;
  using Xunit;

  public class SaveGameFacts : Scenario
  {
    [Fact]
    public void Save()
    {
      var game = SimulateGame();
      var savedGame = game.Save();

      var game1 = GameFactory.Create(GameParameters.Load(
        player1Controller: ControllerType.Machine,
        player2Controller: ControllerType.Machine,
        savedGame: savedGame));
            
      // 2 games will be equal only if game is in exact same state      
      // load game only loads the game up to the last decision recorded
      // resume the game until state count is equal
      game1.Simulate(() => game1.Turn.StateCount < game.Turn.StateCount);      
      
      // hash depends on card visibility, visibility depends
      // on who the searching player is.      
      game.Players.Searching = game.Players.Player1;
      game1.Players.Searching = game1.Players.Player1;

      Assert.Equal(game.CalculateHash(), game1.CalculateHash());
    }

    private Game SimulateGame()
    {
      var p = GameParameters.Simulation(GetDeck("deck1.dec"), GetDeck("deck2.dec"),
        new SearchParameters(maxSearchDepth: 15, maxTargetCount: 2, enableMultithreading: true));

      var game = GameFactory.Create(p);

      game.Start(numOfTurns: 5);
      return game;
    }
  }
}