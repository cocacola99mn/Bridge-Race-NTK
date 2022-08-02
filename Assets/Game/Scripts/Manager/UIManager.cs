using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIStartGame { Open, Close }

public enum UIVictory { Open, Close }

public enum UIFail { Open, Close }
public class UIManager : Singleton<UIManager>
{
    public GameObject uiStartGameCanvas;
    public GameObject InstructionPanel;
    public GameObject uiVictoryPanel;
    public GameObject uiFailPanel;

    private UIStartGame uIStartGame;
    private UIVictory uiVictory;
    private UIFail uiFail;
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

    public void ChangeUIVictory(UIVictory uiVictory)
    {
        this.uiVictory = uiVictory;

        switch (uiVictory)
        {
            case UIVictory.Open:
                uiVictoryPanel.SetActive(true);
                break;
            
            case UIVictory.Close:
                uiVictoryPanel.SetActive(false);
                break;
            default:
                Debug.Log("Error UI Victory");
                break;
        }
    }

    public void ChangeUIFail(UIFail uiFail)
    {
        this.uiFail = uiFail;

        switch (uiFail)
        {
            case UIFail.Open:
                uiFailPanel.SetActive(true);
                break;

            case UIFail.Close:
                uiFailPanel.SetActive(false);
                break;
            default:
                Debug.Log("Error UI Victory");
                break;
        }
    }
}
