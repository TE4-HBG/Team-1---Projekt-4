using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0,0,20);
    
    
    private void OnTriggerEnter(Collider other)
    {
        
        
        Vector3 nextLevelPos = GameManager.instance.currentLevel.transform.position + offset;

        Level nextLevel = Instantiate(GameManager.instance.levelPrefab, nextLevelPos, Quaternion.identity).GetComponent<Level>();
        nextLevel.number = GameManager.instance.currentLevel.number + 1;

        GameManager.instance.camera.transform.position = nextLevel.transform.position + nextLevel.cameraOffset;
        
        
        GameManager.instance.rat.transform.position = nextLevel.transform.position + nextLevel.entranceOffset;

        GameManager.instance.currentLevel = nextLevel;
    }
}
