using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;

public class TileSystem : MonoBehaviour
{
    public Vector3Int size = new Vector3Int(10,10,1);
    public Vector3 cellSize = Vector3.one;
    public GameObject defaultPrefab;

    private GameObject[,,] instances;

    private void Start()
    {
        InitializeInstances();
    }
    private void InitializeInstances()
    {
        instances = new GameObject[size.x, size.y, size.z];
        for (int z = 0; z < instances.GetLength(2); z++)
        {
            for (int y = 0; y < instances.GetLength(1); y++)
            {
                for (int x = 0; x < instances.GetLength(0); x++)
                {
                    instances[x, y, z] = GameObject.Instantiate(defaultPrefab, Vector3.Scale(cellSize,new Vector3(x, y, z)) + transform.position, Quaternion.identity, transform);
                }
            }
        }
    }

    public Vector3Int? IndexOf(GameObject instance)
    {
        for (int z = 0; z < instances.GetLength(2); z++)
        {
            for (int y = 0; y < instances.GetLength(1); y++)
            {
                for (int x = 0; x < instances.GetLength(0); x++)
                {
                    if (instances[x,y,z] == instance)
                    {
                        return new Vector3Int(x, y, z);
                    }
                }
            }
        }
        Debug.Log("UH OH");
        return null;
    }
    
    public void SetTile(Vector3Int index, GameObject prefab)
    {
        Debug.Log(index);
        GameObject.Destroy(instances[index.x, index.y, index.z]);
        instances[index.x, index.y, index.z] = GameObject.Instantiate(prefab, Vector3.Scale(cellSize, index) + transform.position, Quaternion.identity, transform);

    }

}
