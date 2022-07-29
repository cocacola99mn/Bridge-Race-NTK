using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    PlayerInteract interact;
    AIInteract aIInteract;

    private void Start()
    {
        interact = PlayerInteract.Ins;
        aIInteract = AIInteract.Ins;
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (gameObject.tag)
        {
            case GameConstant.BLUE_TAG:
                FallCondition(interact.BrickHolder, other.gameObject);
                break;
            case GameConstant.RED_TAG:
                FallCondition(aIInteract.RedBrickHolder, other.gameObject);
                break;
            case GameConstant.GREEN_TAG:
                FallCondition(aIInteract.GreenBrickHolder, other.gameObject);
                break;
            case GameConstant.YELLOW_TAG:
                FallCondition(aIInteract.YellowBrickHolder, other.gameObject);
                break;
            default:
                break;
        }
    }

    public void FallCondition(List<GameObject> BrickHolder, GameObject other)
    {
        switch (other.tag)
        {
            case GameConstant.BLUE_TAG:
                if (BrickHolder.Count < interact.BrickHolder.Count)
                    Debug.Log("Fall Blue");
                break;
            case GameConstant.RED_TAG:
                if (BrickHolder.Count < aIInteract.RedBrickHolder.Count)
                    Debug.Log("Fall Red");
                break;
            case GameConstant.GREEN_TAG:
                if (BrickHolder.Count < aIInteract.GreenBrickHolder.Count)
                    Debug.Log("Fall Green");
                break;
            case GameConstant.YELLOW_TAG:
                if (BrickHolder.Count < aIInteract.YellowBrickHolder.Count)
                    Debug.Log("Fall Yellow");
                break;
            default:
                Debug.Log("Error Fall Condition");
                break;
        }
    }
}
