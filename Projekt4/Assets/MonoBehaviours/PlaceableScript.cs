using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableScript : MonoBehaviour
{
    public int id;
    public MetaTile metaTile;

    public void OnSelect()
    {
        if(GameManager.instance.god.currentPlaceable == id)
        {
            GameManager.instance.god.ResetCurrentPlaceable();
        }
        else
        {
            GameManager.instance.god.ChangeCurrentPlaceable(id);
        }
        
    }
}
