using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        UIManager.Ins.ChangeUIStartGame(UIStartGame.Close);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
