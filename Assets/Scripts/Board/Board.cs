using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
  public GameObject CellPrefab;

  [SerializeField] private int size = 8;
  public List<List<Cell>> CurrentBoard;

  public void InitializeBoard()
  {
    // Create the board structure
    CurrentBoard = new List<List<Cell>>();

    // Calculate the center offset
    float offset = size / 2;
    bool isSizeOdd = size % 2 == 1;

    // Populate the board with cells
    for (int i = 0; i < size; i++)
    {
      List<Cell> row = new List<Cell>();
      for (int j = 0; j < size; j++)
      {
        // Calculate the position of the cell in world space
        Vector3 position = isSizeOdd
            ? new Vector3(-offset + i, 0, offset - j)
            : new Vector3(-offset + 0.5f + i, 0, offset - 0.5f - j);

        // Instantiate the cell in game coordinates
        GameObject cell = Instantiate(CellPrefab, position, Quaternion.identity);
        Cell cellScript = cell.GetComponent<Cell>();

        // Initialize the cell
        cellScript.Initialize(i, j);
        cell.transform.SetParent(this.transform); //? This might not be necessary (putting it in the parent), but only needed for organization purposes
        cell.name = "Cell_" + i + "_" + j;

        // Add the cell to the row
        row.Add(cellScript);
      }
      // Add the row to the board
      CurrentBoard.Add(row);
    }
  }

  public Piece CreatePieceAtCell(GameObject piecePrefab, GameObject targetCell)
  {
    GameObject piece = Instantiate(piecePrefab);

    Piece pieceScript = piece.GetComponent<Piece>();
    Cell cellScript = targetCell.GetComponent<Cell>();

    pieceScript.CellUnderPiece = cellScript;
    return pieceScript;
  }

  public void MovePieceToCell(GameObject piece, GameObject newCell)
  {

    Vector3 piecePosition = CalculatePiecePosition(piece, newCell);
    piece.transform.position = piecePosition;
  }

  private Vector3 CalculatePiecePosition(GameObject piece, GameObject cell)
  {
    Vector3 cellPosition = cell.transform.position;
    float cellHeight = cell.transform.localScale.y;
    float pieceHeight = piece.transform.localScale.y;
    return new Vector3(cellPosition.x, cellPosition.y + cellHeight / 2 + pieceHeight / 2, cellPosition.z);
  }
}
