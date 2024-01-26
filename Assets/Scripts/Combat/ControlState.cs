using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using UnityEngine;

public class ControlState : ICombatState
{
  private int _numberOfPlayers;

  private int _playerTurn;

  private List<Cell> _cellsWithPiece;
  private List<bool> _hasMoved;

  private Piece _selectedPiece;
  private int _selectedIndex;

  public void EnterState(CombatManager manager, int playerTurn)
  {
    // Get the number of players
    _numberOfPlayers = manager.Players.Count;

    // Set the player turn
    _playerTurn = playerTurn;

    _cellsWithPiece = manager.ActivePieces[_playerTurn].Select(piece => piece.CellUnderPiece).ToList();



    // Set the list of pieces to track if they've moved
    _hasMoved = Enumerable.Repeat(false, manager.Players[_playerTurn].Pieces.Count).ToList();
  }

  public void ExitState(CombatManager manager)
  {
  }

  public void UpdateState(CombatManager manager)
  {
    if (_hasMoved.All(x => x == true))
    {
      if (_playerTurn != _numberOfPlayers - 1)
      {
        manager.ChangeState(new ControlState(), _playerTurn + 1);
      }
      else
      { //! Currently it is looping back to the first player
        manager.ChangeState(new ControlState(), 0);
      }
    }
    if (Input.GetMouseButtonDown(0))
    {
      RaycastHit hitCell = manager.GetHitCell(Input.mousePosition);
      if (!_selectedPiece)
      {
        if (hitCell.collider != null)
        {
          Debug.Log("Selecting");
          Cell cellScript = hitCell.collider.gameObject.GetComponent<Cell>();
          if (cellScript.PieceOnCell == null)
          {
            return;
          }

          for (int i = 0; i < _cellsWithPiece.Count; i++)
          {
            if (cellScript == _cellsWithPiece[i] && _hasMoved[i] == false)
            {
              _selectedPiece = cellScript.PieceOnCell;
              _selectedIndex = i;
              Debug.Log(_selectedPiece);
              break;
            }
          }

        }
      }
      else
      {
        if (hitCell.collider != null)
        {
          Debug.Log("hitCell");
          Cell cellScript = hitCell.collider.gameObject.GetComponent<Cell>();
          if (cellScript.PieceOnCell == null)
          {
            _selectedPiece.CellUnderPiece = cellScript;
            _selectedPiece = null;
            _hasMoved[_selectedIndex] = true;
          }
        }
      }
    }
  }
}