using UnityEngine;
using UnityEngine.AI;

public class InitState : ICombatState
{
  public void EnterState(CombatManager manager, int playerTurn)
  {
    // Initialize the game board
    manager.CurrentBoard.InitializeBoard();
    manager.ChangeState(new SetupState(), 0);
  }

  public void ExitState(CombatManager manager) { }

  public void UpdateState(CombatManager manager) { }
}