using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{
    public LevelState gameState;
    public RedAIState redAIState;
    public GreenAIState greenAIState;
    public YellowAIState yellowAIState;
    private void OnTriggerEnter(Collider other)
    {
        LevelManager.Ins.ChangeGameState(gameState);
        switch (other.tag)
        {
            case GameConstant.RED_TAG:
                AIManager.Ins.ChangeRedAIState(redAIState);
                break;

            case GameConstant.GREEN_TAG:
                AIManager.Ins.ChangeGreenAIState(greenAIState);
                break;

            case GameConstant.YELLOW_TAG:
                AIManager.Ins.ChangeYellowAIState(yellowAIState);
                break;

            default:
                Debug.Log("Error AI State");
                break;
        }
    }
}
