using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Board_Setup", menuName = "Scriptable Object/Board Setup", order = 1)]
public class BoardSetupSO : ScriptableObject
{
  public List<List<TerrainType>> setup;
  public void InitializeMatrix(int width, int height)
  {
    setup = new List<List<TerrainType>>();
    for (int i = 0; i < height; i++)
    {
      List<TerrainType> row = new List<TerrainType>();
      for (int j = 0; j < width; j++)
      {
        row.Add(TerrainType.Base);
      }
      setup.Add(row);
    }
  }
}
