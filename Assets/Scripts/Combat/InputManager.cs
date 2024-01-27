using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  public delegate void CellSelectedHandler(Cell cell);
  public static event CellSelectedHandler OnCellSelected;

  public static InputManager Instance { get; private set; }

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(gameObject); // Destroy any duplicate instances.
    }
  }

  void Update()
  {
    // Check for actions related to left mouse button
    if (Input.GetMouseButtonDown(0))
    {

      #region Find Closest Cell Under Click
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit[] hits = Physics.RaycastAll(ray);
      RaycastHit closestHit = new RaycastHit();
      float closestDistance = float.MaxValue;

      foreach (var hit in hits)
      {
        if (hit.collider.gameObject.CompareTag("Cell"))
        {
          float distance = Vector3.Distance(Camera.main.transform.position, hit.point);
          if (distance < closestDistance)
          {
            closestDistance = distance;
            closestHit = hit;
          }
        }
      }

      Cell cell = closestHit.collider?.gameObject.GetComponent<Cell>();
      if (cell == null) return;
      #endregion

      switch (CombatManager.Instance.CurrentState)
      {
        case SetupState setupState:
          if (cell.PieceOnCell == null)
          {
            OnCellSelected?.Invoke(cell);
          }
          return;

        case PlayerTurnState playerTurnState:
          return;

        default:
          return;
      }
    }
  }
}