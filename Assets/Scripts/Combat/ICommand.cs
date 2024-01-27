using Unity.VisualScripting;
using UnityEngine;

public interface ICommand
{
  void Execute();
  void Undo();
}

public class PlacePieceCommand : ICommand
{
  private readonly Piece _piece;
  private readonly Cell _cell;

  public PlacePieceCommand(Piece piece, Cell cell)
  {
    _piece = piece;
    _cell = cell;
  }

  public void Execute()
  {
    BoardManager.Instance.CreatePieceAtCell(_piece.gameObject, _cell.gameObject);
  }

  public void Undo()
  {
    _piece.CellUnderPiece = null;
  }
}