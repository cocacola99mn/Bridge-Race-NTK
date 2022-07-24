using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{
    public LevelState gameState;

    private void OnTriggerEnter(Collider other)
    {
        LevelManager.Ins.ChangeGameState(gameState);
    }
}
