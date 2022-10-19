using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public GameObject[] tilePrefabs;

    public void OnEnable()
    {
        instance = this;
    }
}
