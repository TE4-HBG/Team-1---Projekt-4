using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// may god have mercy on my soul
using System.Linq;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public void Awake() { instance = this; }

    
    public List<Level> levels;
    #region PREFABS
    public GameObject levelPrefab;
    public GameObject ratPrefab;
    #endregion
    public Rat rat;
    public Camera camera;
    public static void NextLevel(Vector3 offset)
    {
        Level current = GameManager.instance.levels.Last();

        Vector3 nextLevelPos = current.transform.position + offset;

        Level nextLevel = Instantiate(GameManager.instance.levelPrefab, nextLevelPos, Quaternion.identity).GetComponent<Level>();
        nextLevel.number = current.number + 1;

        GameManager.instance.camera.transform.position = nextLevel.transform.position + nextLevel.cameraOffset;


        GameManager.instance.rat.transform.position = nextLevel.transform.position + nextLevel.entranceOffset;

        current.gameObject.SetActive(false);
        GameManager.instance.levels.Add(nextLevel);

        
    }
    public static void StartGame()
    {
        Level level = Instantiate(instance.levelPrefab).GetComponent<Level>();
        instance.levels.Add(level);

        instance.rat = Instantiate(instance.ratPrefab, level.transform.position + level.entranceOffset, Quaternion.identity).GetComponent<Rat>();
        instance.camera.transform.position = level.transform.position + level.cameraOffset;
    }
}
