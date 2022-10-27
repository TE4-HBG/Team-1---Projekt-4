using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public PlaceableObject[] placeableObjects = new PlaceableObject[1];
    public float objectSum;
    public GameObject placeableGrid;
    public static GameManager instance;
    public InfiniteTileSystem preview;
    public void Awake() { instance = this; }

    public bool timerOn = false;
    public float timer = 0f;
    public float maxTime = 60f;
    public TextMeshProUGUI timerUI;
    
    public float score = 0f;
    public TextMeshProUGUI scoreUI;
    public GameObject gameOverUI;
    public List<Level> levels;
    #region PREFABS
    public GameObject levelPrefab;
    public GameObject ratPrefab;
    public GameObject godPrefab;
    #endregion
    public Rat rat;
    public God god;
    public static void NextLevel(Vector3 offset)
    {
        #region Calculate score
        CalculateScore();
        ResetTimer();
        #endregion

        #region Create next level
        Level current = GameManager.instance.levels.Last();


        Vector3 nextLevelPos = current.transform.position + offset;

        Level nextLevel = Instantiate(GameManager.instance.levelPrefab, nextLevelPos, Quaternion.identity).GetComponent<Level>();
        nextLevel.number = current.number + 1;


        ResetRat(nextLevel.transform.position + nextLevel.entranceOffset);

        current.gameObject.SetActive(false);
        GameManager.instance.levels.Add(nextLevel);
        UpdateGod();
        #endregion
        JukeBox.Play(SoundEffect.Goal);
        StartTimer();
    }
    public static void StartGame()
    {
        Level level = Instantiate(instance.levelPrefab).GetComponent<Level>();
        instance.levels.Add(level);
        instance.rat = Instantiate(instance.ratPrefab).GetComponent<Rat>();
        instance.rat.transform.position = level.transform.position + level.entranceOffset;
        CreateGod();
        UpdateGod();
        StartTimer();
    }

    public static void StartTimer()
    {
        instance.timerOn = true;
    }
    public void UpdateTimer()
    {
        if (timerOn)
        {
            timer = Mathf.Min(timer + Time.deltaTime, maxTime);
            timerUI.text = (maxTime - timer).ToString("F1", System.Globalization.CultureInfo.InvariantCulture);
            if (timer == maxTime)
            {
                ResetTimer();
                GameOver(GameOverReason.TimeRanOut(this));
            }

            
        }
    }
    public static void ResetTimer()
    {
        instance.timerOn = false;
        instance.timer = 0f;
    }
    private void Update()
    {
        UpdateTimer();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
    public static void CalculateScore()
    {
        Score += ((instance.maxTime - instance.timer) * 100f)/ instance.maxTime;
    }
    public static float Score 
    {
        get => instance.score;
        set
        {
            instance.score = value;
            instance.scoreUI.text = instance.score.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);
        }
        
    }
    public static void Restart()
    {
        ResetTimer();
        Score = 0f;
        for (int i = 0; i < instance.levels.Count; i++)
        {
            Destroy(instance.levels[i].gameObject);
        }
        instance.levels.Clear();
        Level level = Instantiate(instance.levelPrefab).GetComponent<Level>();
        instance.levels.Add(level);
        UpdateGod();

        ResetRat(level.transform.position + level.entranceOffset);

        StartTimer();
    }
    public static void ResetRat(Vector3 position)
    {
        GameObject rat = instance.rat.gameObject;
        Destroy(rat);
        instance.rat = Instantiate(instance.ratPrefab).GetComponent<Rat>();
        instance.rat.transform.position = position;
    }
    public static void CreateGod()
    {
        instance.god = Instantiate(instance.godPrefab).GetComponent<God>();
    }
    public static void UpdateGod()
    {
        instance.god.preview = instance.preview;
        instance.god.transform.SetPositionAndRotation(instance.levels.Last().godPosition.position, instance.levels.Last().godPosition.rotation);
        instance.god.tileSystem = instance.levels.Last().tileSystem;
        int amountOfRolls = ((int)(Score / 25f) + 4);
        instance.god.ResetCurrentPlaceable();


        instance.god.Clear();
        instance.god.DestroyGameObjects();

        for (int i = 0; i < amountOfRolls; i++)
        {
            float budget = Random.Range(0f,1f) * instance.objectSum;
            // Step through all the possibilities, one by one, checking to see if each one is selected.
            int index = instance.placeableObjects.Length - 1;
            
            while (index >= 0)
            {

                // Remove the last item from the sum of total untested weights and try again.
                budget -= instance.placeableObjects[index].weight;

                if(budget <= 0)
                {
                    instance.god.AddPlaceable(index);
                    break;
                }


                index -= 1;
            }
            
        }
        
    }
    
    public static void GameOver(GameOverReason gameOverReason)
    {
        ResetTimer();
        JukeBox.Play(SoundEffect.GameOver);
        Debug.Log("GAME OVER!");
        Debug.Log($"Reason: {gameOverReason.info}");
        Debug.Log($"Caller: {gameOverReason.caller}");

        instance.gameOverUI.SetActive(true);
    }

    private void Start()
    {
        objectSum = 0f;

        for (int i = 0; i < placeableObjects.Length; i++)
        {
            objectSum += placeableObjects[i].weight;
        }
    }

    public static void SetLight(bool isOn)
    {
        instance.levels.Last().lightContainer.SetActive(isOn);
    }
}
