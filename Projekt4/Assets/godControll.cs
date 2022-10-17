using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godControll : MonoBehaviour
{
    public Camera cam;
    public GameObject obstacle;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log(mousePos);
            Ray ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f))
            {
                GameObject.Instantiate(obstacle, hitInfo.point, Quaternion.identity);
            }
        }
        
    }
}
