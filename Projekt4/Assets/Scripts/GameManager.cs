using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public void Awake() { instance = this; }

    
    public Level currentLevel;
    public GameObject levelPrefab;
    public Rat rat;
    public Camera camera;
}
