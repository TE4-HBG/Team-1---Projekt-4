using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableScript : MonoBehaviour
{
    public int id;
    public MetaTile metaTile;

    public void OnSelect()
    {
        GameManager.instance.god.ChangeCurrentPlaceable(id);
    }
}
