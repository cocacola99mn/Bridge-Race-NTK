using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FirstState, SecondState, ThirdState, Win}
public class LevelManager : Singleton<LevelManager>
{
    private GameState gameState;
    
    public void ChangeGameState(GameState gameState)
    {
        this.gameState = gameState;

        switch (gameState)
        {
            case GameState.FirstState:
                Debug.Log("1");
                break;
            case GameState.SecondState:
                Debug.Log("2");
                break;
            case GameState.ThirdState:
                Debug.Log("3");
                break;
            case GameState.Win:
                Debug.Log("4");
                break;
            default:
                Debug.Log("Invalid State");
                break;
        }
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }
}
