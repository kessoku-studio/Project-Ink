using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Range_Matrix", menuName = "Scriptable Object/Range Matrix", order = 1)]
public class RangeMatrix : ScriptableObject
{
  public List<Vector2Int> MoveMatrix;
}