using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState { FirstState, SecondState, ThirdState, Win, Lose}
public class LevelManager : Singleton<LevelManager>
{
    public GameObject ThePlayer, RedAI, GreenAI;
    private LevelState gameState;

    public void ChangeGameState(LevelState gameState)
    {
        this.gameState = gameState;

        switch (gameState)
        {
            case LevelState.SecondState:
                break;
            
            case LevelState.ThirdState:
                break;
            
            case LevelState.Win:
                OnWin();
                break;
            
            case LevelState.Lose:
                UIManager.Ins.ChangeUIFail(UIFail.Open);
                break;
            
            default:
                Debug.Log("Invalid State");
                break;
        }
    }

    public bool IsState(LevelState gameState)
    {
        if (this.gameState == gameState)
            return true;
        else
            return false;
    }

    public void OnWin()
    {
        Player.Ins.OnFinish = true;
        Player.Ins.MovementAnim.enabled = false;

        PlayerInteract.Ins.RemoveAllElement();

        ThePlayer.transform.rotation = Quaternion.Euler(0, 180, 0);
        ThePlayer.transform.position = new Vector3(0, 16.7f, 44.7f);

        RedAI.SetActive(true);
        GreenAI.SetActive(true);

        UIManager.Ins.ChangeUIVictory(UIVictory.Open);
    }
}
