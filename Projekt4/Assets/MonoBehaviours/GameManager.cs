using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
//using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public PlaceableObject[] placeableObjects = new PlaceableObject[1];
    public float objectSum;
    public GameObject placeableGrid;
    public static GameManager instance;
    public InfiniteTileSystem preview;
    public void Awake() { instance = this; }

    public Timer gameTimer = new Timer(paused: true);
    public CircularTimer PowerUpSpawnTimer = new CircularTimer(paused: true);
    public float preparationTime = 20f;
    public float roundTime = 60f;
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
        #endregion

        #region Create next level
        Level current = instance.levels.Last();


        Vector3 nextLevelPos = current.transform.position + offset;

        Level nextLevel = Instantiate(instance.levelPrefab, nextLevelPos, Quaternion.identity).GetComponent<Level>();
        nextLevel.number = current.number + 1;
        current.gameObject.SetActive(false);
        instance.levels.Add(nextLevel);
        UpdateGod();

        SetRatActivity(false);
        #endregion
        JukeBox.Play(SoundEffect.Goal);
        StartPreparation();
        
    }
    public static void StartGame()
    {
        Level level = Instantiate(instance.levelPrefab).GetComponent<Level>();
        instance.levels.Add(level);
        CreateGod();
        UpdateGod();

        StartPreparation();

    }
    
    public static void StartRound()
    {
        instance.gameTimer.Set(instance.roundTime, GameOverTimer);

        Level level = instance.levels.Last();

        if(level.number == 0)
        {
            CreateRat(level.entranceOffset + level.transform.position);
        }
        else
        {
            SetRatActivity(true);
            UpdateRat(level.entranceOffset + level.transform.position)
        }
        
        

        static void GameOverTimer() => GameOver(GameOverReason.TimeRanOut(instance));   
}
    public static void StartPreparation()
    {
        instance.gameTimer.Set(instance.preparationTime, StartRound);
        JukeBox.Play(Song.Preparations);

    }
    public void UpdateTimer()
    {
        timerUI.text = (gameTimer.end - gameTimer.time).ToString("F1", CultureInfo.InvariantCulture);
        gameTimer.Update(Time.deltaTime);
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
        Score += ((instance.roundTime - instance.gameTimer.time) * 100f)/ instance.roundTime;
    }
    public static float Score 
    {
        get => instance.score;
        set
        {
            instance.score = value;
            instance.scoreUI.text = instance.score.ToString("F0", CultureInfo.InvariantCulture);
        }
        
    }
    public static void Restart()
    {
        Score = 0f;
        for (int i = 0; i < instance.levels.Count; i++)
        {
            Destroy(instance.levels[i].gameObject);
        }
        instance.levels.Clear();
        Level level = Instantiate(instance.levelPrefab).GetComponent<Level>();
        instance.levels.Add(level);
        UpdateGod();
        RemoveRat();

        StartPreparation();
        
    }
    public static void ResetRat(Vector3 position)
    {
        RemoveRat();
        CreateRat(position);
    }

    public static void CreateRat(Vector3 position)
    {
        instance.rat = Instantiate(instance.ratPrefab).GetComponent<Rat>();
        instance.rat.transform.position = position;
    }
    public static void SetRatActivity(bool isActive)
    {
        instance.rat.gameObject.SetActive(isActive);
    }
    public static void UpdateRat(Vector3 position)
    {
        instance.rat.transform.position = position;
    }
    public static void RemoveRat()
    {
        GameObject rat = instance.rat.gameObject;
        Destroy(rat);
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
        instance.gameTimer.paused = true;
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
    private static void SpawnPowerUp()
    {

    }
}
