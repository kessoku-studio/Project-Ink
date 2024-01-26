using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
  [SerializeField] private bool _isShadowed;
  [SerializeField] public RangeSO PossibleMoves;
  [SerializeField] public List<Skill> Skills;

  [SerializeField] protected int _hitPoints;
  protected int _currentHitPoints;

  [SerializeField] private Board _board;
  [SerializeField] private Cell _cellUnderPiece;
  public Cell CellUnderPiece
  {
    get => _cellUnderPiece;
    set
    {
      if (_cellUnderPiece != value)
      {
        Cell oldCell = _cellUnderPiece;
        _cellUnderPiece = value;
        OnPieceMoved(oldCell, value);
      }
    }
  }

  private void Start()
  {
    _currentHitPoints = _hitPoints;
  }

  protected virtual void OnPieceMoved(Cell oldCell, Cell newCell)
  {

    if (oldCell != null)
    {
      oldCell.PieceOnCell = null;
    }

    newCell.PieceOnCell = this;
    newCell.IsShadowed = this._isShadowed;
    _board.MovePieceToCell(this.gameObject, newCell.gameObject);
  }
}