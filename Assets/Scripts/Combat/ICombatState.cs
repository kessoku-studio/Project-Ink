using System.Collections.Generic;

public interface ICombatState
{
  void EnterState(CombatManager manager);
  void UpdateState(CombatManager manager);
  void ExitState(CombatManager manager);
}

public abstract class TurnState : ICombatState
{
  protected List<ICombatState> _subStates;
  protected ICombatState _currentSubstate;

  public virtual void EnterState(CombatManager manager) { }
  public virtual void UpdateState(CombatManager manager) { }
  public virtual void ExitState(CombatManager manager) { }
}