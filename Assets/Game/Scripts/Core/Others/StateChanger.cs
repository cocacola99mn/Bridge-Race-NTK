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
    }
}
