using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PeekCornerFinder : MonoBehaviour
{
    public Tilemap wallTilemap;
    public Tilemap peekTilemap;
    public Tile peekTile;

    [ContextMenu("Mark Peek Corners")]
    public void MarkPeekCorners()
    {
        if (wallTilemap == null || peekTilemap == null || peekTile == null)
        {
            Debug.LogWarning("Assign all tilemaps and the peekTile.");
            return;
        }

        BoundsInt bounds = wallTilemap.cellBounds;

        peekTilemap.ClearAllTiles();

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (wallTilemap.HasTile(pos)) continue;

                TryMarkPeekCorner(pos, new Vector2Int(-1, 1));
                TryMarkPeekCorner(pos, new Vector2Int(1, 1));
                TryMarkPeekCorner(pos, new Vector2Int(-1, -1));
                TryMarkPeekCorner(pos, new Vector2Int(1, -1));
            }
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(peekTilemap); // Marks tilemap dirty so Unity knows to save it
#endif
    }

    void TryMarkPeekCorner(Vector3Int pos, Vector2Int dir)
    {
        Vector3Int corner = pos + new Vector3Int(dir.x, dir.y, 0);
        Vector3Int side1 = pos + new Vector3Int(dir.x, 0, 0);
        Vector3Int side2 = pos + new Vector3Int(0, dir.y, 0);

        if (wallTilemap.HasTile(corner) &&
            !wallTilemap.HasTile(side1) &&
            !wallTilemap.HasTile(side2))
        {
            peekTilemap.SetTile(pos, peekTile);
        }
    }
}