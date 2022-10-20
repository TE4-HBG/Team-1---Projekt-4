using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Level : MonoBehaviour
{
    public int number = 0;
    public Vector3 entranceOffset;
    public Vector3 cameraOffset;

    [ExecuteInEditMode]
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + entranceOffset, 1);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + cameraOffset, 1);

        Gizmos.color = Color.white;
    }
}
