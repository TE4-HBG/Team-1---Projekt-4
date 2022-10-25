using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Meta Tile", menuName = "Tile System/Meta Tile", order = 1)]
public class MetaTile : ScriptableObject
{
    [SerializeField]
    public Vector2Int size = Vector2Int.one;
    [SerializeField]
    public Vector3Int offset = Vector3Int.zero;
    [SerializeField]
    public GameObject[] _tiles;
    
    public GameObject GetTile(Vector3Int index)
    {
        return _tiles[index.x + index.z * size.x];
    }

    public void SetTile(Vector3Int index, GameObject gameObject)
    {
        _tiles[index.x + index.z * size.x] = gameObject;
    }
    private void OnValidate()
    {
        if((size.x * size.y) != _tiles.Length)
        {
            GameObject[] newTiles = new GameObject[size.x * size.y];

            for (int i = 0; i < _tiles.Length && i < newTiles.Length; i++)
            {
                newTiles[i] = _tiles[i];
            }
            _tiles = newTiles;
        }
    }
}
