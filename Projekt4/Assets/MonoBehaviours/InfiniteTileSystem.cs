using System;
using UnityEngine;

public class InfiniteTileSystem : MonoBehaviour
{
    public Vector3 cellSize = Vector3.one;

    public void SetTile(Vector3Int position, GameObject prefab)
    {
        GameObject.Instantiate(prefab, Vector3.Scale(cellSize, position) + transform.position, Quaternion.identity, transform);
    }
    public void ClearAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }
    }
    private Vector3Int MetaTile0d(Vector3Int xy)
    {
        return new Vector3Int(xy.x, 0, xy.z);
    }
    private Vector3Int MetaTile90d(Vector3Int xy)
    {
        return new Vector3Int(xy.z, 0, -xy.x);
    }
    private Vector3Int MetaTile180d(Vector3Int xy)
    {
        return -new Vector3Int(xy.x, 0, xy.z);
    }
    private Vector3Int MetaTile270d(Vector3Int xy)
    {
        return new Vector3Int(-xy.z, 0, xy.x);
    }

    public void PlaceMetaTile(MetaTile metaTile, Vector3Int position, byte rotation = 0)
    {
        Vector3Int truePosition = position + metaTile.offset;
        Func<Vector3Int, Vector3Int>[] PlayeMetaTiles = new Func<Vector3Int, Vector3Int>[4] { MetaTile0d, MetaTile90d, MetaTile180d, MetaTile270d };

        Vector3Int xy = Vector3Int.zero;
        for (xy.z = 0; xy.z < metaTile.size.y; xy.z++)
        {
            for (xy.x = 0; xy.x < metaTile.size.x; xy.x++)
            {
                SetTile(PlayeMetaTiles[rotation](xy) + truePosition, metaTile.GetTile(xy));
            }
        }
    }

}
