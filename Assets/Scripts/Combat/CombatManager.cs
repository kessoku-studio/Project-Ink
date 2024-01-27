using System;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
  public static CombatManager Instance { get; private set; }
  public CommandInvoker Invoker;
  public PlayerSetupSO PlayerSetup;
  // TODO: Enemy setup data should be stored here
  // public EnemySetupSO EnemySetup;

  public List<Piece> PlayerPieces = new List<Piece>();
  public List<Piece> EnemyPieces = new List<Piece>();

  public ICombatState CurrentState { get; private set; }

  private void Start()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(gameObject); // Destroy any duplicate instances.
    }

    this.Invoker = new CommandInvoker();
    // Set the state to the initial state
    ChangeState(new InitState());
  }

  private void Update()
  {
    CurrentState.UpdateState(this);
  }

  public void ChangeState(ICombatState newState)
  {
    CurrentState?.ExitState(this);
    CurrentState = newState;
    CurrentState.EnterState(this);
  }
}