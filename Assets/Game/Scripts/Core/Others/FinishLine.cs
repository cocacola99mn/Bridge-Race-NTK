using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.BLUE_TAG))
            LevelManager.Ins.ChangeGameState(LevelState.Win);
        else if (other.CompareTag(GameConstant.RED_TAG) || other.CompareTag(GameConstant.GREEN_TAG) || other.CompareTag(GameConstant.YELLOW_TAG))
            LevelManager.Ins.ChangeGameState(LevelState.Lose);
    }
}
