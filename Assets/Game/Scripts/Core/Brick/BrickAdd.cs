using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAdd : MonoBehaviour
{
    [SerializeField]
    private AllColor brickColor;
    private AIInteract aIInteract;

    private void Start()
    {
        aIInteract = AIInteract.Ins;     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.Equals(brickColor.color, GameConstant.BLUE_TAG) && other.CompareTag(GameConstant.BLUE_TAG))
        {
            PlayerInteract.Ins.AddBrick(gameObject);
        }
        
        if (string.Equals(brickColor.color, GameConstant.RED_TAG) && other.CompareTag(GameConstant.RED_TAG))
        {
            AIInteract.Ins.AddBrick(GameConstant.RED_TAG,gameObject,aIInteract.RedGridBrickPos,aIInteract.RedHolder,aIInteract.RedBrickHolder);
        }
        
        if (string.Equals(brickColor.color, GameConstant.GREEN_TAG) && other.CompareTag(GameConstant.GREEN_TAG))
        {
            AIInteract.Ins.AddBrick(GameConstant.GREEN_TAG,gameObject, aIInteract.GreenGridBrickPos, aIInteract.GreenHolder, aIInteract.GreenBrickHolder);
        }
        
        if (string.Equals(brickColor.color, GameConstant.YELLOW_TAG) && other.CompareTag(GameConstant.YELLOW_TAG))
        {
            AIInteract.Ins.AddBrick(GameConstant.YELLOW_TAG,gameObject, aIInteract.YellowGridBrickPos, aIInteract.YellowHolder, aIInteract.YellowBrickHolder);
        }
    }
}
