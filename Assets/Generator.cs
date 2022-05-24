using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class Generator : MonoBehaviour
{
    public Tilemap tileMap;
    public TileBase tile, debug;

    public float seed;

    Vector3Int cellPos, prevCellPos = new Vector3Int(-100, -100, -100);

    int offsetX, offsetY;

    // Start is called before the first frame update
    void Start()
    {
        int newPoint;

        int height = (
            tileMap.WorldToCell(camGetUpperLeft()) - 
            tileMap.WorldToCell(camGetLowerLeft())
        ).y;

        int width = (
            tileMap.WorldToCell(camGetLowerRight()) -
            tileMap.WorldToCell(camGetLowerLeft())
        ).x * 2;
        offsetX = tileMap.WorldToCell(camGetLowerLeft()).x;
        offsetY = tileMap.WorldToCell(camGetLowerLeft()).y;

        Debug.Log($"h: {height}+{offsetY} w: {width}+{offsetX}");

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
        var camPos = Camera.main.transform.position;

        camPos.x += Screen.width;
        camPos.y += Screen.height;

        camPos = Camera.main.ScreenToWorldPoint(camPos);

        cellPos = tileMap.WorldToCell(new Vector3(camPos.x, camPos.y, 0));

        if (cellPos != prevCellPos)
        {
            prevCellPos = cellPos;
            Debug.Log("" + camPos + ' ' + cellPos);
            tileMap.SetTile(cellPos, debug);
        }
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
                    tilemap.SetTile(new Vector3Int(x + offsetX, y + offsetY, 0), tile);
    }

    public void UpdateMap(int[,] map, Tilemap tilemap)
    {
        for (int x = 0; x < map.GetUpperBound(0); x++)
            for (int y = 0; y < map.GetUpperBound(1); y++)
                if (map[x, y] == 0)
                    tilemap.SetTile(new Vector3Int(x + offsetX, y + offsetY, 0), null);
    }

    public Vector3 camGetHelper(Camera camera, float x, float y)
    {
        var camPos = Camera.main.transform.position;

        camPos.x += x * Screen.width;
        camPos.y += y * Screen.height;

        return Camera.main.ScreenToWorldPoint(camPos);
    }

    public Vector3 camGetLowerLeft() => camGetHelper(Camera.main, 0, 0);
    public Vector3 camGetUpperLeft() => camGetHelper(Camera.main, 0, 1);
    public Vector3 camGetLowerRight() => camGetHelper(Camera.main, 1, 0);
    public Vector3 camGetUpperRight() => camGetHelper(Camera.main, 1, 1);

}
