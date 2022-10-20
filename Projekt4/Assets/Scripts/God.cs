using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class God : MonoBehaviour
{

    public TileSystem tileSystem;
    public Camera cam;
    public GameObject ground;
    public GameObject pit;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log(mousePos);
            Ray ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, 1 << Layer.Tile))
            {
                Debug.Log(hitInfo.collider.isTrigger);
                Debug.Log(hitInfo.transform);
                if (hitInfo.collider.isTrigger)
                {
                    Vector3Int? possibleIndex = tileSystem.IndexOf(hitInfo.transform.gameObject);
                    if (possibleIndex.HasValue)
                    {
                        tileSystem.SetTile(possibleIndex.Value, pit);
                    }
                    
                }
            }
        }
        
    }
}
