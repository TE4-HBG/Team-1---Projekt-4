using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public void Awake() { instance = this; }

    public bool timerOn = false;
    public float timer = 0f;
    public float maxTime = 60f;
    public TextMeshProUGUI timerUI;

    public float score = 0f;
    public TextMeshProUGUI scoreUI;

    public List<Level> levels;
    #region PREFABS
    public GameObject levelPrefab;
    public GameObject ratPrefab;
    #endregion
    public Rat rat;
    
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


        GameManager.instance.rat.transform.position = nextLevel.transform.position + nextLevel.entranceOffset;

        current.gameObject.SetActive(false);
        GameManager.instance.levels.Add(nextLevel);
        #endregion

        StartTimer();
    }
    public static void StartGame()
    {
        Level level = Instantiate(instance.levelPrefab).GetComponent<Level>();
        instance.levels.Add(level);
        instance.rat = Instantiate(instance.ratPrefab).GetComponent<Rat>();
        instance.rat.transform.position = level.transform.position + level.entranceOffset;

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
                GameOver(GameOverReason.TimeRanOut);
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
            RestartGame();
        }
    }
    public static void CalculateScore()
    {
        Score += instance.maxTime - instance.timer;
    }
    public static float Score 
    {
        get => instance.score;
        set
        {
            instance.score += value;
            instance.scoreUI.text = instance.score.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);
        }
        
    }
    private static void RestartGame()
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

    public static void GameOver(GameOverReason gameOverReason)
    {
        throw new NotImplementedException();
    }
}
