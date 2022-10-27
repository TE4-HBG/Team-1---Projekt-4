using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class God : MonoBehaviour
{
    public GameObject placeablePrefab;
    private MultiHashSet<int> placeableÍDs = new MultiHashSet<int>();
    public int currentPlaceable
    {
        get;
        private set;
    } = -1;
    
    public InfiniteTileSystem preview;
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

        GameObject placeable = Instantiate(placeablePrefab, GameManager.instance.placeableGrid.transform);

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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rotation = (byte)((rotation + 1) % 4);
        }

        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos);
        Ray ray = cam.ScreenPointToRay(mousePos);
        if (currentPlaceable != -1 && Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, 1 << Layer.Tile))
        {

            Vector3Int? possibleIndex = tileSystem.IndexOf(hitInfo.transform.gameObject);
            if (possibleIndex.HasValue)
            {
                preview.transform.position = Vector3.Scale(possibleIndex.Value, tileSystem.cellSize) + tileSystem.transform.position + new Vector3(0f,2.5f,0f);
                preview.transform.eulerAngles = new Vector3(0f, rotation * 90f, 0f);
                if (Input.GetKeyDown(KeyCode.Mouse0) && possibleIndex.Value.x != 0 && possibleIndex.Value.x != 11)
                {

                    tileSystem.PlaceMetaTile(GameManager.instance.placeableObjects[currentPlaceable].metaTile, possibleIndex.Value, rotation);
                    if (RemovePlaceable(currentPlaceable))
                    {
                        ResetCurrentPlaceable();
                        DestroyGameObjects();
                        preview.ClearAll();
                    }
                }
            }


        }

        

    }
    public void ResetCurrentPlaceable()
    {
        if(currentPlaceable != -1)
        {
            if (placeableGameObjects[currentPlaceable] != null)
            {
                placeableGameObjects[currentPlaceable].transform.GetChild(1).gameObject.SetActive(false);
            }
            preview.ClearAll();
            currentPlaceable = -1;
        }
    }
    public void ChangeCurrentPlaceable(int id)
    {
        if (currentPlaceable != -1)
        {
            placeableGameObjects[currentPlaceable].transform.GetChild(1).gameObject.SetActive(false);
            preview.ClearAll();
        }
        currentPlaceable = id;
        placeableGameObjects[currentPlaceable].transform.GetChild(1).gameObject.SetActive(true);
        preview.PlaceMetaTile(GameManager.instance.placeableObjects[currentPlaceable].metaTile, Vector3Int.zero, 0, PreviewExtra);
    }

    private static void PreviewExtra(GameObject gameObject)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();


        for (int i = 0; i < meshRenderers.Length; i++)
        {
            Material mat = meshRenderers[i].material;
            meshRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mat.SetInt("_DstBlend", 10);
            mat.SetInt("_SrcBlend", 5);
            mat.SetFloat("_Mode", 2);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            Color color = mat.GetColor("_Color");
            color.a *= 0.5f;
            meshRenderers[i].material.SetColor("_Color", color);
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
    private void OnValidate()
    {
        rotation %= 4;
    }
}
