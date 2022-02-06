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
    float hexWidth = 1.73f;
    float hexHeight = 2.0f;
    public float gap = 0.0f;
    public float xHexScale = 1.0f;
    public float yHexScale = 1.0f;
    public float zHexScale = 1.0f;

    // Array of Surface Objects
    public Transform[] objects;

    // Position of Hex 1
    Vector3 startPos;
 
    public void GenerateGrid()
    {
        ScaleHex();
        AddGap();
        CalcStartPos();
        CreateGrid();
    }

    void ScaleHex()
    {
        hexWidth = hexWidth * xHexScale;
        hexHeight = hexHeight * zHexScale;
    }
 
    void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }
 
    void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;
 
        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);
 
        startPos = new Vector3(x, 0, z);
    }
 
    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;
 
        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeight * 0.75f;
 
        return new Vector3(x, 0, z);
    }
 
    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                hex.localScale = new Vector3(xHexScale, yHexScale, zHexScale);
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "x" + x + "y" + y;

                int index = Random.Range(0, objects.Length);
                Transform obj = Instantiate(objects[index]) as Transform;
                obj.position = hex.position;
                obj.parent = hex;
                obj.name = "Object";
            }
        }
    }

    public void Reset()
    {
        for (int i = this.transform.childCount; i > 0; --i)
        {
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        }

        // Reset Adjusted Hex Dimensions
        hexWidth = 1.73f;
        hexHeight = 2.0f;
    }
}
