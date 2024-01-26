public interface ICombatState
{
  void EnterState(CombatManager manager, int playerTurn);
  void UpdateState(CombatManager manager);
  void ExitState(CombatManager manager);
}