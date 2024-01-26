using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetupState : ICombatState
{
  private List<GameObject> _piecesToPlace;

  private int _piecesToPlaceIndex = 0;

  private int _numberOfPlayers;

  private int _playerTurn;

  public void EnterState(CombatManager manager, int playerTurn)
  {
    // Get the number of players
    _numberOfPlayers = manager.Players.Count;

    // Set the player turn
    _playerTurn = playerTurn;

    // Set the pieces to place
    _piecesToPlace = manager.Players[_playerTurn].Pieces;

    _piecesToPlaceIndex = 0;

    manager.ActivePieces.Add(new List<Piece>());
  }

  public void ExitState(CombatManager manager) { }

  public void UpdateState(CombatManager manager)
  {
    if (_piecesToPlaceIndex >= _piecesToPlace.Count)
    {
      if (_playerTurn != _numberOfPlayers - 1)
      {
        manager.ChangeState(new SetupState(), _playerTurn + 1);
      }
      else
      {
        manager.ChangeState(new ControlState(), 0);
      }
    }
    if (Input.GetMouseButtonDown(0))
    {
      RaycastHit hitCell = manager.GetHitCell(Input.mousePosition);

      // Check if a valid "Cell" was hit
      if (hitCell.collider != null)
      {
        GameObject cell = hitCell.collider.gameObject;
        if (cell.GetComponent<Cell>().PieceOnCell != null)
        {
          return; // Exit if there's already a piece on the cell
        }
        GameObject piece = _piecesToPlace[_piecesToPlaceIndex];
        Piece pieceScript = manager.CurrentBoard.CreatePieceAtCell(piece, cell);
        manager.ActivePieces[_playerTurn].Add(pieceScript);
        _piecesToPlaceIndex++;
      }
    }
  }
}