using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetupState : ICombatState
{
  private CombatManager _combatManager;
  private List<GameObject> _piecesToPlace;

  private int _piecesToPlaceIndex = 0;

  public void EnterState(CombatManager manager)
  {
    _combatManager = manager;
    // Set the pieces to place
    _piecesToPlace = _combatManager.PlayerSetup.Pieces;
    _piecesToPlaceIndex = 0;

    InputManager.OnCellSelected += HandleCellSelected;
  }

  public void ExitState(CombatManager manager)
  {
    InputManager.OnCellSelected -= HandleCellSelected;
  }

  public void UpdateState(CombatManager manager)
  {
    // If all the pieces have been placed, go to the next state
    if (_piecesToPlaceIndex >= _piecesToPlace.Count)
    {
      // TODO: The player should have the option to tweak the placement of the pieces before finalizing the placement with a button
      //? Now the placement is locked in as soon as all the pieces are placed
      _combatManager.ChangeState(new PlayerTurnState());
    }
  }

  private void HandleCellSelected(Cell cell)
  {
    Piece pieceToPlace = _piecesToPlace[_piecesToPlaceIndex].GetComponent<Piece>();
    ICommand command = new PlacePieceCommand(pieceToPlace, cell);
    _combatManager.Invoker.SetCommand(command);
    _combatManager.Invoker.ExecuteCommand();
    _combatManager.PlayerPieces.Add(pieceToPlace);
    _piecesToPlaceIndex++;
  }
}