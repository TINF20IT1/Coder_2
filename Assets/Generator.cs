using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

class RectMap
{
    int[,] map { set; get; }
    public RectMap(int[,] map)
    {
        this.map = map;
    }

    public void DrawRect(
        int startPosx, int startPosy, int dimensionsx, int dimensionsy)
    {
        for (int x = startPosx; x < startPosx + dimensionsx; x++)
            for (int y = startPosy; y < startPosy + dimensionsy; y++)
                map[x, y] = 1;
    }
}

public class Generator : MonoBehaviour
{
    public Tilemap tileMap;
    public TileBase tile, debug, interactable;

    public float seed;
    
    private TileBase[] Tiles;

    Vector3Int cellPos, nextCellPos = new Vector3Int(-100, -100, -100);
    Vector2Int offset;

    int width, height, start;
    GridMap map;

    private void Awake()
    {
        Tiles = new string[] 
            {
                "BluePL", "GreenPL", "Marssand",
                "Marssandschach", "Steel", "Treibsand",
                "Treibsandschach"
            }
            .Select(x => Resources.Load<TileBase>($"Tiles/{x}")).ToArray();
        foreach (var item in Tiles)
        {
            Debug.Log(item.GetType());
        }
        Debug.Log(Tiles.Length);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        // Remove static tilemap
        tileMap.ClearAllTiles();

        height = (
            tileMap.WorldToCell(camGetUpperLeft()) - 
            tileMap.WorldToCell(camGetLowerLeft())
        ).y * 3;

        width = (
            tileMap.WorldToCell(camGetLowerRight()) -
            tileMap.WorldToCell(camGetLowerLeft())
        ).x * 2;

        nextCellPos = tileMap.WorldToCell(camGetUpperRight());
        nextCellPos.x += width /2;
        tileMap.SetTile(nextCellPos, debug);

        offset = (Vector2Int) tileMap.WorldToCell(camGetLowerLeft());

        Debug.Log($"h: {height}+{offset.y} w: {width}+{offset.x}");

        start = height / 2;

        #region Gen

        GridMap.MapMutator perlinGenerate = (map, offset) =>
        {
            int newPoint;

            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                var input = x * 0.1f;
                //Debug.Log($"{Mathf.PerlinNoise(input, seed)} <- {x}, {seed}");
                newPoint =
                    Mathf.FloorToInt((Mathf.PerlinNoise(input, seed))
                        * map.GetUpperBound(1));
                //Debug.Log(newPoint);
                newPoint /= 4;
                for (int y = newPoint; y >= 0; y--)
                {
                    map[x, y] = 1;
                }
            }
            return map;
        };

        GridMap.MapMutator terrainGenerate = (map, offset) =>
        {
            // map is an empty chunk; add a start and end platform.
            var mmap = new RectMap(map);

            // set start platform
            mmap.DrawRect(0, 0, 2, start);

            // vary start and end pos
            var stepUp = Random.Range(-3, 3);
            if (start + stepUp > (height * 3 / 4))
                start = (height * 3 / 4);
            else if (start + stepUp < (height * 1 / 4))
                start = (height * 1 / 4);
            else
                start += stepUp;

            // set end platform
            mmap.DrawRect(0,0, 2, start);

            return map;
        };

        #endregion Gen

        map = new GridMap(
            width,
            height,
            true,
            offset,
            perlinGenerate
        );

        //var map = GenerateArray(width, height, true);

        map.Render(tileMap, tile, offset);

        tileMap.SetTile(new Vector3Int(3, 0, 0), interactable);

        //RenderMap(map, tileMap, tile);
    }

    // Update is called once per frame
    void Update()
    {
        var camPos = Camera.main.transform.position;

        camPos.x += Screen.width;
        camPos.y += Screen.height;

        camPos = Camera.main.ScreenToWorldPoint(camPos);

        cellPos = tileMap.WorldToCell((Vector2)camPos);

        if (cellPos.x > nextCellPos.x - 1)
        {
            nextCellPos.x += width;
            offset.x += width - 1;
            map.UpdateRender(tileMap, Tiles[Random.Range(0,Tiles.Length)], offset);
            tileMap.SetTile(cellPos, debug);
        }

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
