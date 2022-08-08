using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
        Time.timeScale = 0;

        UIManager.Ins.OpenUI(UIID.UICMainMenu);
    }
}
