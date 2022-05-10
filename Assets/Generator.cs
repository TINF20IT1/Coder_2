using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    public Tilemap tileMap;
    public float seed;
    public int height = 100, width = 500, xOffset = 20, yOffset = 10;
    public TileBase tile;

    // Start is called before the first frame update
    void Start()
    {
        int newPoint;
        var map = GenerateArray(width, height, true);
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            var input = x * 0.1f;

            Debug.Log($"{Mathf.PerlinNoise(input, seed)} <- {x}, {seed}");
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(input, seed)) * map.GetUpperBound(1));
            //Debug.Log(newPoint);
            newPoint /= 4;
            for (int y = newPoint; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }
        RenderMap(map, tileMap, tile);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] array = new int[width, height];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                array[i, j] = empty ? 0 : 1;
        return array;
    }

    public void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        tilemap.ClearAllTiles();
        for (int x = 0; x < map.GetUpperBound(0); x++)
            for (int y = 0; y < map.GetUpperBound(1); y++)
                if (map[x, y] == 1)
                    tilemap.SetTile(new Vector3Int(x - xOffset, y - yOffset, 0), tile);
    }

    public void UpdateMap(int[,] map, Tilemap tilemap)
    {
        for (int x = 0; x < map.GetUpperBound(0); x++)
            for (int y = 0; y < map.GetUpperBound(1); y++)
                if (map[x, y] == 0)
                    tilemap.SetTile(new Vector3Int(x - xOffset , y - yOffset, 0), null);
    }


}
