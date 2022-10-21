using UnityEngine;

public class TileSystem : MonoBehaviour
{
    public Vector3 pivot = new Vector3(0.5f, 0.0f, 0.5f);
    public Vector3Int size = new Vector3Int(10,10,1);
    public Vector3 cellSize = Vector3.one;
    public GameObject defaultPrefab;

    private GameObject[,,] instances;

    private Vector3 max => Vector3.Scale(cellSize, size);
    private void Start()
    {
        InitializeInstances();
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3Int index = new Vector3Int(0, 0, 0);
        for (index.z = 0; index.z < size.z; index.z++)
        {
            for (index.y = 0; index.y < size.y; index.y++)
            {
                for (index.x = 0; index.x < size.x; index.x++)
                {
                    Gizmos.DrawWireCube(Vector3.Scale(cellSize, index) + transform.position, cellSize);
                }
            }
        }

    }

    private void InitializeInstances()
    {
        instances = new GameObject[size.x, size.y, size.z];
        Vector3Int index = new Vector3Int(0,0,0); 
        for (index.z = 0; index.z < instances.GetLength(2); index.z++)
        {
            for (index.y = 0; index.y < instances.GetLength(1); index.y++)
            {
                for (index.x = 0; index.x < instances.GetLength(0); index.x++)
                {
                    SetTile(index, defaultPrefab);
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
        instances[index.x, index.y, index.z] = GameObject.Instantiate(prefab, Vector3.Scale(cellSize, index) + transform.position - Vector3.Scale(max,pivot), Quaternion.identity, transform);

    }

}
