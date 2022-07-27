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
                Debug.Log("2");
                break;
            
            case LevelState.ThirdState:
                Debug.Log("3");
                break;
            
            case LevelState.Win:
                Player.Ins.OnFinish = true;
                Player.Ins.MovementAnim.enabled = false;
                ThePlayer.transform.rotation = Quaternion.Euler(0, 180, 0);
                ThePlayer.gameObject.transform.position = new Vector3(0, 15, 44.2f);
                break;
            
            case LevelState.Lose:
                Debug.Log("Lose");
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
}
