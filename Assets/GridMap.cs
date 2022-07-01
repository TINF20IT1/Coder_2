using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMap
{
    public delegate int[,] MapMutator(int[,] map, Vector2Int offset);

    private int width, height;
    private int[,] map;
    private MapMutator mutator;

    public GridMap(int width, int height, bool empty, Vector2Int offset, MapMutator mapMutator)
    {
        this.width = width;
        this.height = height;
        this.mutator = mapMutator;

        // create empty or full array that'll be used for the map
        int[,] array = new int[width, height];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                array[i, j] = empty ? 0 : 1;

        // generate the map
        map = mutator(array, offset);
    }

    // TODO: Provide a method of mapping integers to tilebases
    public void Render(Tilemap tilemap, TileBase tile, Vector2Int offset)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (map[x, y] == 1)
                    tilemap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), tile);
    }

    public void UpdateRender(Tilemap tilemap, TileBase tile, Vector2Int offset)
    {
        map = mutator(map, offset);
        Render(tilemap, tile, offset);
    }
    
    /*
    public int this[int x, int y]
    {
        get { return map[x, y]; }
        set { map[x, y] = value; }
    }
    */

}
