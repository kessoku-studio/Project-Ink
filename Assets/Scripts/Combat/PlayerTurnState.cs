public class PlayerTurnState : TurnState
{
  public override void EnterState(CombatManager manager)
  {
    RefreshPieces(manager);
  }
  public override void UpdateState(CombatManager manager)
  {
    base.UpdateState(manager);
  }
  public override void ExitState(CombatManager manager)
  {
    base.ExitState(manager);
  }

  private void RefreshPieces(CombatManager manager)
  {
    manager.PlayerPieces.ForEach(piece => piece.RefreshActions());
  }
}