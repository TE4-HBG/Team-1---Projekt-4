using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class God : MonoBehaviour
{
    public GameObject placeablePrefab;
    private MultiHashSet<int> placeableÍDs = new MultiHashSet<int>();
    private int currentPlaceable = -1;
    private void OnEnable()
    {
        placeableGameObjects = new GameObject[GameManager.instance.placeableObjects.Length];
        shouldRemoveGameobject = new bool[GameManager.instance.placeableObjects.Length];
    }
    public void Clear()
    {
        placeableÍDs.Clear();
        for (int i = 0; i < shouldRemoveGameobject.Length; i++)
        {
            shouldRemoveGameobject[i] = true;
        }
    }
    public void AddPlaceable(int id)
    {
        if (placeableÍDs.Add(id, out ulong amount))
        {
            if (shouldRemoveGameobject[id])
            {
                UpdateGameObject(id, amount);
            }
            else
            {
                CreateGameObject(id);
            }

            shouldRemoveGameobject[id] = false;
        }
        else
        {
            UpdateGameObject(id, amount);
        }
    }

    private void CreateGameObject(int id)
    {
        PlaceableObject placeableObject = GameManager.instance.placeableObjects[id];

        GameObject placeable = Instantiate(placeablePrefab, GameManager.instance.PlaceableGrid.transform);

        placeable.GetComponent<Image>().sprite = placeableObject.sprite;
        {
            PlaceableScript placeableScript = placeable.GetComponent<PlaceableScript>();
            placeableScript.id = id;
            placeableScript.metaTile = placeableObject.metaTile;
        }
        placeable.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "1";
        placeableGameObjects[id] = placeable;
    }
    private void UpdateGameObject(int id, ulong amount)
    {
        placeableGameObjects[id].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = amount.ToString();
    }

    public bool RemovePlaceable(int id)
    {
        bool bruh = placeableÍDs.Remove(id, out ulong amount);
        if (bruh)
        {
            shouldRemoveGameobject[id] = true;
        }
        else
        {
            UpdateGameObject(id, amount);
        }
        return bruh;
    }
    public void DestroyGameObjects()
    {
        for (int i = 0; i < shouldRemoveGameobject.Length; i++)
        {
            if (shouldRemoveGameobject[i])
            {
                Destroy(placeableGameObjects[i]);
                shouldRemoveGameobject[i] = false;
            }
        }
    }
    public GameObject[] placeableGameObjects;
    public bool[] shouldRemoveGameobject;
    public TileSystem tileSystem;
    public Camera cam;
    public byte rotation = 0;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log(mousePos);
            Ray ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, 1 << Layer.Tile))
            {
                //Debug.Log(hitInfo.collider.isTrigger);
                //Debug.Log(hitInfo.transform);
                if (hitInfo.collider.isTrigger)
                {
                    Vector3Int? possibleIndex = tileSystem.IndexOf(hitInfo.transform.gameObject);
                    if (possibleIndex.HasValue && currentPlaceable != -1)
                    {
                        tileSystem.PlaceMetaTile(GameManager.instance.placeableObjects[currentPlaceable].metaTile, possibleIndex.Value, rotation);
                        if (RemovePlaceable(currentPlaceable))
                        {
                            ResetCurrentPlaceable();
                            DestroyGameObjects();
                        }
                        
                    }

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rotation = (byte)((rotation + 1) % 4);
        }

    }
    public void ResetCurrentPlaceable()
    {
        currentPlaceable = -1;
    }
    public void ChangeCurrentPlaceable(int id)
    {
        if(currentPlaceable != -1)
        {
            placeableGameObjects[currentPlaceable].transform.GetChild(1).gameObject.SetActive(false);
        }
        currentPlaceable = id;
        placeableGameObjects[currentPlaceable].transform.GetChild(1).gameObject.SetActive(true);
    }
    private void OnValidate()
    {
        rotation %= 4;
    }
}
