using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject level;
    [SerializeField]
    private Vector3 offset = new Vector3(0,0,20);
    [SerializeField]
    private GameObject Kamera;

    
    private void OnTriggerEnter(Collider other)
    {
        Vector3 levelPos = transform.parent.position;

        Vector3 nextLevelPos = levelPos + offset;
        
        GameObject nextLevel =  Instantiate(level, nextLevelPos, Quaternion.identity);
        
        Kamera.transform.position += offset;
        
        
        GameObject.FindGameObjectWithTag("Player").transform.position = nextLevel.transform.Find("Entrance").position;
    }
}
