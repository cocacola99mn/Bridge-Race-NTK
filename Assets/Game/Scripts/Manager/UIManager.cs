using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIStartGame { Open, Close }
public class UIManager : Singleton<UIManager>
{
    public GameObject uiStartGameCanvas;
    public GameObject InstructionPanel;

    private UIStartGame uIStartGame;
    
    public void ChangeUIStartGame(UIStartGame uIStartGame)
    {
        this.uIStartGame = uIStartGame;
        switch (uIStartGame)
        {
            case UIStartGame.Open:
                uiStartGameCanvas.SetActive(true);
                break;
            
            case UIStartGame.Close:
                uiStartGameCanvas.SetActive(false);
                InstructionPanel.SetActive(true);
                break;
            
            default:
                Debug.Log("Error UI StartGame");
                break;
        }
    }
}
