public class InitState : ICombatState
{
  public void EnterState(CombatManager manager)
  {
    // Initialize the game board
    BoardManager.Instance.InitializeBoard();
    // TODO: Should initialize the enemy pieces here so the player can see them before placing their own pieces

    // Init state only occurs once to initialize the board
    // Regardless of how many players are playing
    manager.ChangeState(new SetupState());
  }

  public void ExitState(CombatManager manager) { }

  public void UpdateState(CombatManager manager) { }
}