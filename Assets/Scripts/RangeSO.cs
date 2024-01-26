using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Range", menuName = "Scriptable Object/Range", order = 1)]
public class RangeSO : ScriptableObject
{
  public List<Vector2Int> RangeMatrix;
}