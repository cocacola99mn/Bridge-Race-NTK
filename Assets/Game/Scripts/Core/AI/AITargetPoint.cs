using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AITargetPoint : Singleton<AITargetPoint>
{
    public List<Vector3> RedTarget;
    public List<Vector3> GreenTarget;
    public List<Vector3> YellowTarget;

    private void Start()
    {
        RedTarget = new List<Vector3>();
        GreenTarget = new List<Vector3>();
        YellowTarget = new List<Vector3>();
    }

    public void getTargetPointByColor(string tag, Vector3 spawnPosition)
    {
        switch (tag)
        {
            case GameConstant.BLUE_TAG:
                break;
            
            case GameConstant.RED_TAG:
                RedTarget.Add(spawnPosition);
                Debug.Log(RedTarget[0]);
                break;
            
            case GameConstant.GREEN_TAG:
                GreenTarget.Add(spawnPosition);
                break;
            
            case GameConstant.YELLOW_TAG:
                YellowTarget.Add(spawnPosition);
                break;
            default:
                Debug.Log("TargetAI Error");
                break;
        }
    }
}