using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
  [SerializeField] public RangeSO PossibleMoves;
  [SerializeField] public List<Skill> Skills;
  [SerializeField] protected int _hitPoints;


  [SerializeField] private bool _isShadowed;
  private Cell _cellUnderPiece;
  protected int _currentHitPoints;

  public bool HasMoved { get; set; }
  public bool HasAttacked { get; set; }
  public bool IsExhausted
  {
    get
    {
      return HasMoved && HasAttacked;
    }
  }

  public Cell CellUnderPiece
  {
    get => _cellUnderPiece;
    set
    {
      if (_cellUnderPiece != value)
      {
        Cell oldCell = _cellUnderPiece;
        _cellUnderPiece = value;
        if (_cellUnderPiece == null)
        {
          Destroy(this.gameObject); //? This might be changed in the future, right now it is for the removal of the piece from the board
        }
        else
        {
          OnPieceMoved(oldCell, value);
        }
      }
    }
  }

  private void Start()
  {
    Initialize();
  }

  protected virtual void Initialize()
  {
  }

  protected virtual void OnPieceMoved(Cell oldCell, Cell newCell)
  {

    if (oldCell != null)
    {
      oldCell.PieceOnCell = null;
    }

    newCell.PieceOnCell = this;
    newCell.IsShadowed = this._isShadowed;
    BoardManager.Instance.MovePieceToCell(this.gameObject, newCell.gameObject);
  }

  public virtual void RefreshActions()
  {
    this.HasMoved = false;
    this.HasAttacked = false;
  }

  public virtual void ExhaustActions()
  {
    this.HasMoved = true;
    this.HasAttacked = true;
  }
}