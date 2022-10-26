using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Placeable Object", menuName = "Placeable Object")]
public class PlaceableObject : ScriptableObject
{
    public MetaTile metaTile;
    public Sprite sprite;
    public float weight = 1f;
}
