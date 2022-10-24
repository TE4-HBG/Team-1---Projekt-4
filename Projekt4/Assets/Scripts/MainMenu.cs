using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public void StartGame()
    {
        GameManager.StartGame();
        this.gameObject.SetActive(false);
    }
    public void SettingsButton()
    {
        settings.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
        //Debug.Log("Game Closed");
    }

    
}
