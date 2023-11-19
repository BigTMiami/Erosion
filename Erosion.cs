using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erosion : MonoBehaviour
{
    public int erosionCycles = 20;
    public float erosionFactor = 0.1f;

    public float FrequencyOne = 100f;
    public float AmplitudeOne = 0.9f;
    public float FrequencyTwo = 10f;
    public float AmplitudeTwo = 0.1f;


    // Start is called before the first frame update
    public void CreateHeightMap()
    {
        Terrain terrain = gameObject.GetComponent<Terrain>();

        int resolution = terrain.terrainData.heightmapResolution;

        //float[,] heights = terrain.terrainData.GetHeights(0, 0, resolution, resolution);

        float[,] heights = new float[resolution, resolution];

        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float height = AmplitudeOne * Mathf.PerlinNoise((float)x / FrequencyOne, (float)y / FrequencyOne);
                height += AmplitudeTwo * Mathf.PerlinNoise((float)x / FrequencyTwo, (float)y / FrequencyTwo);
                //height -= 0.03f * Mathf.PerlinNoise((float)x / 2f, (float)y / 2f);
                heights[x, y] = height;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);


    }

    public bool GetLowestNeighbor(int x, int y, float[,] heights, ref int outX, ref int outY, ref float outDiff)
    {
        int maxX = heights.GetLength(0);
        int maxY = heights.GetLength(1);

        outX = -1;
        outY = -1;
        outDiff = 0;

        float startHeight = heights[x, y];
        float heightDiff = 0;

        for (int neighborX = x - 1; neighborX <= x + 1; neighborX++)
        {
            if (neighborX < 0 || neighborX >= maxX) continue;
            for (int neighborY = y - 1; neighborY <= y + 1; neighborY++)
            {
                if (neighborY < 0 || neighborY >= maxY || (neighborX == x && neighborY == y)) continue;
                heightDiff = startHeight - heights[neighborX, neighborY];
                if (heightDiff > outDiff)
                {
                    outX = neighborX;
                    outY = neighborY;
                    outDiff = heightDiff;
                }
            }
        }

        return (outX != -1);
    }

    public void AddArray(float[,] adjustArray, ref float[,] finalArray)
    {
        int maxX = finalArray.GetLength(0);
        int maxY = finalArray.GetLength(1);
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                finalArray[x, y] += adjustArray[x, y];
            }
        }
    }

    public void ErodeHeightMap()
    {
        Debug.Log($"Eroding HeightMap");
        Terrain terrain = gameObject.GetComponent<Terrain>();
        int resolution = terrain.terrainData.heightmapResolution;

        int outX = 0;
        int outY = 0;
        float outDiff = 0f;

        float[,] heights = terrain.terrainData.GetHeights(0, 0, resolution, resolution);

        float[,] adjustments = new float[resolution, resolution];

        for (int cycle = 0; cycle < erosionCycles; cycle++)
        {
            for (int x = 0; x < resolution; x++)
            {
                for (int y = 0; y < resolution; y++)
                {
                    if (GetLowestNeighbor(x, y, heights, ref outX, ref outY, ref outDiff))
                    {

                        adjustments[outX, outY] += outDiff * erosionFactor;
                        adjustments[x, y] -= outDiff * erosionFactor;

                    }
                }
            }
            AddArray(adjustments, ref heights);
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }

}
