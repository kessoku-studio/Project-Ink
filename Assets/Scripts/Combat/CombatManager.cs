using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
  public GameObject BoardReference;
  public Board CurrentBoard { get; private set; }
  public List<PlayerSetup> Players;

  public List<List<Piece>> ActivePieces;

  private ICombatState _currentState;

  private void Start()
  {
    CurrentBoard = BoardReference.GetComponent<Board>();
    ActivePieces = new List<List<Piece>>();
    ChangeState(new InitState(), 0);
  }

  private void Update()
  {
    _currentState.UpdateState(this);
  }

  public void ChangeState(ICombatState newState, int playerTurn)
  {
    _currentState?.ExitState(this);
    _currentState = newState;
    _currentState.EnterState(this, playerTurn);
  }

  public RaycastHit GetHitCell(Vector3 mousePosition)
  {
    // Convert the mouse position to a ray
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    // Perform the raycast and get all hits
    RaycastHit[] hits = Physics.RaycastAll(ray);

    // Initialize the closest hit
    RaycastHit closestHit = new RaycastHit();
    float closestDistance = float.MaxValue;

    // Iterate through all hits to find the closest "Cell"
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
    return closestHit;
  }
}