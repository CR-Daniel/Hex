using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    // get grid width
    // get grid height
    public HexGrid hexGrid;

    // Move Row by Row
    public void Test()
    {
        // StartCoroutine(FullWave(0, hexGrid.gridWidth, 0, hexGrid.gridHeight));
        StartCoroutine(RandomPiston(50, 0.2f, 10.0f, 3));
    }

    // Move 1 Piston
    IEnumerator Wave(Transform cell, float x, float y, float z, float duration)
    {
        float timeElapsed = 0;
        Vector3 startPosition = cell.position;
        Vector3 finalPosition = cell.position + new Vector3(x, y, z);

        while (timeElapsed < duration/2)
        {
            cell.transform.position = Vector3.Lerp(startPosition, finalPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        while (duration/2 <= timeElapsed && timeElapsed < duration)
        {
            cell.transform.position = Vector3.Lerp(finalPosition, startPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    // Move Piston Selection Top-Down
    IEnumerator FullWave(int xStart, int xEnd, int yStart, int yEnd)
    {
        for (int y = yStart; y < yEnd; y++)
        {
            for (int x = xStart; x < xEnd; x++)
            {
                Transform cell = transform.Find("x" + x.ToString() + "y" + y.ToString());
                StartCoroutine(Wave(cell, 0.0f, 5.0f, 0.0f, 2));   
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Move X Pistons at Random
    IEnumerator RandomPiston(int qty, float period, float elevation, int speed)
    {
        for (int i = 0; i < qty; i++)
        {
            int x = Random.Range(0, hexGrid.gridWidth);
            int y = Random.Range(0, hexGrid.gridWidth);

            Transform cell = transform.Find("x" + x.ToString() + "y" + y.ToString());
            StartCoroutine(Wave(cell, 0.0f, elevation, 0.0f, speed));
            yield return new WaitForSeconds(period);
        }
    }
}
