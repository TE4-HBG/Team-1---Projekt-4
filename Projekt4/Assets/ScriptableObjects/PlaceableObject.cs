using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Placeable Object", menuName = "Placeable Object")]
public class PlaceableObject : ScriptableObject
{
    
    public List<PlaceableObject> list
    {
        private get;
        set;
    }
    [NonSerialized]
    private ulong _count = 1;
    public ulong Count
    {
        get => _count;
        set
        {
            if(value < 1)
            {
                list.Remove(this);
            }
        }
    }
    public MetaTile metaTile;
    public Sprite sprite;


}
