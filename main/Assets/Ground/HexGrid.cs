using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    // Cell Prefab
    public Transform hexPrefab;
 
    // Map Dimensions
    public int gridWidth = 11;
    public int gridHeight = 11;
 
    // Cell Dimensions
    float gexWidth;
    float gexHeight;
    const float hexWidth = 1.73f;
    const float hexHeight = 2.0f;
    public float gap = 0.0f;
 
    // Position of Hex 1
    Vector3 startPos;
 
    public void GenerateGrid()
    {
        AddGap();
        CalcStartPos();
        CreateGrid();
    }
 
    void AddGap()
    {
        gexWidth = hexWidth + hexWidth * gap;
        gexHeight = hexHeight + hexHeight * gap;
    }
 
    void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = gexWidth / 2;
 
        float x = -gexWidth * (gridWidth / 2) - offset;
        float z = gexHeight * 0.75f * (gridHeight / 2);
 
        startPos = new Vector3(x, 0, z);
    }
 
    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = gexWidth / 2;
 
        float x = startPos.x + gridPos.x * gexWidth + offset;
        float z = startPos.z - gridPos.y * gexHeight * 0.75f;
 
        return new Vector3(x, 0, z);
    }
 
    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;
            }
        }
    }

    public void Reset()
    {
        for (int i = this.transform.childCount; i > 0; --i)
        {
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        }
    }
}
